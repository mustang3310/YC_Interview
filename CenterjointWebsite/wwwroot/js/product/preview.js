$(".btn-back").on("click", function () {
    $("#previewData").submit();
})

$(".btn-save").on("click", async function () {
    const pageId = $(this).attr("data-page-id");

    const jsonData = sessionStorage.getItem("preview");
    const previewData = JSON.parse(jsonData);
    const resp = await postDataReturnJson(`/ProductManager/ModifyPageInfoAndPageSectionList`, previewData);
    if (resp.success) {
        alert("儲存成功");
        sessionStorage.removeItem("preview");
        location.href = `/ProductManager/PageSectionList/${pageId}`;
        return;
    }
    alert(`儲存失敗 ${resp.data.Name}`);
    sessionStorage.removeItem("preview");
    location.href = `/ProductManager/PageSectionList/${pageId}`;
})

$(".btn-change-size").on("click", function () {
    const frameWidth = $("#previewFrame").width();
    if (frameWidth > 390) {
        mobilMode();
    } else {
        $("#previewFrame").width("100%");
        $("#previewFrame").removeClass("iframe-border-mobile")
        $("#previewFrame").addClass("iframe-border")
    }
})

function mobilMode() {
    $("#previewFrame").width("390px");
    $("#previewFrame").removeClass("iframe-border")
    $("#previewFrame").addClass("iframe-border-mobile")
}