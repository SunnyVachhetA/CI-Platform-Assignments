
let loggedUserId = $('#logged-user-id').val();

const userLoginPageLink = $('#user-login-page-link').val();
$(document).ready(() => {

    let isLoggedOut1 = $('#is-logged-out').val() === 'True' ? true : false;

    if (isLoggedOut1) {
        successMessageSweetAlert("You have been successfully logged out");
    }

    let isRegistered1 = $('#is-registered').val() === 'True' ? true : false;
    if (isRegistered1) {
        successMessageSweetAlert("You have been successfully registered.");
    }

    let loginSuccessMessage = $('#successful-login').val();

    if (loginSuccessMessage !== undefined && loginSuccessMessage !== '') {
        successMessageSweetAlert(loginSuccessMessage);
    }
});

//Logout Ajax
$('#user-logout').on
    (
        'click',
        () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "You can login using email ID and password!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: 'orange',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Confirm'
            }).then((result) => {
                if (result.isConfirmed) {
                    handleUserLogoutAjax();
                }
            })
        }
    );

function handleUserLogoutAjax() {
    $.ajax({
        type: "GET",
        url: "/Volunteer/User/Logout",
        success: function (result) {
            window.location.href = result.redirectToUrl;
        },
        error: ajaxErrorSweetAlert
    });
}