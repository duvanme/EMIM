document.getElementById('change-image-button').addEventListener('click', function () {
    document.getElementById('file-input').click();
});

document.getElementById('file-input').addEventListener('change', function (event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            // Cambiar la imagen de perfil
            document.getElementById('profile-image').src = e.target.result;
            // Cambiar la imagen en el navbar
            document.getElementById('navbar-user-image').src = e.target.result;
        };
        reader.readAsDataURL(file);
    }
});