document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".swiper-slide").forEach(function (slide) {
        slide.addEventListener("click", function () {
            const url = this.getAttribute("data-url");
            if (url) {
                window.location.href = url;
            }
        });
    });
});
