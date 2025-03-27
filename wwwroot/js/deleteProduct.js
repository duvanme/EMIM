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
            const response = await fetch(`/Product/DeleteProduct/${deleteProductId}`, { 
                method: "DELETE",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });

            const data = await response.json();

            if (response.ok) {
                // Eliminar el producto de la vista sin recargar
                const productCard = document.querySelector(`.product-card[data-product-id="${deleteProductId}"]`);
                if (productCard) {
                    productCard.remove();
                }
                closeModal();
            } else {
                // Mostrar mensaje de error más detallado
                alert(`Error: ${data.message}\nDetalles: ${data.details || 'Sin detalles adicionales'}`);
            }
        } catch (error) {
            console.error("Error:", error);
            alert("Hubo un problema al eliminar el producto.");
        }
    }
}

// Función opcional para actualizar el contador de productos
function updateProductCount() {
    const productCountElement = document.getElementById('product-count');
    if (productCountElement) {
        let currentCount = parseInt(productCountElement.textContent);
        productCountElement.textContent = currentCount - 1;
    }
}