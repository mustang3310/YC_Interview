const controller = "/ProductManager";

$(".btn-update").on("click", function () {
    const pageId = $(this).attr("data-page-id");
    location.href = `/ProductManager/PageSectionList/${pageId}`;
})

$(".sortable").sortable({
});

$(".btn-save").on("click", async function () {
    const dataList = [];
    $(".page-view-model").each((index, element) => {
        const id = $(element).children(".page-id").val();
        const isVisible = $(element).find(".is-visible").prop("checked");
        const ordinalNumber = index + 1;
        model = {
            id: id,
            isVisible: isVisible,
            ordinalNumber: ordinalNumber
        };
        dataList.push(model);
    })
    const resp = await postData(`${controller}/ModifyOrdinalNumberAndVisible`, dataList);
    if (resp.success) {
        alert("儲存成功");
        return;
    }
    alert("儲存失敗");
})

$(".btn-delete").on("click", async function () {
    const isConfirm = confirm("確定刪除?");
    if (!isConfirm) {
        return
    }

    const pageId = $(this).attr("data-page-id");
    const resp = await postData(`${controller}/Delete/${pageId}`);
    if (resp.success) {
        alert("刪除成功");
        location.reload();
        return;
    }
    alert("刪除失敗");
})

$(".btn-create").on("click", async function () {
    const name = $("#productName").val();
    const url = $("#productUrl").val();
    ajaxPost(`${controller}/Create`, { Name: name, Url: url }, success, error);
    function success(resp) {
        if (resp.success) {
            alert("新增成功");
            location.reload();
        }
    }

    function error(err) {
        showError(err.responseJSON.data);
    }
    function showError(errorData) {
        for (let item in errorData) {
            for (let elemnt of errorData[item]) {
                $(`.field-validation-valid[data-valmsg-for='${item}']`).text(elemnt);
            }
        }
    }
})