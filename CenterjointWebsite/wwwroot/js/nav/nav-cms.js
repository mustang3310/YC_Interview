
$(".sortable").sortable({
});

$(".modify").on("click", async function () {
    const dataList = [];
    $(".page-view-model").each((index, element) => {
        const id = $(element).find(".page-id").val();
        const isShow = $(element).find(".is-show").prop("checked");
        const ordinalNumber = index + 1;
        model = {
            Id: id,
            IsShow: isShow,
            OrdinalNumber: ordinalNumber
        };
        dataList.push(model);
    })

    const resp = await postData(`/NavItemsManager/ModifyOrdinalNumberAndIsShow`, dataList);

    if (resp.success) {
        alert("儲存成功");
        location.reload();
        return;
    }
    alert("儲存失敗");
})


$(".modify-nav-item").on("click", async function (event) {

    const isConfirm = confirm("確定修改?");
    if (!isConfirm) {
        return
    }
    var itemId = $(this).data("nav-item-id");
    var formSelector = `#navItemFormData-${itemId}`;
    var formData = new FormData($(formSelector)[0]);
    loadingshow();
    $.ajax({
        method: 'POST',
        url: '/NavItemsManager/Modify',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            loadingRemove();
            if (response.success) {
                alert("儲存成功");
                location.reload();
                return;
            } else {
                $(`#navItemFormData-${itemId}`).validate().showErrors(response.data);
            }
        }

    });
});






