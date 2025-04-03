var stripe = Stripe("pk_test_51R8p3nEHOgKBiXCdPTllRynwMDzndAMg5HxkloiVnmBdgI6ImrqhMjrbzFO1yeFkCpGhwgEJd2fdbi6r406Vc62m00hznttsGc"); // publishable key de stripe

document.getElementById("checkout-form").addEventListener("submit", async function (event) {
    event.preventDefault(); // Previene que se envie el form

    // elementos del carrito
    let cartItems = [];
    document.querySelectorAll("#cart-items > div").forEach(item => { // Selecciona los divs dentro de cart-items

        let productName = item.querySelector(".product-name").innerText.trim();
        let quantity = parseInt(item.querySelector(".quantity").innerText.trim());

        //formatea correctamente el precio de los productos
        let priceText = item.querySelector(".price").innerText
            .replace("$", "") // Remueve el signo $
            .replace(/\./g, "") // Remueve el signo de miles
            .replace(",", ".") // Convierte a decimal
            .trim();

        let price = parseFloat(priceText); // convierte el número a decimal

        cartItems.push({
            name: productName,
            quantity: quantity,
            price: price
        });
    });

    console.log(cartItems);


    // envía el cart data al backend
    const response = await fetch("/checkout/createcheckoutsession", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ cartData: cartItems }) // envía el cart data en el request body
    });

    const session = await response.json();

    // Redirije el usuario al Stripe checkout
    stripe.redirectToCheckout({ sessionId: session.id });
});