document.getElementById('sign-out').addEventListener('click', logout);

async function logout() {
    try {
        const response = await fetch('/Account/Logout', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        });

        if (response.ok) {
            var jsonResponse = await response.json();
            if (jsonResponse.success == true) {
                location.reload();
            }
        } else {
            console.error('B³¹d wylogowywania:', response.statusText);
        }
    } catch (error) {
        console.error('B³¹d wylogowywania:', error);
    }
}
