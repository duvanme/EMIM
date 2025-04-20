// Función para abrir el modal
function openModal(id) {
    document.getElementById('modal-' + id).classList.remove('hidden');
}

// Función para cerrar el modal
function closeModal(id) {
    document.getElementById('modal-' + id).classList.add('hidden');
}

document.getElementById('help-form').addEventListener('submit', async function (e) {
    e.preventDefault();

    const data = {
        UserName: document.getElementById('nombre').value,
        Email: document.getElementById('correo').value,
        Subject: document.getElementById('asunto').value,
        Message: document.getElementById('message').value
    };

    const response = await fetch('/HelpQuestion/Submit', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });

    if (response.ok) {
        alert("Tu pregunta fue enviada con éxito");
        document.getElementById('help-form').reset();
    } else {
        alert("Hubo un problema al enviar tu pregunta.");
    }
});