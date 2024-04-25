
$('.delete-success-type').on('click', async function (e) {
    const isConfirm = confirm("確定刪除?");
    if (!isConfirm) {
        return
    }

    const stringId = $(this).data('id');
    const resp = await postData(`/SuccessCaseTypeManager/Delete/${stringId}`);
    if (resp.success) {
        alert("刪除成功");
        location.reload();
        return;
    }
    alert("刪除失敗");
});

$('.btn-create').on('click', async function () {

    const parentIdValue = $("#typeParentId").val();
    const parentId = parentIdValue !== "" ? parentIdValue : null;

    const formData = {
        Name: $("#typeName").val(),
        IsShow: $("#typeIsShow").prop("checked"),
        ParentId: parentId
    };

    const resp = await postDataReturnJson('/SuccessCaseTypeManager/Create', formData);

    if (resp.success) {
        alert("新增成功");
        location.reload();
        return;
    }
    $(".field-validation-valid").text(resp.data.Name)

});


$('.modify-modal').on('click', function () {

    var updateInfo = $(this).data('updateinfo');

    $("#typeModifyId").val(updateInfo.id);
    $("#typeModifyName").val(updateInfo.name);
    $("#typeModifyIsShow").prop("checked", updateInfo.isshow === true || updateInfo.isshow === "True");
    $("#typeModifyParentId").val(updateInfo.parentId);

    var dropdownList = $("#typeModifyParentId");
    dropdownList.find('option').show();

    dropdownList.find('option').filter(function () {
        return $(this).val() === updateInfo.id.toString();
    }).hide();

});

$('.btn-modify').on('click', async function () {

    const formData = {
        Id: $("#typeModifyId").val(),
        Name: $("#typeModifyName").val(),
        IsShow: $("#typeModifyIsShow").prop("checked"),
        ParentId: $("#typeModifyParentId").val() !== "" ? $("#typeModifyParentId").val() : null
    };

    const resp = await postDataReturnJson('/SuccessCaseTypeManager/Modify', formData);

    if (resp.success) {
        alert("新增成功");
        location.reload();
        return;
    }
    $(".field-validation-valid").text(resp.data.Name)

});




