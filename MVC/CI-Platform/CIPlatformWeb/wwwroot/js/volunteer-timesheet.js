$(document).ready(() => {
    addClickEventListenerToAddButtons();
})

function addClickEventListenerToAddButtons() {
    addVolunteerHourBtnClick();
    addVolunteerGoalBtnClick();
}
function addVolunteerHourBtnClick() {
    $('#btnAddVolunteerHour').click
        (
            () => {
                $.ajax({
                    method: 'GET',
                    url: '/Volunteer/User/AddHourModal',
                    data: { userId: loggedUserId },
                    success: function (result) {
                        $('#partial-modal-container').html(result);
                        $('#addVolunteerHourModal').modal('show');
                        //addClickEventListenerToAddButtons();
                        registerAddVolunteerHoursFormSubmit();
                    },
                    error: ajaxErrorSweetAlert
                })
            }
        );
}

function addVolunteerGoalBtnClick() {
    $('#btnAddVolunteerGoal').click
        (
            () => {
                $.ajax({
                    type: 'GET',
                    url: '/Volunteer/User/AddGoalModal',
                    data: { userId: loggedUserId },
                    success: function (result) {
                        $('#partial-modal-container').html(result);
                        $('#addVolunteerGoalModal').modal('show');
                    }
                });
            }
    );
}

function registerAddVolunteerHoursFormSubmit()
{
    $('#form-add-hour').on('submit',
        (event) =>
        {
            event.preventDefault();
            $('#addVolunteerHourModal').modal('hide');
            $('#form-add-hour').valid();
            if ($('#form-add-hour').valid()) {
                $.ajax({
                    type: 'POST',
                    url: '/Volunteer/User/AddVolunteerHours',
                    data: $('#form-add-hour').serialize(),
                    success: function (result) {
                        $('#vol-timesheet-hour').html(result);
                        displayActionMessageSweetAlert('Volunteer Hours Added!', 'Entry sent to admin for approval.', 'success');
                    },
                    error: ajaxErrorSweetAlert
                });
            }
        }
    );
}

