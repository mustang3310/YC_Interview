jQuery.validator.setDefaults({
    showErrors: function (errorMap, errorList) {
        // error column loop
        errorList =
            $.each(errorList, (i, v) => {
                // column error messages
                if (Array.isArray(v.message)) {
                    let itemValue = [];

                    $.each(v.message, (vi, vv) => {
                        itemValue.push(vv);

                        if (vi % 2 == 0) {
                            itemValue.push('</br>')
                        }
                    });

                    v.message = itemValue;
                }
            })
        this.defaultShowErrors();
    },
});