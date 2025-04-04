document.addEventListener("DOMContentLoaded", function () {
    const likeButtons = document.querySelectorAll(".like-button");

    likeButtons.forEach(button => {
        button.addEventListener("click", function () {
            const icon = this.querySelector("svg"); // Encuentra el SVG dentro del botón

            if (icon.classList.contains("text-red-200")) {
                icon.classList.remove("text-red-200");
                icon.classList.add("text-red-500", "fill-red-500"); // Agregar fill-red-500
            } else {
                icon.classList.remove("text-red-500", "fill-red-500");
                icon.classList.add("text-red-200");
            }
        });
    });
});
