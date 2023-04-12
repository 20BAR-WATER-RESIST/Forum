var reportForms = document.querySelectorAll('.needs-validation-send-report-form');
var reportModals = document.querySelectorAll('.report-modal');

reportForms.forEach((reportForm, index) => {
    reportForm.addEventListener('submit', async function (event) {
        event.preventDefault();
        event.stopPropagation();
        if (!reportForm.checkValidity()) {
            event.stopPropagation();
        } else {
            await sendReport(reportForm, index);
        }

        reportForm.classList.add("was-validated");
    });

    reportModals[index].addEventListener("show.bs.modal", function (event) {
        var button = event.relatedTarget;
        var sendingUserId = button.getAttribute("data-sending-user-id");
        var reportedUserId = button.getAttribute("data-reported-user-id");
        var reportType = button.getAttribute("data-report-type");

        resetReportValidation(reportForm, sendingUserId, reportedUserId, reportType);
    });
});

function resetReportValidation(reportForm, sendingUserId, reportedUserId, reportType) {
    reportForm.classList.remove("was-validated");

    var sendingUserIdInput = reportForm.querySelector("#sending-user-id");
    var reportedUserIdInput = reportForm.querySelector("#reported-user-id");
    var reportTypeInput = reportForm.querySelector("#report-type");

    sendingUserIdInput.value = sendingUserId;
    reportedUserIdInput.value = reportedUserId;
    reportTypeInput.value = reportType;
}

const showReportToast = (isSuccessful) => {
    let toastElement = document.getElementById(
        isSuccessful ? "report-success-toast" : "report-error-toast"
    );
    let toast = new bootstrap.Toast(toastElement, { autohide: true, delay: 2500 });
    toast.show();
};

async function sendReport(reportForm, index) {
    const formData = new FormData(reportForm);

    const response = await fetch("?handler=ReportSender", {
        method: "POST",
        body: formData
    });

    if (response.ok) {
        let jsonResponse = await response.json();
        if (jsonResponse.success) {
            const modalInstance = bootstrap.Modal.getInstance(reportModals[index]);
            modalInstance.hide();
            showReportToast(true);
        } else {
            showReportToast(false);
        }

    } else {
        showReportToast(false);
    }
}

const showErrorToast = () => {
    const toastElement = document.getElementById("unauthenticated-user-error-toast");
    const toast = new bootstrap.Toast(toastElement, { autohide: true, delay: 2500 });
    toast.show();
};