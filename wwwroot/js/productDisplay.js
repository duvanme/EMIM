document.addEventListener('DOMContentLoaded', function() {
    console.log("DOM cargado, buscando estrellas...");
    
    const stars = document.querySelectorAll(".star");
    console.log("Estrellas encontradas:", stars.length);
    
    if (stars.length === 0) return;
    
    let selectedRating = 0;

    // Función para actualizar la visualización de las estrellas
    function updateStars(rating, isHover = false) {
        console.log(`Actualizando estrellas: ${rating} (hover: ${isHover})`);
        
        stars.forEach((star, index) => {
            // Obtener el valor de la estrella
            const starValue = parseInt(star.getAttribute("data-value"));
            
            // Si estamos en modo hover y el índice es menor que el rating
            // O si no estamos en modo hover y ya tenemos una selección
            if ((isHover && starValue <= rating) || (!isHover && starValue <= selectedRating)) {
                star.classList.remove("text-gray-400");
                star.classList.add("text-yellow-500");
            } else {
                star.classList.remove("text-yellow-500");
                star.classList.add("text-gray-400");
            }
        });
    }

    // Configurar eventos para cada estrella
    stars.forEach(star => {
        // Hover: muestra preview
        star.addEventListener("mouseenter", function() {
            const value = parseInt(this.getAttribute("data-value"));
            updateStars(value, true);
        });
        
        // Mouse sale: volver al estado seleccionado
        star.addEventListener("mouseleave", function() {
            updateStars(selectedRating, false);
        });
        
        // Click: seleccionar valoración
        star.addEventListener("click", function() {
            selectedRating = parseInt(this.getAttribute("data-value"));
            console.log(`Seleccionada calificación: ${selectedRating}`);
            updateStars(selectedRating, false);
            
            // Si tienes un campo oculto para el formulario
            const ratingInput = document.getElementById("rating-input");
            if (ratingInput) {
                ratingInput.value = selectedRating;
            }
        });
    });
});