# **EMIM - README**  

## **Tabla de Contenido**  
1. [Resumen](#resumen)  
2. [Características](#raracterísticas)  
3. [Getting Started](#getting-started)  
   - [Prerequisites](#prerequisites)  
   - [Instalación](#instalación)  
   - [Configuration](#configuration)  
4. [Usage](#usage)  
   - [Para Compradores](#para-compradores)  
   - [Para Tiendas](#para-tiendas)  
   - [Para Administradores](#para-administradores)  
5. [Seguridad](#security)  
6. [Integración De Pago](#Integración-de-pago)  
7. [Para contribuciones](#para-contribuciones)  
8. [Licencia](#Licencia)  
9. [soporte y contacto](#soporte-y-contacto)  

---

## **Resumen**  

EMIM es un proyecto de Ecommerce que está diseñado para proporcionar una solución robusta y escalable para los minoristas en línea, ofreciendo una experiencia de compra sin fisuras para los clientes y un sistema de gestión eficiente para los propietarios de tiendas.

Construído con escalabilidad en mente, EMIM soporta multiples tiedas, transacciones seguras y un diseño responsivo para que pueda usarse tanto en computadores como en dispositivos móviles. 

---

## **Características**  

### **Características Principales**  
✅ **Gestión De Usuarios** – Registro, Inicio de sesión, perfiles, y acceso basado en roles  (Usuario, Tienda, Admin).  

✅ **Listado De Productos** – Las tiendas puedan añadir, editar y administrar productos con imágenes, descripción y categorías.

✅ **Búsqueda & Filtros** – Búsqueda avanzada con filtros (Precio, categoría,nombre de la tienda, etc.).  

✅ **Carrito De Compras & Pago** – Los usuarios pueden añadir productos y realizar un pago seguro.

✅ **Gestión De Order** – Hacer seguimiento de órdenes, historial de órdenes, and gestión del estado de la orden (Pending, Shipped, Delivered, Cancelled).  

✅ **Reviews & Ratings** – Buyers can leave feedback on products and sellers.  

✅ **Admin Dashboard** – Manage users, products, orders, and site settings.  

### **Características Advanzadas**  
🔹 **Soporte de multiples tiendas** – Cada tienda tiene su propio perfil.  

🔹 **Integración De Pasarela De Pagos** – Suporte de pagos a través de Stripe.  

🔹 **Notificaciones** – Para registro, compra y ayuda que llegan directamente al correo electrónico registrado en EMIM.

🔹 **Sistema De Mensajes** – Usuarios y tiendas pueden comunicarse entre ellos a través de un Q&A en los productos.  

---

## **Getting Started**  

### **Prerequisitos**  
- **Backend**: Dotnet 8.0.0 / ASP.NET Core MVC  
- **Frontend**: Tailwind CSS y Vanilla Javascript  
- **Base De Datos**: SQL Server 2019  
- **Servidor**: NA
- **Procesador De Pagos**: Stripe 

### **Instalación**  
#### **Configuración De Backend**  
1. Clonar el repositorio:  
   ```bash
   git clone https://github.com/duvanme/EMIM.git
   ```  
2. Configuración de la base de datos: 

    2.1. Realizar la migración con el siguiente comando:
   ```bash
   Add-Migration InitialMigration  # Si se usa Nuget Package Manager
   # O
   dotnet ef migrations add <NombreDeLaMigración>  # Si se realiza la migración con Dotnet EF
   ```  
   2.2. Realizar la actualización de la base de datos
   ```bash
   update-database # Si se usa Nuget Package Manager
   #O
   dotnet ef database update # Si se realiza la migración con Dotnet EF
   ```
   

#### **Configuracion Del Frontend**  
1. Instalación de dependencias:  
   ```bash
   npm install
   ```  

### **Configuración**  

Actualizar la siguiente information en el `appsetting.json`

```json
{
  "ConnectionStrings": {
    "EMIMDB": "Server=YOUR_SERVER;Database=EMIM_DB;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "AdminUser": {
    "Email": "admin@example.com",
    "Password": "CHANGE_THIS_TO_A_STRONG_PASSWORD"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "Your App Name",
    "SenderEmail": "your-email@gmail.com",
    "UserName": "your-email@gmail.com",
    "Password": "YOUR_APP_PASSWORD_OR_OAUTH_TOKEN",
    "UseSSL": true
  }
}
```
1. **Conexión a la base de datos:** Reemplaza YOUR_SERVER con la instancia del servidor de SQL
2. **Credenciales de Admin:** Cambiar en producción
3. **Configuración de correo electrónico:** Requiere Gmail App Password y para dominions diferentes, actualizar `SmtpServer` y `Port`
---

## **Uso**  

### **Para Compradores**  
1. **Búsqueda de Productos** – Uso de filtros y categorias para buscar productos.  
2. **Añadir Al Carrito De Compras** – Se selecciona la cantidad y se añade al carrito.  
3. **Pasarela De Pagos** – Paga de forma segura con Stripe.  
4. **Gestión De Órdenes** – Estado de la órden e historial de órdenes.

### **Para tiendas**  
1. **Creación De Tienda** – Aplicación y venta de productos
2. **Publcación De Productos** – Para añadir detalle de producto, imágenes y precio .  
3. **Gestión De Ordenes** – Para actualizar el estado de la órden y su entrega.  


### **Para Administradores**  
1. **Creación De Tiendas** – Para Revisar solicitudes de tiendas, aceptar o rechazar
2. **Gestión De Usuarios** – Se aprueban nuevos usuarios y se manejan permisos.  
3. **Ayuda y Contactos** – Para responder preguntas relacionadas con el funcionamiento de la plataforma

---

## **Seguridad**  
- **Políticas de seguridad fuertes** 
```csharp
options.Password.RequireNonAlphanumeric = true;
options.Password.RequiredLength = 12; // Mínimo 12 caracteres
options.Password.RequireUppercase/Lowercase/Digit = true;
```
- **Correo electrónico único es requerido** 
```csharp
options.User.RequireUniqueEmail = true;
```
- **Gestión De Sesiones**
```csharp
services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true; // Prevents XSS access
});
```
- **Protección CSRF** – Automáticamente activada en ASP.NET Core MVC via `[validateAntiForgeryToken]`.  
- **Seguridad De Cookies**
```csharp
options.Cookie.IsEssential = true;
```
- **Inyección De Dependencias Para Servicios**
```csharp
services.AddScoped<IAccountService, AccountService>();
```

- **Autorización Basada En Roles**
```csharp
services.AddIdentity<User, IdentityRole>() // Role management
        .AddEntityFrameworkStores<EmimContext>();
```
- **Acceso Basado En Claims**
```csharp
services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationUserClaimsPrincipalFactory>();
```


---

## **Integración De Pago**  
Currently supports:  
- **Stripe** (Credit/Debit Cards)  
  
---


## **Para contribuciones**  
1. Crea una copia del repositorio.  
2. Crea una rama (`git checkout -b feature-branch`).  
3. Haz Commit de los cambios (`git commit -m "Add new feature"`).  
4. Haz push a la rama (`git push origin feature-branch`).  
5. Abre un Pull Request.  

---

## **Licencia**  
Este proyecto tiene una licencia **MIT License**. See [LICENSE](LICENSE) for details.  

---

## **soporte y contacto**  
Para reportar problemas o preguntas  
📧 Email: 4dm1nemim@gmail.com  
🐞 Open a GitHub Issue  

---

**Disfruta de tu compra y venta, de parte del equipo de desarrollo de EMIM!** 🚀  
