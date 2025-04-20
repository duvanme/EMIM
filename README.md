# **EMIM - README**  

## **Table of Contents**  
1. [Resumen](#Resumen)  
2. [Características](#Características)  
3. [Getting Started](#getting-started)  
   - [Prerequisites](#prerequisites)  
   - [Installation](#installation)  
   - [Configuration](#configuration)  
4. [Usage](#usage)  
   - [For Buyers](#for-buyers)  
   - [For Sellers](#for-sellers)  
   - [For Administrators](#for-administrators)  
5. [API Documentation](#api-documentation)  
6. [Security](#security)  
7. [Payment Integration](#payment-integration)  
8. [Deployment](#deployment)  
9. [Contributing](#contributing)  
10. [License](#license)  
11. [Support & Contact](#support--contact)  

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
- **Base De DAtos**: SQL Server 2019  
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
- Rename `.env.example` to `.env` and fill in:  
  ```env
  DB_HOST=your_database_host
  DB_USER=your_db_user
  DB_PASS=your_db_password
  STRIPE_API_KEY=your_stripe_key
  JWT_SECRET=your_jwt_secret
  ```  

---

## **Usage**  

### **For Buyers**  
1. **Browse Products** – Use search & filters to find items.  
2. **Add to Cart** – Select quantity and add to cart.  
3. **Checkout** – Enter shipping details and pay securely.  
4. **Track Orders** – View order status in the dashboard.  

### **For Sellers**  
1. **Register as a Seller** – Apply and get approved (if admin approval is required).  
2. **List Products** – Add product details, images, and pricing.  
3. **Manage Orders** – Update order status and handle shipping.  
4. **View Earnings** – Track sales and withdraw funds.  

### **For Administrators**  
1. **Dashboard** – Monitor site activity, sales, and user growth.  
2. **User Management** – Approve sellers, ban users, or adjust permissions.  
3. **Product Moderation** – Review and remove inappropriate listings.  
4. **Site Settings** – Configure fees, payment methods, and policies.  

---

## **API Documentation**  
The backend provides RESTful APIs for:  
- **User Authentication** (`/api/auth`)  
- **Product Management** (`/api/products`)  
- **Order Processing** (`/api/orders`)  
- **Payment Handling** (`/api/payments`)  

See the full API docs [here](#) (link to Swagger/Postman docs).  

---

## **Security**  
- **Data Encryption** – All sensitive data is encrypted (AES-256).  
- **Rate Limiting** – Prevents brute-force attacks.  
- **CSRF Protection** – Secure form submissions.  
- **Regular Audits** – Dependency checks with `npm audit` / `snyk`.  

---

## **Payment Integration**  
Currently supports:  
- **Stripe** (Credit/Debit Cards)  
- **PayPal** (Express Checkout)  
- **Bank Transfers** (Manual verification)  

To enable a new payment method, update the `paymentConfig.js` file.  

---

## **Deployment**  
### **Option 1: Cloud Hosting (AWS/Heroku)**  
```bash
heroku create
git push heroku main
```  
### **Option 2: Docker Deployment**  
```bash
docker-compose up --build
```  

---

## **Contributing**  
1. Fork the repository.  
2. Create a new branch (`git checkout -b feature-branch`).  
3. Commit changes (`git commit -m "Add new feature"`).  
4. Push to the branch (`git push origin feature-branch`).  
5. Open a Pull Request.  

---

## **License**  
This project is licensed under **MIT License**. See [LICENSE](LICENSE) for details.  

---

## **Support & Contact**  
For issues or questions, please:  
📧 Email: support@marketplace.com  
🐞 Open a GitHub Issue  

---

**Happy Buying & Selling!** 🚀  

---

You can customize this README by:  
- Adding your project's specific tech stack  
- Including screenshots or demo links  
- Adjusting installation steps based on your setup  
- Adding more API details if applicable  
- Updating security and compliance policies  

Let me know if you'd like any modifications!