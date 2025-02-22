const toggleMenu = document.getElementById('toggle-menu');
        const menuContainer = document.querySelector('.menu-container');

        toggleMenu.addEventListener('click', () => {
            menuContainer.classList.toggle('hidden');
            mainContainer.classList.toggle('w-full');
            mainContainer.classList.toggle('md:w-4/5');
        });