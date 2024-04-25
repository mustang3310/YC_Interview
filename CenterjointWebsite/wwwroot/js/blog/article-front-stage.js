$('a[name="pager-button"]').on('click', query);

function query(currentPage) {
    let input = document.createElement('input');

    input.type = 'hidden';
    input.name = 'Pager.PageNumber';
    input.value = currentPage;

    let url = `/Blog/List?Pager.PageNumber=${currentPage}`;
    window.location.href = url;

}

$(document).ready(function () {

    let content = $('#content').data('content');
    let sanitizeHtml = DOMPurify.sanitize(content);
    $('#content').html(sanitizeHtml);

    var articleSection = "articleSection";

    var targetSection = $("#" + articleSection);

    if (targetSection.length > 0) {
        $('html, body').animate({
            scrollTop: targetSection.offset().top
        }, 100);
    }
});


    $(".article-fb-share").on('click', function () {
        const currentUrl = window.location.href;
        const facebookShareUrl = 'https://www.facebook.com/sharer/sharer.php?u=' + currentUrl;

        window.open(facebookShareUrl, '_blank');
    });

$(".article-line-share").on('click', function () {
    const currentUrl = window.location.href;
    const lineShareUrl = 'https://lineit.line.me/share/ui?url=' + currentUrl;

    window.open(lineShareUrl, '_blank');
});

$(".article-linked-share").on('click', function () {
    const currentUrl = window.location.href;
    const linkedShareUrl = 'https://www.linkedin.com/sharing/share-offsite/?url=' + currentUrl;

    window.open(linkedShareUrl, '_blank');
});
