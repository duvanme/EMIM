document.addEventListener("DOMContentLoaded", function() {
    // Manejo de botones para eliminar favoritos
    const removeFavoriteButtons = document.querySelectorAll(".remove-favorite-btn");
    
    removeFavoriteButtons.forEach(button => {
        button.addEventListener("click", async function(e) {
            e.stopPropagation(); // Importante para evitar la navegación al producto
            const productId = this.getAttribute("data-id");
            
            try {
                const response = await fetch(`/api/favorites/remove/${productId}`, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json',
                        'X-Requested-With': 'XMLHttpRequest'
                    },
                    credentials: 'include'
                });
                
                if (response.ok) {
                    // Encontrar y quitar la tarjeta del producto
                    const productCard = this.closest(".product-card");
                    if (productCard) {
                        productCard.remove();
                        
                        // Actualizar contador de favoritos
                        const countElement = document.querySelector("button.bg-\\[\\#A9C8CF\\]") || 
                                           document.querySelector("span.bg-\\[\\#A9C8CF\\]");
                        if (countElement) {
                            const currentCount = parseInt(countElement.textContent.match(/\d+/)[0]);
                            const newCount = currentCount - 1;
                            countElement.textContent = `${newCount} productos en favoritos`;
                            
                            // Si estamos en la página de perfil y los favoritos bajan a 3 o menos
                            // ocultar el enlace "Ver todos"
                            if (newCount <= 3) {
                                const viewAllLink = document.querySelector("a[href='/User/Favorites']");
                                if (viewAllLink && viewAllLink.parentElement) {
                                    viewAllLink.parentElement.style.display = 'none';
                                }
                            }
                        }
                        
                        // Si no quedan productos, mostrar mensaje
                        const container = document.querySelector(".flex.flex-col.md\\:flex-row.justify-center") ||
                                        document.querySelector(".grid");
                        
                        if (container && container.querySelectorAll(".product-card").length === 0) {
                            if (window.location.pathname === "/User/Favorites") {
                                // Estamos en la página completa de favoritos
                                container.innerHTML = `
                                    <div class="bg-white rounded-lg shadow-lg p-8 text-center col-span-full">
                                        <p class="text-gray-500 mb-4">No tienes productos favoritos aún.</p>
                                        <a href="/" class="bg-[#13678A] text-white px-4 py-2 rounded-lg hover:bg-[#0D4F65]">
                                            Explorar productos
                                        </a>
                                    </div>
                                `;
                            } else {
                                // Estamos en el perfil
                                container.innerHTML = `
                                    <div class="w-full text-center py-8">
                                        <p class="text-gray-500">No tienes productos favoritos aún.</p>
                                    </div>
                                `;
                            }
                        }
                    }
                } else {
                    console.error("Error al eliminar de favoritos");
                }
            } catch (error) {
                console.error("Error:", error);
            }
        });
    });
    
    // Hacer que los productos en el perfil y en la página de favoritos sean cliqueables
    const productCards = document.querySelectorAll(".product-card");
    productCards.forEach(card => {
        const productId = card.getAttribute("data-id") || 
                        card.querySelector(".remove-favorite-btn").getAttribute("data-id");
        
        card.addEventListener("click", function(e) {
            // Solo navegamos si el clic no fue en el botón de quitar favorito
            if (!e.target.closest(".remove-favorite-btn")) {
                window.location.href = `/Product/ProductDisplay/${productId}`;
            }
        });
    });
});