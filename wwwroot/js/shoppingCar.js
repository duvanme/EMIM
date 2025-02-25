document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".flex.justify-center.items-center.gap-2").forEach((container) => {
        let counter = container.querySelector("span");
        let decreaseBtn = container.querySelector("button:first-child");
        let increaseBtn = container.querySelector("button:last-child");

        function updateCount(change) {
            let currentValue = parseInt(counter.textContent);
            let newValue = currentValue + change;
            if (newValue < 1) return; // Evita números menores a 1
            counter.textContent = newValue;
        }

        decreaseBtn.addEventListener("click", () => updateCount(-1));
        increaseBtn.addEventListener("click", () => updateCount(1));
    });
});
