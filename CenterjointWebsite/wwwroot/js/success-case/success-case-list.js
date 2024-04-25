$("#typeSelect").on("change", function () {
    const typeId = $(this).val();
    window.location.href = `/SuccessCaseManager/List?page=1&typeId=${typeId}`;
})

$(".create-btn").on("click", function () {
    window.location.href = "/SuccessCaseManager/Modify";
});

$(".delete-btn").on("click", async function () {
    const isConfirm = confirm("確定刪除?");
    if (!isConfirm) {
        return
    }

    const stringId = $(this).data('id');
    const resp = await postData(`/SuccessCaseManager/Delete/${stringId}`);
    if (resp.success) {
        alert("刪除成功");
        location.reload();
        return;
    }
    alert("刪除失敗");
});

$(".modify-btn").on("click", function () {

   const id = $(this).attr("data-id")
    
    window.location.href = `/SuccessCaseManager/Modify/${id}`;
});

