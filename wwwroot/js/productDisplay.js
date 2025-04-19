document.addEventListener('DOMContentLoaded', function () {
    // Funcionalidad de carrito
    const addToCartButton = document.getElementById('add-to-cart-btn');
    if (addToCartButton) {
        // Eliminar escuchadores anteriores si existen
        addToCartButton.removeEventListener('click', handleAddToCart);

        // Asignar el nuevo escuchador
        addToCartButton.addEventListener('click', handleAddToCart);
    }

    // Código de calificación con estrellas
    const stars = document.querySelectorAll(".star");

    if (stars.length === 0) return;

    let selectedRating = 0;

    function updateStars(rating, isHover = false) {
        stars.forEach((star, index) => {
            const starValue = parseInt(star.getAttribute("data-value"));

            if ((isHover && starValue <= rating) || (!isHover && starValue <= selectedRating)) {
                star.classList.remove("text-gray-400");
                star.classList.add("text-yellow-500");
            } else {
                star.classList.remove("text-yellow-500");
                star.classList.add("text-gray-400");
            }
        });
    }
    // Eventos de calificación con estrellas
    stars.forEach(star => {
        star.addEventListener("mouseenter", function () {
            const value = parseInt(this.getAttribute("data-value"));
            updateStars(value, true);
        });

        star.addEventListener("mouseleave", function () {
            updateStars(selectedRating, false);
        });

        star.addEventListener("click", function () {
            selectedRating = parseInt(this.getAttribute("data-value"));
            updateStars(selectedRating, false);

            const ratingInput = document.getElementById("rating-input");
            if (ratingInput) {
                ratingInput.value = selectedRating;
            }
        });
    });
});
function handleAddToCart(e) {
    e.preventDefault();
    // Tu lógica para agregar al carrito
    // Obtener detalles del producto de manera más robusta
    const productId = document.querySelector('input[name="ProductId"]')?.value || Date.now().toString();
    const productName = document.querySelector('h1.text-2xl').textContent.trim();

    // Capturar el precio original sin formateo
    const productPriceElement = document.querySelector('h2.text-3xl');
    const productPrice = productPriceElement ? productPriceElement.textContent.trim() : '$0';

    // Solución para capturar la imagen
    const productImageElement = document.querySelector('section img[data-image]');
    const productImage = productImageElement
        ? (productImageElement.getAttribute('data-image') || productImageElement.src)
        : '/images/default-product.png';

    const productQuantity = parseInt(document.querySelector('input[name="ProductQuantity"]')?.value) || 0;

    // Añadir al carrito de almacenamiento local
    agregarAlCarrito({
        id: productId,
        name: productName,
        price: productPrice,  // Mantener el formato original
        image: productImage,
        itemQuantity: 1,
        quantity: productQuantity
    });

    mostrarNotificacionCarrito(productName);
}
function agregarAlCarrito(producto) {
    // Asegurar que la imagen exista
    if (!producto.image) {
        producto.image = '/images/default-product.png';
    }

    // Extraer el valor numérico del precio
    let precioTexto = producto.price.trim().replace('$', '').replace(/\s/g, '');
    precioTexto = precioTexto.replace(/\./g, '');
    if (precioTexto.includes(',')) {
        precioTexto = precioTexto.replace(',', '.');
    }

    // Guardar tanto el precio formateado como el valor numérico
    producto.priceNumeric = parseFloat(precioTexto);

    let carrito = JSON.parse(localStorage.getItem('cart')) || [];

    const indiceProductoExistente = carrito.findIndex(item => item.id === producto.id);

    if (indiceProductoExistente > -1) {
        // Aumentar cantidad si el producto existe
        if (carrito[indiceProductoExistente].itemQuantity < carrito[indiceProductoExistente].quantity) {
            carrito[indiceProductoExistente].itemQuantity += 1;
        }
    } else {
        // Añadir nuevo producto al carrito
        carrito.push(producto);
    }
    localStorage.setItem('cart', JSON.stringify(carrito));
}
function mostrarNotificacionCarrito(nombreProducto) {
    Swal.fire({
        title: "¡Producto añadido!",
        text: `${nombreProducto} se agregó al carrito.`,
        icon: "success",
        timer: 2000,
        showConfirmButton: false,
        toast: true,
        position: "top-end",
        background: "#1f2937",
        color: "#fff"
    });
}