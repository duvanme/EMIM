const btnPersonal = document.getElementById('btn-personal');
const btnEmprendedor = document.getElementById('btn-emprendedor');
const personalForm = document.getElementById('personal-form');
const emprendedorForm = document.getElementById('emprendedor-form');
const personalImage = document.getElementById('personal-image');
const emprendedorImage = document.getElementById('emprendedor-image');

document.getElementById('formulario').addEventListener('submit', (e) => {
    e.preventDefault();
    // Manejar el envío del formulario aquí
});

btnPersonal.onclick = () => {
    // Cambiar estilos de los botones
    btnPersonal.classList.add('bg-gray-600', 'text-black');
    btnEmprendedor.classList.remove('bg-gray-600', 'text-black');
    // btnEmprendedor.classList.add('text-gray-600');

    // Mostrar formulario personal
    personalForm.classList.remove('hidden');
    emprendedorForm.classList.add('hidden');
    // Cambiar imagen a la de personal
    personalImage.classList.remove('hidden');
    emprendedorImage.classList.add('hidden');
};

btnEmprendedor.onclick = () => {
    // Cambiar estilos de los botones
    btnEmprendedor.classList.add('bg-gray-600', 'text-black');
    btnPersonal.classList.remove('bg-gray-600', 'text-black');
    // btnPersonal.classList.add('text-gray-600');

    // Mostrar formulario emprendedor
    emprendedorForm.classList.remove('hidden');
    personalForm.classList.add('hidden');
    // Cambiar imagen a la de emprendedor
    emprendedorImage.classList.remove('hidden');
    personalImage.classList.add('hidden');
};