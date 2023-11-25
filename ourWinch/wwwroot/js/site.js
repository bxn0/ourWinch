document.addEventListener("DOMContentLoaded", function () {

    // Get the current page's path.
    var currentPath = window.location.pathname;

    // Loop through all menu items.
    var navLinks = document.querySelectorAll(".nav-link");
    for (let i = 0; i < navLinks.length; i++) {
        if (navLinks[i].getAttribute('href') === currentPath) {
            navLinks[i].classList.add("active-link");
        }
        navLinks[i].addEventListener("click", function () {
            // Remove the active state from all menus
            for (let j = 0; j < navLinks.length; j++) {
                navLinks[j].classList.remove("active-link");
            }
            // Add the active state to the clicked menu.
            this.classList.add("active-link");
        });
    }
});
