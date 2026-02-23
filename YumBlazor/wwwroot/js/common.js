function ShowConfirmationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById("confirmation-modal")).show();
}

function HideConfirmationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById("confirmation-modal")).hide();
}