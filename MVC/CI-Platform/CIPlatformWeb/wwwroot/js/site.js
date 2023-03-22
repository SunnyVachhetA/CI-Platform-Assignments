
$(document).ready(() => {

    let isLoggedOut1 = $('#is-logged-out').val() === 'True' ? true : false;

    if (isLoggedOut1) {
        successMessageSweetAlert("You have been successfully logged out");
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
            console.log(result.redirectToUrl);
            window.location.href = result.redirectToUrl;
        },
        error: ajaxErrorSweetAlert
    });
}