document.addEventListener("DOMContentLoaded", function () {
    console.log("El script modal.js se está ejecutando");

    const modalOverlay = document.getElementById("modal-overlay");
    const tarjetas = document.querySelectorAll(".tarjeta");
    const cerrarModal = document.getElementById("cerrar-modal");

    // Mostrar el modal al hacer clic en cualquier parte de la tarjeta o en "Ver más..."
    tarjetas.forEach(tarjeta => {
        tarjeta.addEventListener("click", function (event) {
            event.stopPropagation(); // Evita que se cierren eventos padres
            modalOverlay.classList.remove("hidden"); 
            modalOverlay.classList.add("flex"); 
        });
    });

    // Cerrar el modal al hacer clic en "Denegar Tienda"
    cerrarModal.addEventListener("click", function () {
        modalOverlay.classList.add("hidden"); 
        modalOverlay.classList.remove("flex");
    });

    // Cerrar el modal si el usuario hace clic fuera del cuadro
    modalOverlay.addEventListener("click", function (event) {
        if (event.target === modalOverlay) {
            modalOverlay.classList.add("hidden");
            modalOverlay.classList.remove("flex");
        }
    });
});
