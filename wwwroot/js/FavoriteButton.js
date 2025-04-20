document.addEventListener("DOMContentLoaded", function () {
    console.log("DOM Content Loaded");
    
    const favoriteBtn = document.getElementById("add-to-favorite-btn");
    console.log("Botón favoritos:", favoriteBtn);

    if (favoriteBtn) {
        favoriteBtn.addEventListener("click", async function (e) {
            console.log("Botón clickeado");
            e.preventDefault(); // Prevenir comportamiento por defecto si es necesario
            
            const productId = this.getAttribute("data-id");
            console.log("Product ID:", productId);
            
            const isFavorite = this.classList.contains("bg-red-500");
            console.log("Es favorito:", isFavorite);

            try {
                const url = isFavorite
                    ? `/api/favorites/remove/${productId}`
                    : `/api/favorites/add/${productId}`;
                const method = isFavorite ? 'DELETE' : 'POST';
                
                console.log("URL:", url, "Método:", method);

                const response = await fetch(url, {
                    method: method,
                    headers: {
                        'Content-Type': 'application/json',
                        'X-Requested-With': 'XMLHttpRequest'
                    },
                    credentials: 'include'
                });
                
                console.log("Respuesta status:", response.status);
                const responseData = await response.json();
                console.log("Respuesta datos:", responseData);

                if (response.ok) {
                    // Si la respuesta es exitosa, actualizamos la UI
                    if (isFavorite) {
                        // Cambiar a "Añadir a favoritos"
                        this.classList.remove("bg-red-500");
                        this.classList.add("bg-[#13678A]");
                        this.innerHTML = `
                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor"
                                 class="h-5 w-5 mr-2">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                      d="M4 6a4 4 0 000 5.6L12 21l8-9.4a4 4 0 10-5.6-5.6L12 7l-2.4-2.4A4 4 0 004 6z" />
                            </svg>
                            Añadir a favoritos
                        `;
                    } else {
                        // Cambiar a "Quitar de favoritos"
                        this.classList.remove("bg-[#13678A]");
                        this.classList.add("bg-red-500");
                        this.innerHTML = `
                            <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 24 24" stroke="currentColor"
                                 class="h-5 w-5 mr-2">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                      d="M4 6a4 4 0 000 5.6L12 21l8-9.4a4 4 0 10-5.6-5.6L12 7l-2.4-2.4A4 4 0 004 6z" />
                            </svg>
                            Quitar de favoritos
                        `;
                    }
                } else if (response.status === 401) {
                    // Redirigir al login si no está autenticado
                    window.location.href = '/Account/Login';
                } else {
                    console.error("Error en la respuesta:", response.statusText);
                    alert("Hubo un problema al procesar tu solicitud.");
                }
            } catch (error) {
                console.error("Error:", error);
                alert("Ocurrió un error al procesar tu solicitud. Por favor, inténtalo de nuevo.");
            }
        });
    } else {
        console.warn("Botón de favoritos no encontrado en la página");
    }
});