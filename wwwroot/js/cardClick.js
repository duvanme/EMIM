document.addEventListener("DOMContentLoaded", function () {
    const productCards = document.querySelectorAll(".product-card");

    productCards.forEach(card => {
        card.addEventListener("click", function (event) {
            // Evita la redirección si se hace clic en los botones
            if (event.target.closest("button") || event.target.closest(".edit-button")) {
                event.stopPropagation();
                return;
            }

            // Obtiene la URL desde el atributo data-url y redirige
            const url = this.getAttribute("data-url");
            if (url) {
                window.location.href = url;
            }
        });
    });
});
