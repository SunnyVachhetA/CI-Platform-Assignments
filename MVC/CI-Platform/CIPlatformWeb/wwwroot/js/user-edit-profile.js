const imgUpload = document.querySelector('#usr-profile-upload');
const profileImg = document.querySelector('#usr-profile-img');

imgUpload.addEventListener
    (
        'change',
        () => {
            const file = imgUpload.files[0];

            profileImg.src = URL.createObjectURL(file);

            handleProfileImageUpload( file );
        }
);

function handleProfileImageUpload(file) {
    var formData = new FormData();
    formData.append('file', file);
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