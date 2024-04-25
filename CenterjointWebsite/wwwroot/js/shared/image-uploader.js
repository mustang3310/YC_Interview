document.addEventListener(
    'DOMContentLoaded',
    () => {
        $('#ImageUploader').on('change', (e) => {
            $('.article-create-img-default').addClass('d-none');
            if (validImageSize(e)) {
                $(e.target).val("");
            }
            ImageChange($(e.target));
        })
    });

function changeImgSrc(response) {
    $("#ImageUploader_Path").val(response);
}
/**
 * 顯示 已經載入畫面的圖片(且上傳)
 */
function ImageChange(jqElement) {
    const [files] = jqElement[0].files;
    const viewdata = jqElement.attr('id');
    if (files) {
        const bloburl = URL.createObjectURL(files);
        $('#img_' + viewdata)[0].src = bloburl;
        const formData = new FormData();
        formData.append("formFile", files);
        ajaxPostWithImage(`/File/SaveTempFile`, formData, changeImgSrc);
    } else {
        const bloburl = "";
        $('#img_' + viewdata)[0].src = bloburl;
        $('#BlobUrl').val(bloburl);
    }
}

/**
 * 檢驗圖片上傳大小
 */
function validImageSize(e) {

    const fileInput = e.target;
    const file = fileInput.files[0];
    if (!file) {
        return;
    }
    const fileSize = file.size;
    const maxSize = 2048 * 1024; // 2MB
    let res = fileSize > maxSize;

    if (res) {
        alert('File size must be less than 2MB');
    }

    return res;
}

function getBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}

