document.addEventListener("DOMContentLoaded", function () {
    const productCards = document.querySelectorAll(".product-card");

    productCards.forEach(card => {
        card.addEventListener("click", function (event) {
            // Evita la redirección si se hace clic en el botón del corazón o en el botón de editar
            if (event.target.closest("button") || event.target.closest(".edit-button")) {
                event.stopPropagation();
                return;
            }

            // Redirige a la vista DisplayProduct sin datos
            window.location.href = this.getAttribute("data-url");
        });
    });
});
