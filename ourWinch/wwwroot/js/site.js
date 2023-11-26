/**
 * <summary>
 * This script highlights the active navigation link based on the current URL path.
 * It also adds 'click' event listeners to navigation links for dynamic highlighting.
 * </summary>
 *
 * <functionality>
 * - The script waits for the DOM content to be fully loaded.
 * - It fetches the current window's pathname.
 * - It selects all elements with the 'nav-link' class and iterates over them.
 * - If a nav link's href attribute matches the current path, it adds the 'active-link' class to it.
 * - Each nav link also gets a 'click' event listener.
 *   - On click, the 'active-link' class is removed from all nav links and then added to the clicked one.
 * </functionality>
 *
 * <usage>
 * - Used in web applications where navigation link highlighting is needed to indicate the current page.
 * - Enhances user experience by visually representing the active page in the navigation menu.
 * </usage>
 */
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
