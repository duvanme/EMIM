function updateStatus(status) {
    // Actualizar la barra de progreso
    const progressBar = document.getElementById('progress-bar');
    
    // Calcular el porcentaje basado en el estado (0%, 33.33%, 66.66%, 100%)
    let percentage;
    if (status === 1) percentage = '25%';
    else if (status === 2) percentage = '50%';
    else if (status === 3) percentage = '75%';
    else percentage = '100%';
    
    progressBar.style.width = percentage;
    
    // Actualizar los iconos y textos
    for (let i = 1; i <= 4; i++) {
        const icon = document.getElementById(`status-icon-${i}`);
        const text = document.getElementById(`status-text-${i}`);
        
        if (i <= status) {
            // Activar este estado y todos los anteriores
            icon.classList.remove('bg-gray-200');
            icon.classList.add('bg-green-500');
            text.classList.remove('text-gray-500');
            text.classList.add('text-green-500');
        } else {
            // Desactivar este estado y todos los siguientes
            icon.classList.remove('bg-green-500');
            icon.classList.add('bg-gray-200');
            text.classList.remove('text-green-500');
            text.classList.add('text-gray-500');
        }
    }
}