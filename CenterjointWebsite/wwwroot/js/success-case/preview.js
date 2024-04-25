$(".btn-back").on("click", function () {
    $("#formUpdate").submit();
})

$(".btn-save").on("click", async function () {
    const formData = new FormData($("#formUpdate")[0]);
    const storedValuesString = sessionStorage.getItem("tagValue");
    const selectedValues = JSON.parse(storedValuesString);
    selectedValues.forEach((selectedValue, i) => {
        const tagObject = JSON.parse(selectedValue);
        formData.append(`Tags[${i}].Id`, tagObject.Id);
        formData.append(`Tags[${i}].Name`, tagObject.Name);
    });
    const postId = formData.get("StringId");

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
        url: `/SuccessCaseManager/Create`,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (!response.success) {
                errorString = errorString(response.data);
                alert(errorString);
                $("#formUpdate").submit();
            } else if (response.success) {
                sessionStorage.removeItem("tagValue");
                alertMessage.success("新增", goToPageList);

            }
        }
    })
}

function modify(formData) {
    $.ajax({
        method: 'POST',
        url: `/SuccessCaseManager/Modify`,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (!response.success) {
                errorString = errorString(response.data);
                alert(errorString);
                $("#formUpdate").submit();
            } else if (response.success) {
                sessionStorage.removeItem("tagValue");
                alertMessage.success("修改", goToPageList);
            }

        }

    })
}

function goToPageList() {
    location.href = '/SuccessCaseManager/List';
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

$(document).ready(function () {
    const storedValuesString = sessionStorage.getItem("tagValue");
    const selectedValues = JSON.parse(storedValuesString);

    $("#formUpdate input[name='TagsValue']").val(JSON.stringify(selectedValues));

    const formData = new FormData($("#formUpdate")[0]);

    const formDataObject = {};
    formData.forEach((value, key) => {
        formDataObject[key] = value;
    });

    const parsedObjects = selectedValues.map(jsonString => JSON.parse(jsonString));

    formDataObject["Tags"] = parsedObjects;

    const formDataJson = JSON.stringify(formDataObject);
    $("#previewFrame").attr("src", `/SuccessCase/List?newData=${encodeURIComponent(formDataJson)}`);

});


