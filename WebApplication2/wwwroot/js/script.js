const menuItems = document.querySelectorAll(".menu-item");
const welcomeSection = document.querySelector(".welcome");
const cashiersSection = document.querySelector(".cashiers-section");
const usersSection = document.querySelector(".users-section");
function hideAllSections() {
    welcomeSection.style.display = "none";
    cashiersSection.style.display = "none";
    usersSection.style.display = "none";
}

menuItems[1].addEventListener("click", (e) => {
    e.preventDefault();
    hideAllSections();
    cashiersSection.style.display = "block";
});

menuItems[2].addEventListener("click", (e) => {
    e.preventDefault();
    hideAllSections();
    usersSection.style.display = "block";
});
