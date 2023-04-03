const imgUpload = document.querySelector('#usr-profile-upload');
const profileImg = document.querySelector('#usr-profile-img');

imgUpload.addEventListener
    (
        'change',
        () => {
            const file = imgUpload.files[0];

            profileImg.src = URL.createObjectURL(file);

            handleProfileImageUpload(file);
        }
    );

function handleProfileImageUpload(file) {
    let avatar = $('#user-avatar').val();
    let userId = $('#edit-user-id').val();
    var formData = new FormData();
    formData.append('file', file);
    formData.append('avatar', avatar);
    formData.append('userId', userId);
    $.ajax({
        type: 'PATCH',
        url: '/Volunteer/User/UserAvatar',
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
        },
        error: ajaxErrorSweetAlert
    });
}



$('#country-menu option').click
    (
        () => {
            let countryId = $('#country-menu').find(':selected').val();
            handleCityDisplayOption(countryId);
        }
    );

let cityList = $('[data-countryid]')
function handleCityDisplayOption(countryId) {

    if ($('#city-menu').find(':selected').data('countryid') != countryId) {
        $('#city-menu option[value=""]').prop('selected', true);
    }

    $.each(cityList, (_, item) => {
        let id = $(item).data('countryid');

        if (countryId === undefined) return;

        if (id == countryId)
            $(item).show();
        else
            $(item).hide();
    });
}


//Change Password
$('#btn-change-password').on('click', handleChangePassword);

function handleChangePassword(event)
{
    
    let UserId = $('#changePasswordVm_UserId').val();
    let OldPassword = $('#changePasswordVm_OldPassword').val();
    let NewPassword = $('#changePasswordVm_NewPassword').val();
    let ConfirmPassword = $('#changePasswordVm_ConfirmPassword').val();

    
    alert(UserId);
    if (NewPassword != ConfirmPassword) {
        return;    
    }

    $.ajax({
        type: 'PATCH',
        url: '/Volunteer/User/ChangePassword',
        data:
        {
            UserId, OldPassword, NewPassword, ConfirmPassword
        },
        success: function (_, _, jqXHR) {
            console.log(jqXHR.status);
            if (jqXHR.status == 200) {
                $('#changePasswordModal').modal('hide');
                displayActionMessageSweetAlert('Password Changed', 'Password Changed Successfully!', 'success');
            }
            else {
                $('#err-old-pass').show();
                $('#err-old-pass').text('Incorrect Old Password');    
            }
        },
        error: ajaxErrorSweetAlert
    });

}

$('#changePasswordVm_ConfirmPassword').keydown
    (
        () =>
        {
            let password = $('#changePasswordVm_NewPassword').val();

            let cnfPass = $('#changePasswordVm_ConfirmPassword').val();

            if (password != cnfPass) {
                $('#err-cnf-pass').show();
                $('#err-cnf-pass').text('Confirm password should be same as new password!');
            }
            else {
                $('#err-cnf-pass').hide();
            }
        }
);

