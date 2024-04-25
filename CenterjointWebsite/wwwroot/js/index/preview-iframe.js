$(function () {
    const jsonPreview = sessionStorage.getItem("indexPageSection");
    const pageSectionViewModels = JSON.parse(jsonPreview);
    pageSectionViewModels.forEach(function (element, index) {
        Object.keys(element).forEach((key) => {
            const inputValue = element[key];
            if (Array.isArray(inputValue)) {
                inputValue.forEach(function (e, i) {
                    const inputName = `PageSectionViewModels[${index}].${key}[${i}]`;
                    const inputArrayValue = e;
                    const inputElement = createInput(inputName, inputArrayValue);
                    $("#preview").append(inputElement)
                })
            } else {
                const inputName = `PageSectionViewModels[${index}].${key}`;
                const inputElement = createInput(inputName, inputValue);
                $("#preview").append(inputElement)
            }
        });
    })

    $("#preview").submit();
})

function createInput(inputName, inputValue) {
    return $("<input>", {
        type: 'hidden',
        name: inputName,
        value: inputValue
    })
}