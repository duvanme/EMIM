// clearCart.js
document.addEventListener('DOMContentLoaded', function() {
    // Función para obtener el valor de una cookie por su nombre
    function getCookie(name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
        return null;
    }
    
    // Verificar si existe la cookie de limpieza del carrito
    if (getCookie('ClearCart') === 'true') {
        // Limpiar el carrito del localStorage
        localStorage.removeItem('cart');
        
        // Eliminar la cookie
        document.cookie = 'ClearCart=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
        
        // Si estás en la página del carrito, actualiza la visualización
        if (typeof renderCart === 'function') {
            renderCart();
        }
    }
});