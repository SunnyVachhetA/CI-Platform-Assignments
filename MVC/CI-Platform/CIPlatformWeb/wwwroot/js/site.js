let loggedUserId = $('#logged-user-id').val();

const userLoginPageLink = $('#user-login-page-link').val();
const loggedUserName = $('#loggedUserName').val();
const loggedUserEmail = $('#loggedUserEmail').val();
$(document).ready(() => {

    let isLoggedOut1 = $('#is-logged-out').val() === 'True' ? true : false;

    if (isLoggedOut1) {
        successMessageSweetAlert("You have been successfully logged out");
    }


    let loginSuccessMessage = $('#successful-login').val();

    if (loginSuccessMessage !== undefined && loginSuccessMessage !== '') {
        successMessageSweetAlert(loginSuccessMessage);
        loginSuccessMessage = '';
    }
    handleCMSMenu();
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
                confirmButtonColor: '#f88634',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Logout'
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

//Handling contact us form


$('#btn-contact-us').click
    (
        () => {

            if (loggedUserId === undefined || loggedUserId == 0) {
                loginRequiredSweetAlert(userLoginPageLink, window.location.href);
                return;
            }

            $.ajax({
                type: 'GET',
                url: '/Volunteer/User/ContactUs',
                data: { userId: loggedUserId, userName: loggedUserName, userEmail: loggedUserEmail },
                success: function (result) {
                    $('#partial-contact-form').html(result);
                    $('#contactUsFormModal').modal('show');
                    registerContactUsSubmit();
                },
                error: ajaxErrorSweetAlert
            });
        }
    );

function registerContactUsSubmit() {
    $('#contact-form').on('submit', (event) => handleContactUsFormSubmit(event));
}

function handleContactUsFormSubmit(event) {
    event.preventDefault();
    $('#contact-form').valid();

    if ($('#contact-form').valid()) {
        $.ajax({
            type: 'POST',
            url: '/Volunteer/User/ContactUsPost',
            data: $('#contact-form').serialize(),
            success: function (_, _, status) {
                $('#contactUsFormModal').modal('hide');
                displayActionMessageSweetAlert('Thank You!', 'Your message has been received.', 'success');
            },
            error: ajaxErrorSweetAlert
        });
    }
}

const cmsMenu = document.getElementById('cms-drop');


function handleCMSMenu() {
    if (cmsMenu == null) return;

    $.ajax({
        type: 'GET',
        url: '/Volunteer/CmsPage/Index',
        dataType: 'json',
        success: (result) => {
            $('#cms-drop ul').empty();
            result.forEach(page => {
                var li = $('<li>');
                var link = $('<a>').addClass('dropdown-item').text(page.title).attr('href', '/Volunteer/CmsPage/Page/' + page.id);
                li.append(link);
                $('#cms-drop ul').append(li);
            });
        },
        error: ajaxErrorSweetAlert
    });

}

const notificationBodyContainer = document.querySelector('.notification-body-container');
let notisListing;
let notisSettings;
let btnSetting;
let unreadMessages;
let clearAllNotis;
let notificationsCount;
let isBtnClearAllClicked = false;
let isBellClicked = false;
let msgCount = 0;
handleNotificationDisplay();

function handleNotificationDisplay() {
    if (notificationBodyContainer == null || notificationBodyContainer == undefined) return;

    $.ajax({
        type: 'GET',
        url: '/Volunteer/Notification/Index',
        data: { id: loggedUserId },
        success: (result) => {
            $(notificationBodyContainer).html(result);
            loadNotificationOnDOM();
        },
        error: ajaxErrorSweetAlert
    });
}

function loadNotificationOnDOM() {
    notificationsCount = $('#notification-count').val();
    notisBadge = $(".notification-badge");
    notisListing = $('#notification-listing');
    notisSettings = $('#notification-settings');
    btnSetting = $('#notis-setting');
    unreadMessages = $('.dot-orange');
    clearAllNotis = $('#clear-all-notis');
    msgCount = $('#msg-count').val();

    $('.notification').click(() => {
        notisListing.toggleClass('d-none');
        notisSettings.addClass('d-none');
        if (!isBellClicked) genericNotificationAjax('PATCH', '/Volunteer/Notification/UpdateLastCheck', 0, '');
        isBellClicked = true;
    });
    handleMessageRead();
    updateBadgeCount();
    handleNotificationSettingFormSubmit();
    registerNotificationEvents();
}

function updateBadgeCount() {
    if (notificationsCount == 0) {
        notisBadge.remove();
        return;
    }
    notisBadge.text(notificationsCount);
}
function handleMessageRead() {
    $('.dot-orange').click(function () {
        var notifsId = $(this).data('notifsid');
        $(this).replaceWith('<img src="/assets/read.svg" alt="Read" class="ms-auto" height="10px" width="10px">');

        genericNotificationAjax('PATCH', '/Volunteer/Notification/MarkAsRead', notifsId, 'Notification marked as read.');
        notificationsCount--;
        updateBadgeCount();
    });
}
function registerNotificationEvents() {
    btnSetting.click(() => {
        notisListing.addClass('d-none');
        notisSettings.removeClass('d-none');
    });

    $('#notis-cancel').click((e) => {
        e.preventDefault();
        notisSettings.addClass('d-none');
        notisListing.removeClass('d-none');
    });

    clearAllNotis.click(() => {
        if (isBtnClearAllClicked || msgCount == 0) return;
        $('.notis-listing').remove();
        let div = $("<div>").addClass("bg-light text-center py-1 fw-light text-black-1").text('Woohoo.. No notifications for now!');
        notisListing.append(div);
        notificationsCount = 0;
        genericNotificationAjax('DELETE', '/Volunteer/Notification/Delete', 0, 'Notifications removed!');
        isBtnClearAllClicked = true;
    });
}

function genericNotificationAjax(type, url, notifsId, msg) {
    if (notifsId != 0) {
        $.ajax({
            type: type,
            url: url,
            data: { userId: loggedUserId, id: notifsId },
            success: (_) => successMessageSweetAlert(msg),
            error: ajaxErrorSweetAlert
        });
    }
    else {
        $.ajax({
            type: type,
            url: url,
            global: false,
            data: { userId: loggedUserId },
            success: (_) => {
                if (msg === '') return;
                successMessageSweetAlert(msg)
            },
            error: ajaxErrorSweetAlert
        });
    }
}

function handleNotificationSettingFormSubmit() {
    $('#form-notifs-settings').on('submit', e => {
        e.preventDefault();
        notisSettings.addClass('d-none');
        notisListing.addClass('d-none');
        $.ajax({
            type: 'PUT',
            url: '/Volunteer/Notification/Settings',
            data: $('#form-notifs-settings').serialize(),
            success: (_) => successMessageSweetAlert("Notification settings saved!"),
            error: ajaxErrorSweetAlert
        });
    });
}