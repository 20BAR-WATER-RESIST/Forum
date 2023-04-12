///zmienne
var currentReportId = null;
var currentType = null;
var deleteForm = document.querySelector(".delete-report-modal-form");
var deleteReportModal = document.getElementById("delete-report-modal");
var banFormUser = document.querySelector("#manager-user-provide-ban-modal .needs-validation");
var banFormTopic = document.querySelector("#quick-topic-provide-ban-modal .needs-validation");
var banFormComment = document.querySelector("#quick-comment-provide-ban-modal .needs-validation");
var liftBanForm = document.querySelector("#lift-ban-modal .needs-validation");
var checkboxDeleteElement = document.getElementById('deleteReportManagerDashboardModalCheckbox');
var checkboxElement = document.querySelector('.form-check-input');
var element = document.querySelector(`.manager-ban-button`);

banFormUser.addEventListener("submit", (event) => handleBanFormSubmit(event, banFormUser));
banFormTopic.addEventListener("submit", (event) => handleBanFormSubmit(event, banFormTopic));
banFormComment.addEventListener("submit", (event) => handleBanFormSubmit(event, banFormComment));
liftBanForm.addEventListener("submit", (event) => handleLiftBanFormSubmit(event, liftBanForm));

deleteReportModal.addEventListener("show.bs.modal", resetValidation);
document.getElementById("quick-comment-provide-ban-modal").addEventListener("show.bs.modal", resetValidation);
document.getElementById("quick-topic-provide-ban-modal").addEventListener("show.bs.modal", resetValidation);
document.getElementById("lift-ban-modal").addEventListener("show.bs.modal", resetValidation);

//funkcje
function setID(providedID) {
    currentReportId = providedID;
    }


function setIDSetType(providedID, providedType) {
    currentReportId = providedID;
    currentType = providedType;
}

async function loadComment(id) {
    const commentResponse = await fetch(`?handler=LoadReportedComment&id=${id}`);
    const commentData = await commentResponse.json();

    displayCommentDetails(commentData);
}

function displayCommentDetails(commentData) {
    document.getElementById('comment-author').innerText = `Author: ${commentData.author}`;
    document.getElementById('comment-date').innerText = `Added: ${commentData.date}`;
    document.getElementById('comment-content').innerText = `Comment content: ${commentData.content}`;

    let commentModal = new bootstrap.Modal(document.getElementById('comment-report-details'));
    commentModal.show();
}

//skrypt usuwania raportu
deleteForm.addEventListener('submit', async function (event) {
    event.preventDefault();

    checkbox = checkboxDeleteElement.checked;

    console.log(checkbox);

    if (!checkbox) {
        checkboxElement.classList.add('is-invalid');
    } else {
        checkboxElement.classList.remove('is-invalid');
        checkboxElement.classList.add('is-valid');
        await sendDeleteReportForm();

        let deleteReportModalInstance = bootstrap.Modal.getInstance(deleteReportModal);
        deleteReportModalInstance.hide();
    }

    deleteForm.classList.add('was-validated');
});




const sendDeleteReportForm = async () => {
    console.log('im here now');
    const deleteReportResponse = await fetch("/Manager/Dashboard?handler=DeleteReport",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.getElementsByName('__RequestVerificationToken')[0].value
            },
            body: JSON.stringify({ reportId: parseInt(currentReportId) }),
        });

    if (deleteReportResponse.ok) {
        let jsonresults = await deleteReportResponse.json();
        if (jsonresults.success == true) {
            let deleteReportModalInstance = bootstrap.Modal.getInstance(deleteReportModal);
            deleteReportModalInstance.hide();
            let tableRow = document.querySelector(`[report-row-number="${currentReportId}"]`);
            tableRow.classList.add("d-none");
        } else {
            console.log(jsonresults.message);
        }

    } else {
        console.log(deleteReportResponse.status);
    }
}


function resetValidation(event) {
    let modalElement = event.target;
    let form = modalElement.querySelector('.needs-validation');
    checkboxElement.checked = false;
    checkboxElement.classList.remove("is-invalid");
    checkboxElement.classList.remove("is-valid");
    form.classList.remove("was-validated");
}



// skrypt bana

function handleBanFormSubmit(event, form) {
    event.preventDefault();
    event.stopPropagation();
    if (!form.checkValidity()) {
        event.stopPropagation();
    } else {
        sendBanForm();
    }

    form.classList.add("was-validated");
}

function handleLiftBanFormSubmit(event, form) {
    event.preventDefault();
    event.stopPropagation();
    if (!form.checkValidity()) {
        event.stopPropagation();
    } else {
        sendLiftBanForm();
    }

    form.classList.add("was-validated");
}

const sendBanForm = async () => {

    if (currentType === 'Users') {
        let banReasonInput = document.getElementById('ban-reason-input');
        let banDateTimeInput = document.getElementById('datetime-input');
        const banReasonValue = banReasonInput.value;
        const banDateTimeInputValue = banDateTimeInput.value;

        const banFormResponse = await fetch("/Manager/Dashboard?handler=BanUser",
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.getElementsByName('__RequestVerificationToken')[0].value
                },
                body: JSON.stringify({
                    ReportTargetID: parseInt(currentReportId),
                    ConfirmedBanReason: banReasonValue,
                    UserBannedTime: banDateTimeInputValue
                }),
            });

        if (banFormResponse.ok) {
            let jsonBanResults = await banFormResponse.json();
            if (jsonBanResults.success == true) {
                let modalElement = document.querySelector('.modal.show');
                let modalInstance = bootstrap.Modal.getInstance(modalElement);
                modalInstance.hide();
            } else {
            }

        } else {
        }
    } else if (currentType === 'Comment' || currentType === 'Topic') {
        const quickBanFormResponse = await fetch("/Manager/Dashboard?handler=QuickBan",
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.getElementsByName('__RequestVerificationToken')[0].value
                },
                body: JSON.stringify({
                    ReportTargetID: parseInt(currentReportId),
                    ReportType: currentType
                }),
            });

        if (quickBanFormResponse.ok) {
            let jsonBanResults = await quickBanFormResponse.json();
            if (jsonBanResults.success == true) {
                let modalElement = document.querySelector('.modal.show');
                let modalInstance = bootstrap.Modal.getInstance(modalElement);
                modalInstance.hide();
            } else {
            }

        } else {
        }
    }
}

const sendLiftBanForm = async () => {
    console.log(currentReportId);
   const liftBanFormResponse = await fetch("/Manager/Dashboard?handler=LiftBan",
       {
           method: 'POST',
           headers: {
               'Content-Type': 'application/json',
               'RequestVerificationToken': document.getElementsByName('__RequestVerificationToken')[0].value
           },
           body: JSON.stringify({
               ReportTargetID: parseInt(currentReportId)
           }),
       });

    if (liftBanFormResponse.ok) {
        let jsonBanResults = await liftBanFormResponse.json();
        if (jsonBanResults.success == true) {
            let modalElement = document.querySelector('.modal.show');
            let modalInstance = bootstrap.Modal.getInstance(modalElement);
            modalInstance.hide();
        } else {
        }
    }
}