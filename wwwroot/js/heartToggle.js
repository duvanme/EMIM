document.addEventListener("DOMContentLoaded", function () {
    const likeButtons = document.querySelectorAll(".like-button");

    likeButtons.forEach(button => {
        button.addEventListener("click", function () {
            const icon = this.querySelector("svg"); // Encuentra el SVG dentro del botón

            if (icon.classList.contains("text-blue-200")) {
                icon.classList.remove("text-blue-200");
                icon.classList.add("text-blue-500", "fill-blue-500"); // Agregar fill-red-500
            } else {
                icon.classList.remove("text-blue-500", "fill-blue-500");
                icon.classList.add("text-blue-200");
            }
        });
    });
});
