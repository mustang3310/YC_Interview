$(function () {
    const jsonPreview = sessionStorage.getItem("blogPreview");
    const blogPreviewData = JSON.parse(jsonPreview);
    Object.keys(blogPreviewData).forEach((key) => {
        const inputName = `${key}`;
        const inputValue = blogPreviewData[key];
        const inputElement = createInput(inputName, inputValue);
        $("#blogPage").append(inputElement)
    });

    $("#blogPage").submit();
})

function createInput(inputName, inputValue) {
    return $("<input>", {
        type: 'hidden',
        name: inputName,
        value: inputValue
    })
}
