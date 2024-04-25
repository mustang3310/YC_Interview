$(function () {
    const jsonPreview = sessionStorage.getItem("preview");
    const pagePreviewData = JSON.parse(jsonPreview);
    const pageViewModel = pagePreviewData.pageViewModel;
    const pageSectionViewModels = pagePreviewData.pageSectionViewModels;
    Object.keys(pageViewModel).forEach((key) => {
        const inputName = `PageViewModel.${key}`;
        const inputValue = pageViewModel[key];
        const inputElement = createInput(inputName, inputValue);
        $("#preview").append(inputElement)
    });
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
