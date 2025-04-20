document.addEventListener('DOMContentLoaded', function() {
    // Evento para filtrar órdenes por estado
    const statusFilter = document.getElementById('statusFilter');
    statusFilter.addEventListener('change', function() {
        applyFilters();
    });

    // Evento para ordenar
    const sortOrder = document.getElementById('sortOrder');
    sortOrder.addEventListener('change', function() {
        applyFilters();
    });

    function applyFilters() {
        const status = statusFilter.value;
        const sort = sortOrder.value;
       
        window.location.href = `/Store/Purchase?statusFilter=${status}&sortOrder=${sort}`;
    }
});