// wwwroot/js/purchaseStatus.js
document.addEventListener('DOMContentLoaded', function() {
    // Solo mostrar opciones de cambio de estado para administradores/vendedores
    const isAdmin = document.body.classList.contains('admin-user') || document.body.classList.contains('vendor-user');
    
    if (isAdmin) {
        // Agregar eventos click a los iconos de estado
        const statusIcons = document.querySelectorAll('[data-status]');
        statusIcons.forEach(icon => {
            icon.addEventListener('click', function() {
                const status = this.getAttribute('data-status');
                const statusId = parseInt(this.getAttribute('data-status-id'));
                updateOrderStatus(status, statusId);
            });
        });
    } else {
        // Si no es admin, quitar el cursor pointer
        const statusIcons = document.querySelectorAll('[data-status]');
        statusIcons.forEach(icon => {
            icon.classList.remove('cursor-pointer');
        });
    }

    function updateOrderStatus(status, statusId) {
        const orderId = document.getElementById('orderId').value;
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        
        // Pedir confirmación antes de cambiar el estado
        if (confirm(`¿Estás seguro de cambiar el estado a "${status}"?`)) {
            fetch('/Order/UpdateOrderStatus', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'RequestVerificationToken': token
                },
                body: `orderId=${orderId}&status=${status}`
            })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    // Actualizar la UI para reflejar el nuevo estado
                    updateUI(statusId);
                    alert('Estado actualizado correctamente');
                } else {
                    alert('Error al actualizar el estado: ' + data.message);
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Ocurrió un error al actualizar el estado');
            });
        }
    }

    function updateUI(statusId) {
        // Actualizar la barra de progreso
        const progressBar = document.getElementById('progress-bar');
        progressBar.style.width = `${statusId * 25}%`;

        // Actualizar los iconos y textos
        for (let i = 1; i <= 4; i++) {
            const icon = document.getElementById(`status-icon-${i}`);
            const text = document.getElementById(`status-text-${i}`);
            
            if (i <= statusId) {
                // Si es el estado actual o anterior, marcarlo como completado
                icon.classList.remove('bg-gray-200');
                icon.classList.add('bg-green-500');
                text.classList.remove('text-gray-500');
                text.classList.add('text-green-500');
            } else {
                // Si es un estado posterior, marcarlo como pendiente
                icon.classList.remove('bg-green-500');
                icon.classList.add('bg-gray-200');
                text.classList.remove('text-green-500');
                text.classList.add('text-gray-500');
            }
        }
    }
});