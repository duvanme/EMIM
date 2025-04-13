// orders.js - Script para la funcionalidad de la página de órdenes
document.addEventListener('DOMContentLoaded', function() {
    // Obtener todas las tarjetas de órdenes
    const orderCards = document.querySelectorAll('.cursor-pointer');
    
    // Agregar evento de clic a cada tarjeta
    orderCards.forEach(card => {
        card.addEventListener('click', function() {
            // Obtener el número de orden del título (e.g., "Orden #12345")
            const orderTitle = this.querySelector('h3').textContent;
            const orderNumber = orderTitle.split('#')[1];
            
            // Redireccionar a la página de detalles de la orden (puramente visual)
            // En un entorno MVC real, esto debería ser una ruta adecuada
            window.location.href = `/Store/PurchaseStatus`;
        });
    });
});