const pageController = "/ProductManager";
const controller = "/PageSectionManager";

function toPageList() {
    sessionStorage.removeItem("preview");
    location.href = `${pageController}/List`;
}

$(".sortable").sortable({
});

$(".btn-update").on("click", function () {
    const pageSectionId = $(this).attr("data-page-section-id");
    const backUrl = $(this).attr("data-back-url");
    location.href = `${controller}/ModifyPageSection/${pageSectionId}?backUrl=${backUrl}`;
})

$(".btn-create").on("click", function () {
    const pageId = $(this).attr("data-page-id");
    const backUrl = $(this).attr("data-back-url");
    location.href = `${controller}/TemplateList?pageId=${pageId}&backUrl=${backUrl}`;
})

$(".btn-back").on("click", function () {
    toPageList();
})

$(".btn-delete").on("click", async function () {
    const isConfirm = confirm("確定刪除?");
    if (!isConfirm) {
        return
    }

    const pageSectionId = $(this).attr("data-page-section-id");
    const resp = await postData(`${controller}/DeletePageSection/${pageSectionId}`);
    if (resp.success) {
        alert("刪除成功");
        location.reload();
        return;
    }
    alert("刪除失敗");
})

$(".btn-save").on("click", async function () {
    const formData = new FormData($("#page-info")[0]);
    const data = Object.fromEntries(formData);
    ajaxPost(`${pageController}/ModifyPageInfo`, data, success, error);
    function success() {
        $(`.field-validation-valid`).text("");
        alert('儲存成功');
    }
    function error(err) {
        if (err.status === 422) {
            showError(err.responseJSON.data);
        }
    }
})

$(".btn-save-sort").on("click", async function () {

    const dataList = getPageSectionIdAndIsVisible();
    const resp = await postData(`${controller}/ModifyOrdinalNumberAndVisible`, dataList);
    if (resp.success) {
        alert("儲存成功");
        return;
    }
    alert("儲存失敗");
})

$(".btn-preview").on("click", async function (event) {
    event.preventDefault();

    const formData = new FormData($("#page-info")[0]);
    const data = Object.fromEntries(formData)
    
    const valid = await postDataReturnJson(`${pageController}/CheckPageValid`, data);
    if (!valid.success) {
        showError(valid.data);
        return;
    }
    Object.keys(data).forEach((key) => {
        const inputName = `PageViewModel.${key}`;
        const inputValue = data[key];
        const inputElement = createInput(inputName, inputValue);
        $("#preview").append(inputElement)
    });
    const pageSectionList = getPageSectionIdAndIsVisible();
    pageSectionList.forEach(function (element, index) {
        Object.keys(element).forEach((key) => {
            const inputName = `PageSectionViewModels[${index}].${key}`;
            const inputValue = element[key];
            const inputElement = createInput(inputName, inputValue);
            $("#preview").append(inputElement)
        });
    })

    const pagePreviewData = { PageViewModel: data, PageSectionViewModels: pageSectionList };

    const resp = await postDataReturnJson(`${pageController}/GetPreviewData`, pagePreviewData);

    const jsonData = JSON.stringify(resp);
    sessionStorage.setItem("preview", jsonData);

    $("#preview").submit();
})


$("#Name").on("change", function () {
    if ($(this).val() != "") {
        $(".field-validation-valid").text("");
    }
})

function createInput(inputName, inputValue) {
    return $("<input>", {
        type: 'hidden',
        name: inputName,
        value: inputValue
    })
}

function getPageSectionIdAndIsVisible() {
    const dataList = [];
    $(".page-section-id").each((index, element) => {
        const data = {};
        data.Id = element.value;
        data.IsVisible = $(".is-visible").eq(index).prop("checked");
        dataList.push(data);
    })
    return dataList;
}

function showError(errorData) {
    for (let item in errorData) {
        for (let elemnt of errorData[item]) {
            $(`.field-validation-valid[data-valmsg-for='${item}']`).text(elemnt);
        }
    }
}