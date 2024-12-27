const toggleMenu = document.getElementById('toggle-menu');
        const menuContainer = document.querySelector('.menu-container');

        toggleMenu.addEventListener('click', () => {
            menuContainer.classList.toggle('hidden');
        });