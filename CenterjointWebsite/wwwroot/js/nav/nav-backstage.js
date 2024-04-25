
$(document).ready(changeNavTitle());

function changeNavTitle() {
    const $pageTitle = $(".page-title");

    if ($pageTitle.length > 0) {
        const pageTitleText = $pageTitle.text();
        $(".dropdown-btn .title-text").text(pageTitleText);
    }
}
$(".jq-goTop").click(function (e) {
    e.preventDefault();
    $("html,body").animate(
        {
            scrollTop: 0,
        },
        300
    );
});

$(window).scroll(function () {
    if ($(window).scrollTop() > 200) {
        if ($(".goTop").hasClass("hide")) {
            $(".goTop").toggleClass("hide");
        }
    } else {
        $(".goTop").addClass("hide");
    }
});