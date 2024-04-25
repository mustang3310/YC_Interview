$(function () {
    $('#btnCreate').on('click', function () {
        create();
    });

    $(".dateImg").on('click', function () {
        $("#EnableDate").click();
    })

    $(".btn-back").on('click', function () {
        goToPageList();
    })

    $('#btnPreview').on('click', function () {
        preview();
    })

    $('.article-create-img-default').removeClass('d-none');
})

async function create() {
    const formData = new FormData($("#frmCreate")[0]);
    convertOptionValueToFormData(formData);
    formData.append("ImageUploader", $("#ImageUploader")[0].files[0])

    $.ajax({
        method: 'POST',
        url: `/BlogManager/Create`,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success) {
                alertMessage.success("新增", goToPageList);
            }
        },
        error: function (err) {
            if (err.status === 422) {
                $("#frmCreate").validate().showErrors(err.responseJSON.data);

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

function goToPageList() {
    location.href = '/BlogManager/List';
}

function preview() {
    const formData = new FormData($("#frmCreate")[0]);
    $("#frmCreate").append(createInput("ImageUploader", $("#ImageUploader")[0].files[0]));
    $("#frmCreate").append(createInput("ImagePath", $('#ImageUploader_Path').val()));

    const selectedValues = $('#tagSelect').val();
    selectedValues.forEach((selectedValue, i) => {
        const tagObject = JSON.parse(selectedValue);
        $("#frmCreate").append(createInput(`Tags[${i}].Id`, tagObject.Id));
        $("#frmCreate").append(createInput(`Tags[${i}].Name`, tagObject.Name));
    });
    convertOptionValueToFormData(formData);
    if ($("#Url").val().trim() === '') {
        $("#Url").val(" ");
    }
    formData.delete("__RequestVerificationToken");
    const data = Object.fromEntries(formData);
    sessionStorage.setItem("blogPreview", JSON.stringify(data));
    $("#frmCreate").submit();
}

function createInput(inputName, inputValue) {
    return $("<input>", {
        type: 'hidden',
        name: inputName,
        value: inputValue
    })
}
