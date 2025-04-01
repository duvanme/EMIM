document.addEventListener("DOMContentLoaded", function () {
    console.log("El script modal.js se está ejecutando");

    const modalOverlay = document.getElementById("modal-overlay");
    const tarjetas = document.querySelectorAll(".tarjeta");
    const cerrarModal = document.getElementById("cerrar-modal");
        const form = document.querySelector("form");
        const storeIdInput = document.getElementById("storeId");

        // Asegurar que el ID de la tienda se envíe correctamente
        form.addEventListener("submit", function (event) {
            if (!storeIdInput.value) {
                event.preventDefault(); // Evita el envío si no hay storeId
                alert("Error: No se ha definido el ID de la tienda.");
            }
        });

    // Mostrar el modal al hacer clic en cualquier parte de la tarjeta o en "Ver más..."
    tarjetas.forEach(tarjeta => {
        tarjeta.addEventListener("click", function (event) {
            event.stopPropagation(); // Evita que se cierren eventos padres
            document.getElementById("storeId").value = this.dataset.storeId
            document.getElementById("storeName").textContent = this.dataset.storeName || "Sin nombre";
            document.getElementById("userName").textContent = `${this.dataset.userFirstName || "Sin nombre"} ${this.dataset.userLastName || ""}`;
            document.getElementById("userEmail").textContent = this.dataset.userEmail || "Sin nombre";
            document.getElementById("userId").textContent = this.dataset.id || "Sin nombre";
            document.getElementById("storeDescription").textContent = this.dataset.storeDescription || "Sin nombre";
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
