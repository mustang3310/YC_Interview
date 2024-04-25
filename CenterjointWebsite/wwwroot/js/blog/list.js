$('#btnQuery').on('click', queryFromSearch);

$('#reset').on('click', function (e) {
    e.preventDefault();
    sessionStorage.removeItem('search');
    $('#frmQuery :input').val('');
});

function queryFromSearch(currentPage) {
    let frmQuery = $('#frmQuery')[0];
    let input = document.createElement('input');

    input.type = 'hidden';
    input.name = 'Pager.PageNumber';
    input.value = currentPage;
    frmQuery.appendChild(input);

    frmQuery.submit();
}

function query(currentPage) {
    let queryForm = $('#queryForm')[0];
    let input = document.createElement('input');

    input.type = 'hidden';
    input.name = 'Pager.PageNumber';
    input.value = currentPage;
    queryForm.appendChild(input);

    queryForm.submit();
}

$('a[id="btnDelete"]').on('click', function (e) {
    confirm("是否刪除") ? del(e) : null;
});

function del(e) {

    let frmDelete = $(e.target).parent()
    let frmDeleteData = frmDelete.serializeArray();
    let jsonData = {};
    $.each(frmDeleteData, (i, v) => {
        jsonData[v.name] = v.value;
    })

    postData(`/BlogManager/Delete`, jsonData)
        .then((res) => {
            if (res.success) {
                alertMessage.success("刪除", () => {
                    location.reload();
                });
            } else {
                frmDelete.validate().showErrors(res.data);
            }
        });
}

$(document).ready(function () {
    $("#frmQuery").submit(function (event) {
        const searchCriteria = {};
        $(this).serializeArray().forEach(function (item) {
            searchCriteria[item.name] = item.value;
        });
        sessionStorage.setItem("search", JSON.stringify(searchCriteria));
    });

    const savedSearchCriteria = sessionStorage.getItem("search");
    if (savedSearchCriteria) {
        const searchCriteria = JSON.parse(savedSearchCriteria);
        $.each(searchCriteria, function (key, value) {

            if (key.includes('Date') && value != '') {
                const inputElement = $("#" + key);
                const date = new Date(value).toLocaleString('af');
                inputElement.val(date);
            }
            $("#frmQuery [name='" + key + "']").val(value);
        });
    }
});