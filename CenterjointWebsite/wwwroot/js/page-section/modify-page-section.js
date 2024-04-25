const controller = "/PageSectionManager";

$(".btn-choose-template").on("click", function () {
    const pageSectionId = $(this).attr("data-page-section-id");
    const pageId = $(this).attr("data-page-id");
    const backUrl = $(".btn-back").attr("data-back-url");
    location.href = `${controller}/TemplateList?pageSectionId=${pageSectionId}&pageId=${pageId}&backUrl=${backUrl}`;
})

async function ReturnToList() {
    const pageId = $(".btn-back").attr("data-page-id");
    const backUrl = $(".btn-back").attr("data-back-url");

    location.href = `/${backUrl}/${pageId}`;
}

$(".btn-back").on("click", function () {
    ReturnToList();
})

$(".image-input").on("change", function () {
    const formData = new FormData();
    const file = $(this)[0].files[0];
    formData.append("formFile", file)

    const imageInput = $(this);
    const changeImgSrc = function (response) {
        imageInput.parent(".input-group").prev().val(response);
    }

    ajaxPostWithImage(`/File/SaveTempFile`, formData, changeImgSrc);
})

$(".btn-save").on("click", async function () {

    const formData = new FormData($("#pageSection")[0]);

    $.ajax({
        method: 'POST',
        url: `${controller}/ModifyPageSection`,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (!response.success) {
                $("#pageSection").validate().showErrors(response.data);
                return;
            }

            alert('儲存成功');
            ReturnToList();
        }
    })
})

$(function () {
    $(".text-input").each(function (index, element) {
        $(element).attr("data-index", index);
        const thisText = $(this).val();
        if (thisText == "") {
            const changeText = `文字區塊${index + 1}`;
            $(this).val(changeText);
            $(".text-block").eq(index).text(changeText);
        }
    })
    $(".image-input").each(function (index, element) {
        $(element).attr("data-index", index);
    })
})

$(function () {
    let fileName = "";
    $(".image-input").on("click", () => {
        fileName = $(this).val();
    })

    $(".image-input").on("change", (e) => {

        if (validImageSize(e)) {
            $(e.target).val(fileName);
        }
        ImageChange($(e.target));
    })
})

$(".text-input").on("input", function () {
    const changeText = $(this).val();
    const textIndex = $(this).data("index");
    $(".text-block").eq(textIndex).text(changeText);
});

$(".show-block").on("click", function () {
    const dataShow = $(this).attr("data-show");
    const isShow = dataShow === "true";
    if (!isShow) {
        $(".image-block").each(function (index, element) {
            const imageBlock = `圖片區塊${index + 1}`;
            const imageTextDiv = createImageIndexText(imageBlock);
            const imageContainer = $(element).parent();
            $(this).addClass("border-dotted");
            imageContainer.append(imageTextDiv);
        })

        $(".text-block").addClass("border-dotted");
        $(this).attr("data-show", !isShow);
        return;
    }

    $(".image-block").each(function (index, element) {
        const imageText = $(element).parent().find(".image-text");
        imageText.remove();
    })
    $(".image-block").removeClass("border-dotted");
    $(".text-block").removeClass("border-dotted");
    $(this).attr("data-show", !isShow);
})

/**
 * 顯示 已經載入畫面的圖片(但是尚未上傳)
 */
function ImageChange(jqElement) {
    const [file] = jqElement[0].files;
    const imageIndex = jqElement.attr("data-index");
    if (file) {
        $(".image-block").eq(imageIndex)[0].src = URL.createObjectURL(file);
    } else {
        $(".image-block").eq(imageIndex)[0].src = "/img/default_img.jpg";
    }
}

/**
 * 檢驗圖片上傳大小
 */
function validImageSize(e) {

    const fileInput = e.target;
    const file = fileInput.files[0];
    if (!file) {
        return;
    }
    const fileSize = file.size;
    const maxSize = 2048 * 1024; // 2MB
    let res = fileSize > maxSize;

    if (res) {
        alert('File size must be less than 2MB');
    }

    return res;
}

function createImageIndexText(text) {
    const imageText = $("<div>").addClass("image-text");
    imageText.text(text);
    return imageText;
}

$(async function () {
    const pageId = $(".page-title").attr("data-page-id");
    const response = await ajaxGet(`/PageGroup/GetPageGroupByPageId/${pageId}`);
    if (response.message == 1) {
        changeTitle(productModify);
        changeNavTitle();
    }
    if (response.message == 2) {
        changeTitle(indexModify);
        changeNavTitle();
    }
})
