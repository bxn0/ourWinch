document.addEventListener("DOMContentLoaded", function () {

    // Şu anki sayfanın yolunu al
    var currentPath = window.location.pathname;

    // Tüm menü öğelerini döngüye al
    var navLinks = document.querySelectorAll(".nav-link");
    for (let i = 0; i < navLinks.length; i++) {
        if (navLinks[i].getAttribute('href') === currentPath) {
            navLinks[i].classList.add("active-link");
        }
        navLinks[i].addEventListener("click", function () {
            // Tüm menülerden aktifliği kaldır
            for (let j = 0; j < navLinks.length; j++) {
                navLinks[j].classList.remove("active-link");
            }
            // Tıklanan menüye aktifliği ekle
            this.classList.add("active-link");
        });
    }
});
