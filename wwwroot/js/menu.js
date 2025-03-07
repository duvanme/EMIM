document.addEventListener("DOMContentLoaded", function () {
    const toggleMenu = document.getElementById("toggle-menu");
    const menu = document.getElementById("menu");
    const mainContainer = document.getElementById("main-container"); // Asegúrate de que este ID exista en tu HTML

    if (!toggleMenu || !menu) return;

    toggleMenu.addEventListener("click", () => {
        if (menu.classList.contains("hidden")) {
            // Mostrar menú con animación
            menu.classList.remove("hidden");
            setTimeout(() => {
                menu.classList.remove("opacity-0", "scale-95");
                menu.classList.add("opacity-100", "scale-100");
            }, 10);
        } else {
            // Ocultar menú con animación
            menu.classList.remove("opacity-100", "scale-100");
            menu.classList.add("opacity-0", "scale-95");
            setTimeout(() => menu.classList.add("hidden"), 300);
        }

        // Ajustar ancho del contenido principal si es necesario
        if (mainContainer) {
            mainContainer.classList.toggle("w-full");
            mainContainer.classList.toggle("md:w-4/5");
        }
    });
});
