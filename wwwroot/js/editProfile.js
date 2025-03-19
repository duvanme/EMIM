document.addEventListener("DOMContentLoaded", async () => {
    try {
        const response = await fetch("GetUserProfile", {
            method: "GET",
            headers: { "Content-Type": "application/json" }
        }); // Llama al controlador

        if (!response.ok) throw new Error(`Error ${response.status}: ${response.statusText}`);


        const data = await response.json();

        const addressField = document.getElementById("Address");
        document.getElementById("Address").value = data?.address ?? "";
        const emailField = document.getElementById("Email");
        document.getElementById("Email").value = data?.email ?? "";
        const phoneField = document.getElementById("PhoneNumber");
        document.getElementById("PhoneNumber").value = data?.phoneNumber ?? "";
        const birthDateField = document.getElementById("BirthDate");
        document.getElementById("BirthDate").value = data?.birthDate ?? "";
        const imageProfile = document.getElementById("ImageProfile");
        document.getElementById("ImageProfile").src = data?.imageProfile || "img/";

        const userIdField = document.getElementById("user-id");
        document.getElementById("user-id").value = data?.id || "";

    } catch (error) {
        console.error("Failed to load profile:", error);
    }
    //window.onload = loadUserProfile;
});

//Guardar el perfil editado
document.getElementById("user-edit-form").addEventListener("submit", async function (event) {
    event.preventDefault();

    const formData = new FormData(this);
    const userId = document.getElementById("user-id").value; //Se obtiene el ID
    console.log("User ID obtained:", userId);
    if (!userId) {
        alert("Failed to get user ID.");
        return;
    } 
    // Asegúrate de que todos los campos se están añadiendo correctamente
    const userProfileData = {
        Id: userId,
        Address: formData.get("Address"),
        Email: formData.get("Email"),
        PhoneNumber: formData.get("PhoneNumber"),
        BirthDate: formData.get("BirthDate"),
        ImageProfile: document.getElementById("ImageProfile").src || "" //Usa la URL de la imagen
    };

    try{
        const response = await fetch('UpdateUserProfile', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(userProfileData)
        });
        if (!response.ok) {
            const error = await response.json();
            alert("Error: ${error.message}.");
            return;
        } 
        alert("Profile updated successfully.");
        //Redirige al perfil    
        //window.location.href = "/UserProfile";
    }catch{
        console.error("Error updating profile:", error);
        alert("Unexpected error updating profile.");
    }
});


//Abiri los documentos 
document.getElementById('change-image-button').addEventListener('click', function () {
    document.getElementById('file-input').click();
});

//Subir imagen de perfil
document.getElementById("file-input").addEventListener("change", async (event) => {
    const file = event.target.files[0];
    if (!file) return;

    // Previsualizar la imagen antes de enviarla
    const reader = new FileReader();
    reader.onload = function (e) {
        document.getElementById("ImageProfile").src = e.target.result; //Mostrar imagen en el formulario
    };
    reader.readAsDataURL(file); //Convierte la imagen a base64 para mostrarla


    const formData = new FormData();
    formData.append("file", file);

    try {
        const response = await fetch("/api/user/UploadProfileImage", {
            method: "POST",
            body: formData
        });

        if (!response.ok) throw new Error("Error uploading image");

        const data = await response.json();
        document.getElementById("ImageProfile").src = data.ImageUrl;
        ducument.getElementById("ImageProfileUrl").value = data.ImageUrl;
    } catch (error) {
        console.error("Error uploading image:", error);
    }
});