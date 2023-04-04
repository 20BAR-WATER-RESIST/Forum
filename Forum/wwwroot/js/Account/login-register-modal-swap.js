document.addEventListener('DOMContentLoaded', function() {
    const loginmod = new bootstrap.Modal(document.getElementById('loginModal'));
    const registermod = new bootstrap.Modal(document.getElementById('registerModal'));

    document.getElementById('openRegisterModal').addEventListener('click', function() {
        loginmod.hide();
        registermod.show();
    });

    document.getElementById('openLoginModal').addEventListener('click', function() {
        registermod.hide();
        loginmod.show();
    });

});

