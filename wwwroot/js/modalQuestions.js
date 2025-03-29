document.addEventListener("DOMContentLoaded", function () {
    console.log("El script modalQuestions.js se está ejecutando");

    const modalOverlay = document.getElementById("modalQuestions-overlay");
    const modal = document.getElementById("modalQuestions");
    const cerrarModal = document.getElementById("cerrar-modalQuestions");
    const answerSection = document.getElementById("answerSection");
    const questionAnswerElement = document.getElementById("questionAnswer");
    
    // Referencias a elementos del modal
    const userNameElement = document.getElementById("userName");
    const questionTextElement = document.getElementById("questionText");
    const questionStatusElement = document.getElementById("questionStatus");
    const productNameElement = document.getElementById("productName");

    // Elementos del formulario (pueden ser null si el usuario no es vendedor)
    const answerForm = document.getElementById("answerForm");
    const questionIdInput = answerForm ? document.getElementById("questionId") : null;
    const answerFormInput = document.getElementById("answerFormInput");
    const submitAnswerBtn = document.getElementById("submitAnswerBtn");

    // Obtener todas las tarjetas de preguntas
    document.querySelectorAll(".tarjeta-question").forEach(tarjeta => {
        tarjeta.addEventListener("click", function (event) {
            // Obtener los datos de la pregunta desde los atributos data-
            const questionId = tarjeta.getAttribute("data-id");
            const userName = tarjeta.getAttribute("data-user");
            const productName = tarjeta.getAttribute("data-product");
            const questionText = tarjeta.getAttribute("data-question");
            const status = tarjeta.getAttribute("data-status");
            const answer = tarjeta.getAttribute("data-answer");

            // Insertar datos en el modal
            userNameElement.textContent = userName;
            questionTextElement.textContent = questionText;
            questionStatusElement.textContent = status;
            productNameElement.textContent = productName;
            
            // Si hay un formulario para responder (usuario es vendedor)
            if (answerForm) {
                questionIdInput.value = questionId;
            }

            // Lógica para mostrar/ocultar elementos según el estado
            if (status === "Answered") {
                // Mostrar la respuesta si ya está respondida
                questionAnswerElement.textContent = answer;
                answerSection.style.display = "block";
                
                // Ocultar el formulario si existe
                if (answerFormInput) {
                    answerFormInput.style.display = "none";
                }
                if (submitAnswerBtn) {
                    submitAnswerBtn.style.display = "none";
                }
            } else {
                // Ocultar la sección de respuesta
                answerSection.style.display = "none";
                
                // Mostrar el formulario si existe (usuario es vendedor)
                if (answerFormInput) {
                    answerFormInput.style.display = "block";
                }
                if (submitAnswerBtn) {
                    submitAnswerBtn.style.display = "inline-block";
                }
            }

            // Mostrar el modal
            modalOverlay.classList.remove("hidden");
            modalOverlay.classList.add("flex");
        });
    });

    // Cerrar el modal al hacer clic en "Cerrar"
    cerrarModal.addEventListener("click", function () {
        modalOverlay.classList.add("hidden");
        modalOverlay.classList.remove("flex");
        if (answerForm) {
            answerForm.reset(); // Limpiar el formulario si existe
        }
    });

    // Cerrar el modal si el usuario hace clic fuera del cuadro
    modalOverlay.addEventListener("click", function (event) {
        if (event.target === modalOverlay) {
            modalOverlay.classList.add("hidden");
            modalOverlay.classList.remove("flex");
            if (answerForm) {
                answerForm.reset(); // Limpiar el formulario si existe
            }
        }
    });
});