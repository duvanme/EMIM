document.getElementById('image-upload').addEventListener('change', function (event) {
    const previewContainer = document.getElementById('image-preview');
    // previewContainer.innerHTML = ''; // Limpiar el contenedor antes de agregar nuevas imágenes
    previewContainer.classList.add('relative');

    const files = event.target.files;

    if (files.length > 0) {
        Array.from(files).forEach((file, index) => {
            const reader = new FileReader();
            reader.onload = function (e) {
                const img = document.createElement('img');
                img.src = e.target.result;
                img.classList.add('absolute', 'w-28', 'h-28', 'object-cover', 'rounded-lg', 'shadow-lg');
                img.style.top = `${index * 5}px`; // Para que se superpongan un poco
                img.style.left = `${index * 5}px`; 

                previewContainer.appendChild(img);
            };
            reader.readAsDataURL(file);
        });
    } else {
        previewContainer.innerHTML = '<input type="file" id="image-upload" multiple accept="image/*" class="hidden">';
    }
});