document.addEventListener(
    'DOMContentLoaded',
    () => {
        const pageCount = $("#pager").attr("data-total-page");

        $('#startPage').on('click', function (e) {
            query(1)
        });

        $('#previousPage').on('click', function (e) {
            let pageNumber = parseInt($('#pager').find('.active').text());

            if (pageNumber <= 1) {
                query(parseInt(1));
            } else {
                query(parseInt($('#pager').find('.active').text()) - 1)
            }
        });

        $('#nextPage').on('click', function (e) {
            let pageNumber = parseInt($('#pager').find('.active').text());

            if (pageNumber >= pageCount) {
                query(pageCount);
            } else {
                query(pageNumber + 1);
            }
        });

        $('#endPage').on('click', function (e) {
            query(pageCount)
        });

        $('a[name="pager-button"]').on('click', function (e) {
            query(e.target.text)
        });
    }
);