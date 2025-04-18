// Al cargar la página, animamos el círculo y la cruz
window.addEventListener("DOMContentLoaded", () => {
    const circle = document.getElementById("circle");
    const cross = document.getElementById("cross");

    // Animar el círculo
    setTimeout(() => {
        circle.classList.remove("scale-0");
        circle.classList.add("scale-100");
    }, 100);

    // Dibujar la cruz
    setTimeout(() => {
        cross.style.strokeDashoffset = "0";
    }, 500);
});