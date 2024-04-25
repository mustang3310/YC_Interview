$('.owl-carousel').owlCarousel({
    center: true,
    loop: true,
    margin: 10,
    dots: true,
    nav: true,
    autoplay: true,
    reposiveClass: true,
    responsive: {
        0: {
            items: 1
        },
        992: {
            items: 3
        }
    }
})