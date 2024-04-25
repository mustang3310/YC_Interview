const controller = "/PageSectionManager";

$(".sortable").sortable({
});

$(".btn-save-sort").on("click", async function () {
    const dataList = getPageSectionIdAndIsVisible();
    const resp = await postData(`${controller}/ModifyOrdinalNumberAndVisible`, dataList);
    if (resp.success) {
        alert("儲存成功");
        location.reload();
        return;
    }
    alert("儲存失敗");
})

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

$(".btn-preview").on("click", async function () {

    const pageSectionList = getPageSectionIdAndIsVisible()
    pageSectionList.forEach(function (element, index) {
        Object.keys(element).forEach((key) => {
            const inputName = `PageSectionViewModels[${index}].${key}`;
            const inputValue = element[key];
            const inputElement = createInput(inputName, inputValue);
            $("#preview").append(inputElement)
        });
    })
    const resp = await postDataReturnJson(`/IndexManager/GetPageSectionViewModel`, pageSectionList);
    const jsonData = JSON.stringify(resp);
    sessionStorage.setItem("indexPageSection", jsonData);

    $("#preview").submit();
})

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
function createInput(inputName, inputValue) {
    return $("<input>", {
        type: 'hidden',
        name: inputName,
        value: inputValue
    })
}