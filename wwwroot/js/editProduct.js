document.getElementById('edit-image-button').addEventListener('click', function (event) {
    event.preventDefault(); // Prevenir el comportamiento por defecto del botón
    document.getElementById('image-modal').classList.remove('hidden'); // Muestra el modal
    document.getElementById('image-input').style.display = 'block'; // Forzar visibilidad del input
});

document.getElementById('close-modal').addEventListener('click', function () {
    document.getElementById('image-modal').classList.add('hidden'); // Oculta el modal
});

// Cerrar el modal al hacer clic fuera de él
document.getElementById('image-modal').addEventListener('click', function (event) {
    if (event.target === this) {
        this.classList.add('hidden'); // Oculta el modal
    }
});

// Función para eliminar una imagen
function removeImage(button) {
    const imageContainer = button.parentElement; // Obtiene el contenedor de la imagen
    imageContainer.remove(); // Elimina el contenedor de la imagen
}

// Agregar nueva imagen
document.getElementById('image-input').addEventListener('change', function (event) {
    const files = event.target.files;
    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const reader = new FileReader();
        reader.onload = function (e) {
            const imageGallery = document.getElementById('image-gallery');
            const newImageDiv = document.createElement('div');
            newImageDiv.className = 'relative m-2';
            newImageDiv.innerHTML = `
                <img src="${e.target.result}" alt="Imagen" class="w-32 h-32 object-cover rounded">
                <button class="absolute top-0 right-0 bg-red-500 text-black rounded-full p-1" onclick="removeImage(this)">X</button>
            `;
            imageGallery.appendChild(newImageDiv); // Agrega la nueva imagen al contenedor
        };
        reader.readAsDataURL(file); // Lee la imagen como URL
    }
});
