$(function () {
    $('.article-create-img-default').removeClass('d-none');
})
//送出修改表單(modify 頁面)
$("#submitModify").on("click", async function (e) {

    const formData = new FormData($("#successCase")[0]);
    convertOptionValueToFormData(formData);

    $.ajax({
        method: 'POST',
        url: '/SuccessCaseManager/Modify',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (!response.success) {
                $("#successCase").validate().showErrors(response.data);
                return;
            }

            alertMessage.success("修改", ReturnToList);
        }
    })
});

$("#submitCreate").on("click", async function (e) {
    const formData = new FormData($("#successCase")[0]);
    convertOptionValueToFormData(formData);

    $.ajax({
        method: 'POST',
        url: '/SuccessCaseManager/Create',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (!response.success) {
                $("#successCase").validate().showErrors(response.data);
                return;
            }

            alertMessage.success("新增", ReturnToList);
        }
    })
})

$("#btn_back_to_success_list").on("click", function () {
    ReturnToList();
});

$("#btnPreview").on("click", function (e) {
    e.preventDefault();
    const selectedValues = $('#tagSelect').val();
    const selectedValuesString = JSON.stringify(selectedValues);
    sessionStorage.setItem("tagValue", selectedValuesString);
    $("#successCase").submit();
});

function ReturnToList() {
    window.location.href = `/SuccessCaseManager/List`
};

$("#ImageUploader").on("change", function () {
    const formData = new FormData();
    const file = $(this)[0].files[0];
    formData.append("formFile", file)

    const changeImgSrc = function (response) {
        $('#BlobUrl').val(response);
    }

    ajaxPostWithImage(`/File/SaveTempFile`, formData, changeImgSrc);
})

$("#typeIsShow").on("change", function () {
    const selectedValue = $(this).val();
    selectedTypeNotShowError(selectedValue);
})

$(document).ready( function () {
    const selectedValue = $("#typeIsShow").val();
    selectedTypeNotShowError(selectedValue);
});

async function selectedTypeNotShowError(selectedValue) {
    const response = await ajaxGet(`/SuccessCaseTypeManager/Query/${selectedValue}`);
    if (!response.data.iS_SHOW) {
        $("#typeAlert").text("此區域目前預設不顯示於前台");
    } else {
        $("#typeAlert").text("");
    }

}