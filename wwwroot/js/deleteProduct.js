let deleteProductId = null;

function confirmDelete(productId, event) {
    event.stopPropagation(); // Evita que el clic en el botón active el evento de la tarjeta
    deleteProductId = productId;
    document.getElementById("deleteModal").classList.remove("hidden");
}

function closeModal() {
    document.getElementById("deleteModal").classList.add("hidden");
    deleteProductId = null;
}

async function deleteConfirmed() {
    if (deleteProductId) {
        try {
            const response = await fetch(`/Product/DeleteProduct/${deleteProductId}`, { method: "DELETE" });

            if (response.ok) {
                location.reload(); // Recarga la página al eliminar
            } else {
                alert("Error al eliminar el producto.");
            }
        } catch (error) {
            console.error("Error:", error);
            alert("Hubo un problema al eliminar el producto.");
        }
    }
    closeModal();
}