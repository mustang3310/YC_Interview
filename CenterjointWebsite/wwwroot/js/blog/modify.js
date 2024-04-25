$(function () {
    $('#btnUpdate').on('click', function () {
        confirmMessage.modify(modify);
    });

    $('#btnPreview').on('click', function () {
        preview();
    });

    $(".btn-back").on('click', function () {
        goToPageList();
    });

    $(".dateImg").on('click', function () {
        $("#EnableDate").click();
    });

})

async function modify() {
    const formData = new FormData($("#frmUpdate")[0]);
    convertOptionValueToFormData(formData);
    formData.append("ImageUploader", $("#ImageUploader")[0].files[0])

    $.ajax({
        method: 'POST',
        url: `/BlogManager/Modify`,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success) {
                alertMessage.success("修改", goToPageList);
            }
        },
        error: function (err) {
            if (err.status === 422) {
                $("#frmUpdate").validate().showErrors(err.responseJSON.data);

                if (!err.responseJSON.data.ImageUploader) {
                    $('[data-valmsg-for="ImageUploader"]').text('');
                }

                if (!err.responseJSON.data.EnableDate) {
                    $('[data-valmsg-for="EnableDate"]').text('');
                }
                if (!err.responseJSON.data.ArticleContent) {
                    $('[data-valmsg-for="ArticleContent"]').text('');
                }
            }
        }
    })

}

function preview() {
    const formData = new FormData($("#frmUpdate")[0]);
    $("#frmUpdate").append(createInput("ImageUploader", $("#ImageUploader")[0].files[0]));
    $("#frmUpdate").append(createInput("ImagePath", $('#img_ImageUploader')[0].src));

    const selectedValues = $('#tagSelect').val();
    selectedValues.forEach((selectedValue, i) => {
        const tagObject = JSON.parse(selectedValue);
        $("#frmUpdate").append(createInput(`Tags[${i}].Id`, tagObject.Id));
        $("#frmUpdate").append(createInput(`Tags[${i}].Name`, tagObject.Name));
    });
    convertOptionValueToFormData(formData);
    formData.delete("__RequestVerificationToken");
    const data = Object.fromEntries(formData);
    sessionStorage.setItem("blogPreview", JSON.stringify(data));
    $("#frmUpdate").submit();
}

function createInput(inputName, inputValue) {
    return $("<input>", {
        type: 'hidden',
        name: inputName,
        value: inputValue
    })
}

function goToPageList() {
    location.href = '/BlogManager/List';
}

