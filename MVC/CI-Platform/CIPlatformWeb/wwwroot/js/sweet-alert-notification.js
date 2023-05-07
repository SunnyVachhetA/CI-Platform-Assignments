function ajaxErrorSweetAlert(xhr, status, error) {
    console.log("Error: " + status + " - " + error);
    errorMessageSweetAlert();
}

function errorMessageSweetAlert() {
    Swal.fire({
        icon: 'error',
        title: 'Oops',
        text: 'Something went wrong!',
        footer: 'Please try again later!'
    });
}

function successMessageSweetAlert(message) {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: 2000
    })
}

function loginRequiredSweetAlert(loginPageLink, returnUrl = '', text = 'You need to login before using this feature!') {
    if (returnUrl != '') {
        const encodedReturnUrl = encodeURIComponent(returnUrl);
        loginPageLink = `${loginPageLink}?returnUrl=${encodedReturnUrl}`;
    }
    Swal.fire({
        icon: 'info',
        title: 'Login Required!',
        text: text,
        footer: `<a href="${loginPageLink}">Login Here</a>`
    });
}

function requiredSweetAlert(title, text, link, linkText) {
    Swal.fire({
        icon: 'info',
        title: title,
        text: text,
        footer: `<a href="${link}">${linkText}</a>`
    });
}

function displayActionMessageSweetAlert(title, subTitle, icon) {
    Swal.fire(
        title,
        subTitle,
        icon
    )
}