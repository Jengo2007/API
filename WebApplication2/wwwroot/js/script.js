// const menuItems = document.querySelectorAll(".menu-item");
// const welcomeSection = document.querySelector(".welcome");
// const cashiersSection = document.querySelector(".cashiers-section");
// const usersSection = document.querySelector(".users-section");
// function hideAllSections() {
//     welcomeSection.style.display = "none";
//     cashiersSection.style.display = "none";
//     usersSection.style.display = "none";
// }
//
// menuItems[1].addEventListener("click", (e) => {
//     e.preventDefault();
//     hideAllSections();
//     cashiersSection.style.display = "block";
// });
//
// menuItems[2].addEventListener("click", (e) => {
//     e.preventDefault();
//     hideAllSections();
//     usersSection.style.display = "block";
// });

// 📌 Открытие модалки
document.addEventListener('DOMContentLoaded', function () {
    // Удаляем все предыдущие обработчики, если они есть
    const modal = document.getElementById("editCashierModal");
    modal.classList.remove("show"); // Убедимся, что модалка скрыта при загрузке

    document.querySelectorAll(".edit-cashier-btn").forEach(function (button) {
        button.removeEventListener("click", openModal); // Удаляем старый обработчик
        button.addEventListener("click", openModal);
    });

    function openModal() {
        const cashierId = this.getAttribute("data-cashier-id");
        const cashierName = this.getAttribute("data-cashier-name");
        const cashierPhone = this.getAttribute("data-cashier-phone");

        document.getElementById("cashierId").value = cashierId;
        document.getElementById("cashierName").value = cashierName;
        document.getElementById("cashierPhone").value = cashierPhone;

        modal.classList.add("show");
    }

    // Закрытие модалки
    document.querySelectorAll(".custom-close, .cancel-btn").forEach(function (btn) {
        btn.addEventListener("click", function () {
            modal.classList.remove("show");
        });
    });

    // Закрытие при клике вне модалки
    modal.addEventListener("click", function (event) {
        if (event.target === modal) {
            modal.classList.remove("show");
        }
    });
});



// 📌 Отправка формы
document.getElementById("editCashierForm").addEventListener("submit", function (e) {
    e.preventDefault();

    const formData = new FormData(this);

    fetch("/Admin/EditCashier?id=" + formData.get("Id"), {
        method: "POST",
        body: formData
    })
        .then(response => {
            if (response.ok) {
                document.getElementById("editCashierModal").classList.remove("show");
                location.reload();
            } else {
                alert("Ошибка при сохранении.");
            }
        })
        .catch(error => {
            console.error("Ошибка:", error);
        });
});