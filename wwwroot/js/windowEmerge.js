const alerta = document.querySelector("#alerta");
const botonAceptar = document.querySelector("#aceptarMensaje");


    if (alerta && botonAceptar) {
        console.log("Funciona")
        if (sessionStorage.getItem("alertShown")) {
            alerta.style.display = "none"; // Oculta la alerta si ya se mostró
        } else {
            alerta.style.display = "flex"; // Muestra la alerta
            sessionStorage.setItem("alertShown", "true"); // Marca que la alerta se mostró
        }

        botonAceptar.addEventListener("click", function () {
            alerta.style.display = "none"; // Oculta la alerta
            sessionStorage.removeItem("alertShown"); // Limpia el estado al aceptar
            window.location.href = "/Account/Login"; // Redirige al login
        });
    }
