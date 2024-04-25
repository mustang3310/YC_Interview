
const cmsTitle = "宗宸鑫網站內容編輯器";

const productTitle = "產品與解決方案";
const indexTitle = "首頁頁面管理";

const productModify = {
    title: productTitle,
    breadcrumbIitem: [cmsTitle, productTitle, "產品頁面管理", "編輯產品區塊"]
}

const indexModify = {
    title: indexTitle,
    breadcrumbIitem: [cmsTitle, indexTitle, "編輯首頁區塊"]
}

const productTemplate = {
    title: productTitle,
    breadcrumbIitem: [cmsTitle, productTitle, "模板選擇"]
}

const indexTemplate = {
    title: indexTitle,
    breadcrumbIitem: [cmsTitle, indexTitle, "模板選擇"]
}

function changeTitle(data) {
    $(".page-title").text(data.title)
    const ol = $("<ol>").addClass("breadcrumb");
    for (let i = 0; i < data.breadcrumbIitem.length; i++) {
        const li = $("<li>")
            .addClass("breadcrumb-item")
            .text(data.breadcrumbIitem[i]);
        if (i == (data.breadcrumbIitem.length - 1)) {
            li.addClass("active");
            $(document).attr("title", `${data.breadcrumbIitem[i]} - CenterJoint`);
        }
        ol.append(li);
    }
    $(".page-title").after(ol);
}

async function changeAllTitle() {
    const pageId = $(".page-title").attr("data-page-id");
    const response = await ajaxGet(`/PageGroup/GetPageGroupByPageId/${pageId}`);
    if (response.message == 1) {
        changeTitle(productTemplate);
        changeNavTitle();
    }
    if (response.message == 2) {
        changeTitle(indexTemplate);
        changeNavTitle();
    }
}