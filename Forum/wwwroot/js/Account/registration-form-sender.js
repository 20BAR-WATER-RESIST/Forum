var regForm = document.getElementById('register-form');
var registerUsernameInput = document.getElementById('registerUsername');
var registerEmailInput = document.getElementById('registerEmail');
var registerPasswordInput = document.getElementById('registerPassword');
var registerConfirmPasswordInput = document.getElementById('registerConfirmPassword');

regForm.addEventListener('submit', function (event) {
    event.preventDefault();

    var isFormValid = true;

    if (!registerUsernameInput.value || registerUsernameInput.value.length < 6 || registerUsernameInput.value.length > 12) {
        event.stopPropagation();
        registerUsernameInput.classList.add('is-invalid');
        isFormValid = false;
    } else {
        registerUsernameInput.classList.remove('is-invalid');
        registerUsernameInput.classList.add('is-valid');
    }

    if (!registerEmailInput.value || !isValidRegisterEmail(registerEmailInput.value)) {
        event.stopPropagation();
        registerEmailInput.classList.add('is-invalid');
        isFormValid = false;
    } else {
        registerEmailInput.classList.remove('is-invalid');
        registerEmailInput.classList.add('is-valid');
    }

    if (!registerPasswordInput.value || registerPasswordInput.value.length < 8 || registerPasswordInput.value.length > 15 || registerPasswordInput.value.includes(' ')) {
        event.stopPropagation();
        registerPasswordInput.classList.add('is-invalid');
        isFormValid = false;
    } else {
        registerPasswordInput.classList.remove('is-invalid');
        registerPasswordInput.classList.add('is-valid');
    }

    if (registerConfirmPasswordInput.value !== registerPasswordInput.value || registerConfirmPasswordInput.value.length < 8 || registerConfirmPasswordInput.value.length > 15) {
        event.stopPropagation();
        registerConfirmPasswordInput.classList.add('is-invalid');
        isFormValid = false;
    } else {
        registerConfirmPasswordInput.classList.remove('is-invalid');
        registerConfirmPasswordInput.classList.add('is-valid');
    }

    if (isFormValid) {
        sendregisterfetchitems();
    }

    function isValidRegisterEmail(email) {
        var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailRegex.test(email);
    }
});

const sendregisterfetchitems = async () => {
    const registerformData = new FormData(regForm);

    const regresponse = await fetch("/Account/Register", {
        method: "POST",
        body: new URLSearchParams(registerformData)
    });

    const showToast = (isSuccessful) => {
        const regtoastElement = document.getElementById(
            isSuccessful ? "registration-success-toast" : "registration-error-toast"
        );
        const regtoast = new bootstrap.Toast(regtoastElement, { autohide: true, delay: 1500 });
        regtoast.show();
    };

    if (regresponse.ok) {
        const jsonRegResponse = await regresponse.json();
        if (jsonRegResponse.success == true) {
            const registerModalElement = document.getElementById('registerModal');
            const registerModal = bootstrap.Modal.getInstance(registerModalElement);
            showToast(true);

            if (registerModalElement.classList.contains("static-registration-modal")) {
                setTimeout(() => {
                    window.location.href = "/Index";
                }, 2500);
            } else {
                setTimeout(() => {
                    registerModal.hide();
                }, 2500);
            }

        } else {
            const registerErrorAllert = document.getElementById("register-error-alert");
            const registerErrorAllertBody = document.getElementById("registration-danger-allert-message");
            registerErrorAllertBody.textContent = jsonRegResponse.message;
            registerErrorAllert.classList.remove('d-none');
        }
        
    } else {
        showToast(false);
    }
};
