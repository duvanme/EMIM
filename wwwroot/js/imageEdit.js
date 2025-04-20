document.addEventListener('DOMContentLoaded', function() {
    const fileInput = document.getElementById('ImageFile');
    const previewContainer = fileInput.parentElement.previousElementSibling;
    
    if (fileInput) {
        fileInput.addEventListener('change', function() {
            if (this.files && this.files[0]) {
                const reader = new FileReader();
                
                reader.onload = function(e) {
                    // Si ya existe un contenedor de vista previa, actualizarlo
                    if (previewContainer && previewContainer.classList.contains('mb-2')) {
                        const img = previewContainer.querySelector('img');
                        if (img) {
                            img.src = e.target.result;
                        }
                    } 
                    // Si no existe, crear uno nuevo
                    else {
                        const newPreview = document.createElement('div');
                        newPreview.classList.add('mb-2', 'border', 'rounded-lg', 'p-2', 'text-center');
                        newPreview.innerHTML = `
                            <img src="${e.target.result}" class="mx-auto w-24 h-24 sm:w-32 sm:h-32 md:w-40 md:h-40 object-cover rounded" />
                            <p class="mt-1 text-xs sm:text-sm text-gray-500">Vista previa</p>
                        `;
                        fileInput.parentElement.insertBefore(newPreview, fileInput);
                    }
                }
                
                reader.readAsDataURL(this.files[0]);
            }
        });
    }
});