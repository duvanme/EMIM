function renderCart() {
    cartContainer.innerHTML = '';

    // Obtener carrito del almacenamiento local
    const cart = JSON.parse(localStorage.getItem('cart')) || [];

    // Mostrar mensaje de carrito vacío si no hay productos
    if (cart.length === 0) {
        cartEmptyMessage.style.display = 'block';
        totalItemsSpan.textContent = '0 items';
        subtotalSpan.textContent = '$0';
        totalPriceSpan.textContent = '$0';
        return;
    }

    // Ocultar mensaje de carrito vacío
    cartEmptyMessage.style.display = 'none';

    let totalItems = 0;
    let totalPrice = 0;

    cart.forEach((producto, indice) => {

        // SOLUCIÓN COMPLETA: Convertir precio a número para pesos colombianos
        let precioTexto = producto.price;

        // Primero, eliminar el símbolo de pesos y espacios
        precioTexto = precioTexto.trim().replace('$', '').replace(/\s/g, '');

        // Reemplazar puntos (separadores de miles) y dejar solo dígitos
        // Para formato colombiano donde 25.000,00 significa veinticinco mil pesos
        precioTexto = precioTexto.replace(/\./g, '');

        // Reemplazar coma por punto para decimales (si hay coma decimal)
        if (precioTexto.includes(',')) {
            precioTexto = precioTexto.replace(',', '.');
        }

        // Ahora convertir a número
        const precioNumerico = parseFloat(precioTexto);

        if (isNaN(precioNumerico)) {
            console.error('Error al convertir precio:', producto.price);
        }

        const totalProducto = precioNumerico * producto.itemQuantity;

        totalItems += producto.itemQuantity;
        totalPrice += totalProducto;

        const imgElement = document.createElement('img');
        imgElement.src = producto.image || '/images/default-product.png';


        imgElement.style.width = '64px';      // Ancho fijo
        imgElement.style.height = '64px';     // Alto fijo
        imgElement.style.objectFit = 'cover'; // Cubrir todo el contenedor
        imgElement.style.objectPosition = 'center'; // Centrar imagen
        imgElement.style.borderRadius = '0.375rem'; // Bordes redondeados
        imgElement.style.margin = '0';        // Sin márgenes
        imgElement.style.display = 'block';   // Bloque para control preciso

        imgElement.onerror = function () {
            this.src = '/images/default-product.png';
        };

        const elementoCarrito = document.createElement('div');
        elementoCarrito.classList.add('grid', 'grid-cols-3', 'py-3', 'items-center');
        elementoCarrito.setAttribute('data-product-id', producto.id);

        elementoCarrito.innerHTML = `
    <div class="flex items-center gap-3 items-center">
        ${imgElement.outerHTML}
        <span class="ml-3 product-name">${producto.name}</span>
        <input type="hidden" class="product-id" value="${producto.id}">
    </div>
    <div class="flex justify-center items-center gap-2">
        <button class="decrease-btn border ml-10 mr-[-15px] px-2 py-1">-</button>
        <span class="quantity px-4 quantity">${producto.itemQuantity}</span>
        ${producto.itemQuantity < producto.quantity
                ? `<button class="increase-btn border ml-[-15px] px-2 py-1">+</button>`
                : ''}
    </div>
    <div class="flex justify-end items-center gap-3">
        <span class="price">${producto.price}</span>
        <button class="remove-btn text-red-600 font-bold">✖</button>
    </div>
`;

        cartContainer.appendChild(elementoCarrito);
    });

    // Costo de envío
    const costoEnvio = 10000;
    const precioTotal = totalPrice + costoEnvio;
    

    // Actualizar elementos de resumen
    totalItemsSpan.textContent = `${totalItems} items`;
    subtotalSpan.textContent = formatearPrecioCOP(totalPrice);
    totalPriceSpan.textContent = formatearPrecioCOP(precioTotal);

    agregarEventosCarrito();
}

// Función para formatear precios en formato de pesos colombianos
function formatearPrecioCOP(numero) {
    // Asegurarse de que es un número y redondearlo (los pesos colombianos no suelen usar decimales)
    numero = Math.round(Number(numero));
    // Formatear con separadores de miles usando toLocaleString
    return '$' + numero.toLocaleString('es-CO');
}


function agregarEventosCarrito() {
    // Reducir cantidad
    document.querySelectorAll('.decrease-btn').forEach(boton => {
        boton.addEventListener('click', function () {
            const contenedorProducto = this.closest('[data-product-id]');
            const idProducto = contenedorProducto.getAttribute('data-product-id');
            actualizarCantidad(idProducto, -1);
        });
    });

    // Aumentar cantidad
    document.querySelectorAll('.increase-btn').forEach(boton => {
        boton.addEventListener('click', function () {
            const contenedorProducto = this.closest('[data-product-id]');
            const idProducto = contenedorProducto.getAttribute('data-product-id');
            actualizarCantidad(idProducto, 1);       
        });
    });

    // Eliminar producto
    document.querySelectorAll('.remove-btn').forEach(boton => {
        boton.addEventListener('click', function () {
            const contenedorProducto = this.closest('[data-product-id]');
            const idProducto = contenedorProducto.getAttribute('data-product-id');
            eliminarDelCarrito(idProducto);
        });
    });
}

function actualizarCantidad(idProducto, cambio) {
    let carrito = JSON.parse(localStorage.getItem('cart')) || [];
    const indiceProducto = carrito.findIndex(item => item.id === idProducto);

    if (indiceProducto > -1) {
        carrito[indiceProducto].itemQuantity += cambio;

        // Eliminar si la cantidad llega a 0
        if (carrito[indiceProducto].itemQuantity <= 0) {
            carrito.splice(indiceProducto, 1);
        }

        localStorage.setItem('cart', JSON.stringify(carrito));
        renderCart();
    }
}

function eliminarDelCarrito(idProducto) {
    let carrito = JSON.parse(localStorage.getItem('cart')) || [];
    const carritoActualizado = carrito.filter(item => item.id !== idProducto);

    localStorage.setItem('cart', JSON.stringify(carritoActualizado));
    renderCart();
}

// Vaciar carrito
emptyCartButton?.addEventListener('click', function () {
    localStorage.removeItem('cart');
    renderCart();
});

// Funcionalidad de checkout (placeholder)


// Renderizar carrito inicial
renderCart();