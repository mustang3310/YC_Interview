$(".btn-template").on("click", function () {
    const templateId = $(this).attr("data-template-id");
    const pageId = $(this).attr("data-page-id");
    const pageSectionId = $(this).attr("data-page-section-id");
    const backUrl = $(this).attr("data-back-url");
    location.href = `/PageSectionManager/ModifyPageSection/${pageSectionId}?templateId=${templateId}&pageId=${pageId}&backUrl=${backUrl}`;
})

$(".btn-back").on("click", async function () {
    const pageId = $(this).attr("data-page-id");
    const pageSectionId = $(this).attr("data-page-section-id");
    const backUrl = $(this).attr("data-back-url");

    const backAction = pageSectionId == 0 ? `${backUrl}` : "PageSectionManager/ModifyPageSection";
    const id = pageSectionId == 0 ? pageId : pageSectionId;

    location.href = `/${backAction}/${id}?backUrl=${backUrl}`;

})

$(async function () {
    changeAllTitle();
})