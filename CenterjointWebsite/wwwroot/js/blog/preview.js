$(".btn-back").on("click", function () {
    sessionStorage.removeItem("blogPreview");
    $("#formUpdate").submit();
})

$(".btn-save").on("click", async function () {
    const postId = $(this).attr("data-post-id");

    const formData = new FormData($("#formUpdate")[0]);

    if (postId != "") {
        modify(formData);
        return;
    }
    create(formData);
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

function create(formData) {
    $.ajax({
        method: 'POST',
        url: `/BlogManager/Create`,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success) {
                alertMessage.success("新增", goToPageList);
                sessionStorage.removeItem("fileBase64");
                sessionStorage.removeItem("blogPreview");
            }
        },
        error: function (err) {
            if (err.status === 422) {

                errorString = errorString(err.responseJSON.data);
                alert(errorString);
                $("#formUpdate").submit();

            }
        }

    })
}

function modify(formData) {
    $.ajax({
        method: 'POST',
        url: `/BlogManager/Modify`,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success) {
                alertMessage.success("修改", goToPageList);
                sessionStorage.removeItem("fileBase64");
                sessionStorage.removeItem("blogPreview");
            }
        },
        error: function (err) {
            if (err.status === 422) {
                errorString = errorString(err.responseJSON.data);
                alert(errorString);
                $("#formUpdate").submit();
            }
        }
    })
}

function goToPageList() {
    location.href = '/BlogManager/List';
}

function errorString(data) {
    let errorString = "";
    Object.values(data).forEach((element) => {
        element.forEach((string) => {
            errorString += string + "\n";
        })
    })
    return errorString;
}
