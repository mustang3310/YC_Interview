document.addEventListener(
    'DOMContentLoaded',
    () => {
        $(".datetimepickerInfo").each(function () {
            const datetimepickerId = $(this).attr("data-datetimepicker-id");
            const time = $(this).attr("data-time");
            intiDatetimePicker(datetimepickerId);
            intiDatetimePicker(datetimepickerId).setDate(time);

            SetDatetimePickerOnChange(datetimepickerId);

            setDateTimeValue(datetimepickerId)
        })
    }
);

function intiDatetimePicker(id) {
    let picker = flatpickr('#' + id, {
        locale: 'zh_tw'
        , enableTime: true
        , dateFormat: 'Y-m-d H:i'
    });

    return picker;
}

function SetDatetimePickerOnChange(id) {
    $('#' + id).on('change', () => {
        let datetimepicker = intiDatetimePicker(id);
        setDateTimeValue(id);
    })
}

function setDateTimeValue(id) {
    let datetimepicker = intiDatetimePicker(id);

    let isoDatetime = datetimepicker.selectedDates[0] ? new Date(datetimepicker.selectedDates[0]).toISOString() : ''

    $('input[name=' + id + ']').val(isoDatetime);
}