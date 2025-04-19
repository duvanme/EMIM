
document.getElementById('burger-icon').addEventListener('click', function () {
    const menu = document.getElementById('exp-menu');

    // cambiar entre 'hidden' y 'flex'
    if (menu.classList.contains('hidden')) {
        menu.classList.remove('hidden');
        menu.classList.add('flex');
    } else {
        menu.classList.remove('flex');
        menu.classList.add('hidden');
    }

    // Cerrar cuando se hace clic fuera del menú
    document.addEventListener('click', function closeMenu(e) {
        if (!menu.contains(e.target) && e.target.id !== 'burger-icon') {
            menu.classList.remove('flex');
            menu.classList.add('hidden');
            document.removeEventListener('click', closeMenu);
        }
    });
});
