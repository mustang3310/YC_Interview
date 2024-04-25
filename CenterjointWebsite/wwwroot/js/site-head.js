document.addEventListener("DOMContentLoaded", () => {
    const date = new Date();
    document.cookie = `timeoffest=${-(date.getTimezoneOffset())}; path=/;`;
});