// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const confirmMessage = {
    modify: (acceptFunc, cancelFunc) => {
        confirmFunction('是否修改', acceptFunc, cancelFunc);
    },
    delete: (acceptFunc, cancelFunc) => {
        confirmFunction('是否刪除', acceptFunc, cancelFunc);
    }
}

const confirmFunction = (confirmString, acceptFunc, cancelFunc) => {
    const yes = confirm(confirmString);
    if (yes) {
        acceptFunc && acceptFunc();
    } else {
        cancelFunc && cancelFunc();
    }
}

const alertMessage = {
    success: (actionName, func) => {
        alert(`${actionName}成功`,);
        func();
    },
    fail: (actionName) => {
        alert(`${actionName}失敗`);
    },
    internalServerError: (error) => {
        alert(`系統發生錯誤，請紀錄下方識別碼後，回報系統管理員，謝謝 \r\n ${error.Guid}`);
    }
}

const postData =
    async (url = '', data = {}, successFunc,) => {
        return fetch(url, { ...fetDataConfig(data), method: 'POST' })
            .then((res) => {
                return res.json();
            })
            .then((json) => {
                if (json.success)
                    return json;
                else
                    throw json.Guid;
            })
            .catch((error) => {
                loadingshow();
                alertMessage.internalServerError(error);
                loadingRemove();

            })
    };

const postDataReturnJson =
    async (url = '', data = {}, successFunc,) => {
        return fetch(url, { ...fetDataConfig(data), method: 'POST' })
            .then((res) => {
                return res.json();
            })
            .then((json) => {
                return json;
            })
            .catch((error) => {
                loadingshow();
                alertMessage.internalServerError(error);
                loadingRemove();

            })
    };

const ajaxPostWithImage = function (url, formData, successFunc, errorFunc) {
    $.ajax({
        method: 'POST',
        url: url,
        data: formData,
        contentType: false,
        processData: false,
        headers: {
            'RequestVerificationToken': csrfToken
        },
        success: function (response) {
            successFunc && successFunc(response);
        },
        error: function (err) {
            errorFunc ? errorFunc(err) : alert(err);
        }
    });
}

const getData = async (url = '', data = {}) => {
    return fetch(url, { ...fetDataConfig(data), method: 'GET' })
        .then((res) => {
            return res.json();
        })
        .then((json) => {
            if (json.success)
                return json;
            else
                throw json.Guid;
        })
        .catch((error) => {
            alertMessage.internalServerError(error);
        })
};

const csrfToken = $("#__AjaxAntiForgeryForm").find("input").val();

const fetDataConfig = (data) => {
    return {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': csrfToken
        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: JSON.stringify(data), // body data type must match "Content-Type" header
    }
}

const utsiUtilities = {
    fetch: {
        post: postData,
        get: getData
    },
}

/**
 * 加上loading icon,發送ajax前使用
 */
function loadingshow() {
    $body = $("body");
    $body.addClass("loading");
}

/**
 * 加上loading icon,發送ajax後使用
 */
function loadingRemove() {
    setTimeout(function () {
        $body = $("body");
        $body.removeClass("loading");
    }, 1000);
}

function urltoFile(url, filename, mimeType) {
    if (url.startsWith('data:')) {
        var arr = url.split(','),
            mime = arr[0].match(/:(.*?);/)[1],
            bstr = atob(arr[arr.length - 1]),
            n = bstr.length,
            u8arr = new Uint8Array(n);
        while (n--) {
            u8arr[n] = bstr.charCodeAt(n);
        }
        var file = new File([u8arr], filename, { type: mime || mimeType });
        return Promise.resolve(file);
    }
    return fetch(url)
        .then(res => res.arrayBuffer())
        .then(buf => new File([buf], filename, { type: mimeType }));
}

const csrfTokenPost = new FormData($("#__AjaxAntiForgeryForm")[0]);
const csrfObj = {};
csrfTokenPost.forEach((value, key) => (csrfObj[key] = value));

function ajaxPost(url, requestData, successFunc, errorFunc, option = {}) {
    return $.ajax({
        url: url,
        method: "POST",
        data: JSON.stringify(requestData),
        contentType: "application/json;charset=utf-8",
        headers: { "RequestVerificationToken": csrfObj.__RequestVerificationToken },
        dataType: "json",
        ...option,
        success: function (res) {
            successFunc && successFunc(res);
        },
        error: function (err) {
            errorFunc && errorFunc(err);
        }
    });
}
function ajaxGet(url, requestData, option = {}) {
    return $.ajax({
        url: url,
        method: "GET",
        data: requestData,
        dataType: "json",
        success: function (res) {
        },
        error: function (err) {
            console.log(err)
        }
    });
}