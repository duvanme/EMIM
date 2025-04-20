document.addEventListener("DOMContentLoaded", function () {
    const likeButtons = document.querySelectorAll(".like-button");

    // Función para manejar la acción de like/unlike
    async function toggleFavorite(button, productId) {
        const icon = button.querySelector("svg");
        const isFavorite = !icon.classList.contains("text-red-500");
        
        try {
            // URL y método según si agregamos o quitamos de favoritos
            const url = isFavorite 
                ? `/api/favorites/add/${productId}` 
                : `/api/favorites/remove/${productId}`;
            const method = isFavorite ? 'POST' : 'DELETE';
            
            const response = await fetch(url, {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                    'X-Requested-With': 'XMLHttpRequest'
                },
                credentials: 'include'
            });
            
            if (response.ok) {
                // Cambiamos la apariencia según corresponda
                if (isFavorite) {
                    icon.classList.remove("text-blue-600");
                    icon.classList.add("text-red-500", "fill-red-500");
                } else {
                    icon.classList.remove("text-red-500", "fill-red-500");
                    icon.classList.add("text-blue-600");
                }
            } else {
                // Si no está autenticado, redirigir al login
                if (response.status === 401) {
                    window.location.href = '/Account/Login';
                } else {
                    const errorData = await response.json();
                    console.error("Error:", errorData.message);
                }
            }
        } catch (error) {
            console.error("Error al procesar la solicitud:", error);
        }
    }

    likeButtons.forEach(button => {
        button.addEventListener("click", function(e) {
            e.stopPropagation(); // Evitar que el clic se propague a la tarjeta
            const productId = this.getAttribute("data-id");
            toggleFavorite(this, productId);
        });
    });

    // Verificar estado inicial de favoritos para usuarios autenticados
    async function checkFavoriteStatus() {
        const productCards = document.querySelectorAll(".product-card");
        
        for (const card of productCards) {
            const productId = card.getAttribute("data-product-id");
            const likeButton = card.querySelector(".like-button");
            
            if (likeButton) {
                try {
                    const response = await fetch(`/api/favorites/check/${productId}`, {
                        method: 'GET',
                        headers: {
                            'X-Requested-With': 'XMLHttpRequest'
                        },
                        credentials: 'include'
                    });
                    
                    if (response.ok) {
                        const data = await response.json();
                        const icon = likeButton.querySelector("svg");
                        
                        if (data.isFavorite) {
                            icon.classList.remove("text-blue-600");
                            icon.classList.add("text-red-500", "fill-red-500");
                        }
                    }
                } catch (error) {
                    console.error("Error al verificar favoritos:", error);
                }
            }
        }
    }
    
    // Solo verificar si hay botones de like en la página
    if (likeButtons.length > 0) {
        checkFavoriteStatus();
    }
});