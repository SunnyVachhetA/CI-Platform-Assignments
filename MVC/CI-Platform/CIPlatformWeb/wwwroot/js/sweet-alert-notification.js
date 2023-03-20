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
        timer: 1500
    })
}

function loginRequiredSweetAlert(loginPageLink) {
    Swal.fire({
        icon: 'info',
        title: 'Login Required!',
        text: 'You need to login before adding favourite mission.',
        footer: `<a href="${loginPageLink}">Login Here</a>`
    });
}

function displayActionMessageSweetAlert(title, subTitle, icon) {
    Swal.fire(
        title,
        subTitle,
        icon
    )
}