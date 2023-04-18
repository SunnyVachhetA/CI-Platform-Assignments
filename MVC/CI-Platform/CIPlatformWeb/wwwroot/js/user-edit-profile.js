const imgUpload = document.querySelector('#usr-profile-upload');
const profileImg = document.querySelector('#usr-profile-img');
const navProfileImg = document.querySelector('#nav-profile-img');


imgUpload.addEventListener
    (
        'change',
        () => {
            const file = imgUpload.files[0];

            profileImg.src = URL.createObjectURL(file);
            navProfileImg.src = profileImg.src;

            handleProfileImageUpload(file);

            $('#user-avatar').val(profileImg.src);
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
                $('#changePasswordVm_OldPassword').val('');
                $('#changePasswordVm_NewPassword').val('');
                $('#changePasswordVm_ConfirmPassword').val('');
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

$('#changePasswordVm_ConfirmPassword').change
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


//Add Skill Modal

const userSkillMap = new Map();
const userSkillList = [];
let currentSelectedSkill = {};
//For add/remove skill
$('#user-skill li').each(function () {
    const skillid = $(this).attr('data-skillid');
    userSkillList.push(skillid);
    userSkillMap.set(skillid, $(this).text());
});
handleAllSkillActive();
function handleAllSkillActive() {
    $('#all-skill li').each(function () {
        const skillid = $(this).attr('data-skillid');
        if (userSkillMap.has(skillid)) {
            $(this).addClass('active');
        }
        else {
            $(this).removeClass('active');
        }
    });
}

let prevSkill;
$('#all-skill li').each
    (
        (_, item) => {
            $(item).click
                (
                    () => {
                        const skillid = $(item).attr('data-skillid');

                        if (!$.isEmptyObject(prevSkill) && prevSkill.attr('data-skillid') !== skillid && !userSkillMap.has(prevSkill.attr('data-skillid'))) {
                            prevSkill.removeClass('active');
                        }
                        prevSkill = $(item);

                        if (!userSkillMap.has(skillid)) {
                            $(item).addClass('active');
                            currentSelectedSkill.skillid = skillid;
                            currentSelectedSkill.skill = $(item).text();
                            currentSelectedSkill.from = 'allskill';
                        }

                    }
                );
        }
    );

$('#usr-skill-add').click
    (
        () => {
            if (Object.keys(currentSelectedSkill).length !== 0 && !userSkillMap.has(currentSelectedSkill.skillid)) {
                if (currentSelectedSkill.from == 'allskill') {
                    let skillLi = $("<li>").text(currentSelectedSkill.skill).attr('data-skillid', currentSelectedSkill.skillid);
                    skillLi.click(selectFromUserSkill);
                    $('#user-skill').append(skillLi);
                    userSkillMap.set(currentSelectedSkill.skillid, currentSelectedSkill.skill);
                }
            }
        }
    );


let selectedRemoveSkill;
$('#user-skill li').each
    (
        (_, item) => {
            $(item).click
                (
                    selectFromUserSkill
                );
        }
    );

function selectFromUserSkill(event) {
    event.preventDefault();
    $('#user-skill li').removeClass('active');
    $(this).addClass('active');
    selectedRemoveSkill = $(this);
}

$('#usr-skill-remove').click
    (
        () => {
            if (selectedRemoveSkill !== undefined && !$.isEmptyObject(selectedRemoveSkill)) {
                const skillid = $(selectedRemoveSkill).attr('data-skillid');
                userSkillMap.delete(skillid);
                selectedRemoveSkill.remove();
                console.log(userSkillMap);
                handleAllSkillActive();
            }
        }
    );

$('#btn-skill-save').click(displayUserProfileSkills);
function displayUserProfileSkills() {
    $('#user-profile-skill').empty();

    for (const [skillid, skill] of userSkillMap) {
        
        let li = $("<li>").text(skill).attr('data-skillid', skillid);
        let inputElement = $('<input>', {
            type: 'hidden',
            value: skillid,
            name: 'finalSkillList'
        });
        $('#user-profile-skill').append(li);
        $('#user-profile-skill').append(inputElement);
    }
    if (userSkillMap.size === 0) {
        var div = $('<div>').addClass('ps-3 py-2 fw-light font-xs text-danger').attr('id', 'no-skill').text('No skill added!');
        $('#user-profile-skill').append(div);
    }
    
    $('#addSkillModal').modal('hide');
}

