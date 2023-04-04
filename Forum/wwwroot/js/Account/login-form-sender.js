var logForm = document.getElementById("login-form");
var loginEmailInput = document.getElementById('loginEmail');
var loginPasswordInput = document.getElementById('loginPassword');

logForm.addEventListener('submit', function (event) {
    event.preventDefault();

    var isFormValid = true;

    if (!loginEmailInput.value || !isValidLoginEmail(loginEmailInput.value)) {
        event.stopPropagation();
        loginEmailInput.classList.add('is-invalid');
        isFormValid = false;
    } else {
        loginEmailInput.classList.remove('is-invalid');
        loginEmailInput.classList.add('is-valid');
    }

    if (!loginPasswordInput.value || loginPasswordInput.value.length < 8 || loginPasswordInput.value.length > 15 || loginPasswordInput.value.includes(' ')) {
        event.stopPropagation();
        loginPasswordInput.classList.add('is-invalid');
        isFormValid = false;
    } else {
        loginPasswordInput.classList.remove('is-invalid');
        loginPasswordInput.classList.add('is-valid');
    }

    if (isFormValid) {
        sendfetchitems();
    }

    function isValidLoginEmail(email) {
        var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailRegex.test(email);
    }
});

const sendfetchitems = async () => {
    const logFormData = new FormData(logForm);

    const response = await fetch("/Account/Login", {
        method: "POST",
        body: new URLSearchParams(logFormData)
    });

    const showToast = (isSuccessful) => {
        const toastElement = document.getElementById(
            isSuccessful ? "login-success-toast" : "login-error-toast"
        );
        const toast = new bootstrap.Toast(toastElement, { autohide: true, delay: 1500 });
        toast.show();
    };

    if (response.ok) {
        var jsonLoginResponse = await response.json();
        if (jsonLoginResponse.success == true) {
            const loginModalElement = document.getElementById('loginModal');
            const loginModal = bootstrap.Modal.getInstance(loginModalElement);
            loginModal.hide();
            showToast(true);
            
            if (loginModalElement.classList.contains("static-login-modal")) {
                setTimeout(() => {
                    window.location.href = "/Index";
                }, 2500);
            } else {
                setTimeout(() => {
                location.reload();
                }, 2500);
            }
        }
        else {
            const loginErrorAllert = document.getElementById("login-error-alert");
            const loginErrorAllertBody = document.getElementById("login-danger-allert-message");
            loginErrorAllertBody.textContent = jsonLoginResponse.message;
            loginErrorAllert.classList.remove('d-none');
        }
        
    } else {
        showToast(false);
    }
};

