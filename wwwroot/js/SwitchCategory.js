document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".category-filter").forEach(button => {
        button.addEventListener("click", function () {
            let categoryId = this.getAttribute("data-category-id");
            let categoryName = this.getAttribute("data-category-name");

            if (categoryName == "todos")
            {
                window.location.href = "/";

            } else
            {

            fetch(`/Product/FilterByCategory?categoryId=${categoryId}`)
                .then(response => response.text())
                .then(html => {
                    document.getElementById("products-container").innerHTML = html;
                    document.getElementById("category-title").textContent = categoryName
                        ? `Productos en ${categoryName}`
                        : "Todos los productos";
                })
                    .catch(error => console.error("Error fetching products:", error));

            }
        });
    });
});