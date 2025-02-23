// Selección de estrellas

let selectedRating = 0;

const stars = document.querySelectorAll(".star");
stars.forEach(star => {
    star.addEventListener("click", function () {
        selectedRating = this.getAttribute("data-value");

        // Cambiar el color de las estrellas seleccionadas
        stars.forEach(s => {
            s.classList.remove("text-yellow-500");
            s.classList.add("text-gray-400");
        });

        for (let i = 0; i < selectedRating; i++) {
            stars[i].classList.remove("text-gray-400");
            stars[i].classList.add("text-yellow-500");
        }
    });
});

// Enviar la calificación
function submitRating() {
    const comment = document.getElementById("comment").value;

    if (selectedRating > 0 && comment.trim() !== "") {
        // Aquí puedes manejar el envío de datos al backend si es necesario
        console.log(`Calificación: ${selectedRating} estrellas`);
        console.log(`Comentario: ${comment}`);

        // Mostrar mensaje de confirmación
        document.getElementById("rating-message").classList.remove("hidden");
    } else {
        alert("Por favor selecciona una calificación y deja un comentario.");
    }
}
//Imagenes 
function swapImages(thumbnail) {
    // Obtén la imagen principal
    const mainImage = document.getElementById("main-image");

    // Guarda temporalmente el src de la imagen principal
    const tempSrc = mainImage.src;

    // Intercambia el src de la imagen principal con el de la miniatura
    mainImage.src = thumbnail.src;

    // Actualiza el src de la miniatura con el src de la imagen principal
    thumbnail.src = tempSrc;
}

//chat 
document.getElementById('send-button').addEventListener('click', function () {
    const messageInput = document.getElementById('message-input');
    const messageText = messageInput.value;

    if (messageText) {
        const messagesContainer = document.getElementById('messages');
        const newMessage = document.createElement('div');
        newMessage.textContent = messageText;
        newMessage.classList.add('bg-blue-100', 'p-2', 'rounded', 'mb-2');
        messagesContainer.appendChild(newMessage);
        messageInput.value = '';
        messagesContainer.scrollTop = messagesContainer.scrollHeight; // Desplazar hacia abajo
    }
});

document.getElementById('toggle-chat').addEventListener('click', function () {
    const chatContainer = document.getElementById('chat-container');
    chatContainer.classList.toggle('hidden'); // Alterna la clase 'hidden'
});