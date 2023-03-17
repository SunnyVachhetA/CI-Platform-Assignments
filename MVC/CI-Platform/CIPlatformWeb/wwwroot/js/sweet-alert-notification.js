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