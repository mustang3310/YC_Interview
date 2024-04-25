document.addEventListener('DOMContentLoaded', function () {

    const selectElement = document.querySelector('#tagSelect');
    const maxCount = selectElement.getAttribute('data-maxCount');
    const optionDataString = selectElement.getAttribute('data-optiondata');

    const optionData = JSON.parse(optionDataString);

    const dataArray = optionData.map(item => ({
        id: JSON.stringify({ Id: item.Id, Name: item.Name }),
        text: item.Name
    }));

    $('#tagSelect').select2({
        maximumSelectionLength: maxCount,
        data: dataArray,
        tags: true,
        createTag: function (params) {
            return {
                id: JSON.stringify({ Id: 0, Name: params.term }),
                text: params.term
            }
        }
    });

    const selectOptionData = selectElement.getAttribute('data-selected');
    const selectedData = JSON.parse(selectOptionData);
    const selectedValues = selectedData.map(item => JSON.stringify({
        Id: item.Id,
        Name: item.Name
    }));

    $("#tagSelect").val(selectedValues).trigger("change");
    
});

function convertOptionValueToFormData(formData) {
    const selectedValues = $('#tagSelect').val();

    selectedValues.forEach((selectedValue, i) => {
        const tagObject = JSON.parse(selectedValue);
        formData.append(`Tags[${i}].Id`, tagObject.Id);
        formData.append(`Tags[${i}].Name`, tagObject.Name);
    });

}
