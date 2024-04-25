
function toggleTemplate(templateName) {
    const showTemplateSelector = `.show${templateName}`;
    const clickTemplateSelector = `.click${templateName}`;
    const hideShowDivSelector = `.hide-show${templateName}-div`;

    const hideShowDiv = document.querySelectorAll(hideShowDivSelector);
    const showTemplate = document.querySelectorAll(showTemplateSelector);
    const clickTemplate = document.querySelectorAll(clickTemplateSelector);

    const index = hideShowDiv.length - 1;

    const templateTop = findParentWithClass(clickTemplate[index], "templateTop");
    const screenWidth = window.innerWidth || document.documentElement.clientWidth;

    clickTemplate[index].addEventListener("click", function () {
        clickTemplate[index].style.opacity = "0";
        clickTemplate[index].classList.remove("cursor-pointer");
        showTemplate[index].classList.remove("d-none");
    });

    hideShowDiv[index].addEventListener("click", function () {
        showTemplate[index].classList.add("d-none");
        clickTemplate[index].classList.add("cursor-pointer");
        clickTemplate[index].style.opacity = "1";

        if (screenWidth < 992) {
            scrollToTemplateTop();
        }
    });

    function findParentWithClass(element, className) {
        // 遞歸向上查找父元素，直到找到具有指定class的元素
        while (element && !element.classList.contains(className)) {
            element = element.parentElement;
        }

        return element; // 返回找到的父元素，或者為null（如果未找到）
    }

    function scrollToTemplateTop() {
        window.scrollTo({
            top: templateTop.offsetTop,
            behavior: 'smooth' // 使用平滑的滾動效果
        });
    }

}
