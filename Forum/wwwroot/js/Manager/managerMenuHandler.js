document.addEventListener('DOMContentLoaded', function () {
    var toggleButton = document.getElementById('menuToggler');
    var menu = document.getElementById('sidebar');

    toggleButton.addEventListener('click', function () {
        if (menu.style.display === 'none' || menu.style.display === '') {
            menu.style.display = 'flex';
        } else {
            menu.style.display = 'none';
        }
    });
});