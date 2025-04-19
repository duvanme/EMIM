// Al cargar la página, animamos el círculo y el check
window.addEventListener("DOMContentLoaded", () => {
    const circle = document.getElementById("circle");
    const check = document.getElementById("check");
    check.style.strokeDashoffset = "0";

    // Escalar el círculo
    setTimeout(() => {
        circle.classList.remove("scale-0");
        circle.classList.add("scale-100");
    }, 100); // ligero delay

    // Dibujar el check
    setTimeout(() => {
        check.style.strokeDashoffset = "0";
    }, 500); // después del círculo
});