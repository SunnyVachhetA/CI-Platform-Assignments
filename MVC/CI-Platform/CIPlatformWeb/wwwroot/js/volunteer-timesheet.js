const editHourUrl = '/Volunteer/User/EditVolunteerHour';
const editGoalUrl = '/Volunteer/User/EditVolunteerGoal';
const deleteHourUrl = '/Volunteer/User/DeleteVolunteerHour';
const deleteGoalUrl = '/Volunteer/User/DeleteVolunteerGoal';

let goalCount = 0;
let timeCount = 0;
$(document).ready(() => {
    addClickEventListenerToAddButtons();
    goalCount = $('#goal-msn-count').val();
    timeCount = $('#time-msn-count').val();
})

function addClickEventListenerToAddButtons() {
    addVolunteerHourBtnClick();
    addVolunteerGoalBtnClick();
    addClickEventListenerToEditDeleteButtons();
}
function addVolunteerHourBtnClick() {
    $('#btnAddVolunteerHour').click
        (
            () => {
                if (timeCount == 0) {
                    displayActionMessageSweetAlert('No Time Mission!', 'You need to be volunteer of time mission to create entry!', 'info');
                    return;
                }
                $.ajax({
                    method: 'GET',
                    url: '/Volunteer/User/AddHourModal',
                    data: { userId: loggedUserId },
                    success: function (result) {
                        $('#partial-modal-container').html(result);
                        $('#addVolunteerHourModal').modal('show');
                        addClickEventListenerToAddButtons();
                        registerAddVolunteerHoursFormSubmit();
                    },
                    error: ajaxErrorSweetAlert
                })
            }
        );
}
function addClickEventListenerToEditDeleteButtons() {
    $('.edit-hour-entry').each
    (
        (_, item) => {
            $(item).on('click', handleVolunteerHourEdit);
        }
    );
    $('.edit-goal-entry').each((_, item) => $(item).on('click', handleVolunteerGoalEdit));

    $('.delete-hour-entry').each((_, item) => $(item).on('click', handleVolunteerDelete));
    $('.delete-goal-entry').each((_, item) => $(item).on('click', handleVolunteerDelete));
}

function handleVolunteerHourEdit() {
    let timesheetId = $(this).data('timesheetid');
    $.ajax({
        type: 'GET',
        url: editHourUrl,
        data: { timesheetId: timesheetId, userId: loggedUserId },
        success: function (result) {
            $('#partial-modal-container').html(result);
            $('#editVolunteerHourModal').modal('show');
            registerEditHourFormSubmit();
        },
        error: ajaxErrorSweetAlert
    });
}

function handleVolunteerGoalEdit() {
    let timesheetId = $(this).data('timesheetid');
    $.ajax({
        type: 'GET',
        url: editGoalUrl,
        data: { timesheetId: timesheetId, userId: loggedUserId },
        success: function (result) {
            $('#partial-modal-container').html(result);
            $('#editVolunteerGoalModal').modal('show');
            registerEditGoalFormSubmit();
        },
        error: ajaxErrorSweetAlert
    });
}

function handleVolunteerDelete() {
    let timesheetId = $(this).data('timesheetid');
    let type = $(this).data('type');
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to recover entry!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#f88634',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete'
    }).then((result) => {
        if (result.isConfirmed) {
            deleteVoluteerEntry(timesheetId, type);
        }
    })
}

function deleteVoluteerEntry(timesheetId, type) {
    $.ajax({
        type: 'DELETE',
        url: '/Volunteer/User/DeleteTimesheetEntry',
        data: { userId: loggedUserId, timesheetId: timesheetId, type: type },
        success: function (result) {
            if (type === 'goal')
                $('#vol-timesheet-goal').html(result);
            else
                $('#vol-timesheet-hour').html(result);

            addClickEventListenerToAddButtons();
            register
            displayActionMessageSweetAlert('Deleted!', 'Your timesheet entry has been deleted.', 'success');
        },
        error: ajaxErrorSweetAlert
    });
}

function addVolunteerGoalBtnClick() {
    $('#btnAddVolunteerGoal').click
        (
            () => {
                if (timeCount == 0) {
                    displayActionMessageSweetAlert('No Time Mission!', 'You need to be volunteer of goal mission to create entry!', 'info');
                    return;
                }
                $.ajax({
                    type: 'GET',
                    url: '/Volunteer/User/AddGoalModal',
                    data: { userId: loggedUserId },
                    success: function (result) {
                        $('#partial-modal-container').html(result);
                        $('#addVolunteerGoalModal').modal('show');
                        registerAddVolunteerGoalsFormSubmit();
                        addClickEventListenerToAddButtons();
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
                        addClickEventListenerToAddButtons();
                        registerAddVolunteerHoursFormSubmit();
                    },
                    error: ajaxErrorSweetAlert
                });
            }
        }
    );
}

function registerAddVolunteerGoalsFormSubmit() {
    $('#form-add-goal').on('submit',
        (event) => {
            event.preventDefault();
            $('#addVolunteerGoalModal').modal('hide');
            $('#form-add-goal').valid();
            if ($('#form-add-goal').valid()) {
                $.ajax({
                    type: 'POST',
                    url: '/Volunteer/User/AddVolunteerGoals',
                    data: $('#form-add-goal').serialize(),
                    success: function (result) {
                        $('#vol-timesheet-goal').html(result);
                        displayActionMessageSweetAlert('Volunteer Goals Added!', 'Entry sent to admin for approval.', 'success');
                        registerAddVolunteerGoalsFormSubmit();
                        addClickEventListenerToAddButtons();
                    },
                    error: ajaxErrorSweetAlert
                });
            }
        }
    );
}

function registerEditHourFormSubmit() {
    $('#form-edit-hour').on('submit',
        (evt) => {
            evt.preventDefault();
            $('#form-edit-hour').valid();
            if (!$('#form-edit-hour').valid()) return;
            $('#editVolunteerHourModal').modal('hide');
            $.ajax({
                type: 'PUT',
                url: editHourUrl+'Put',
                data: $('#form-edit-hour').serialize(),
                success: function (result) {
                    $('#vol-timesheet-hour').html(result);
                    displayActionMessageSweetAlert('Entry edited!', 'Changes have been sent to admin for approval!', 'success');
                    addClickEventListenerToAddButtons();
                },
                error: ajaxErrorSweetAlert
            });
        }
    );
}

function registerEditGoalFormSubmit() {
    $('#form-edit-goal').on('submit',
        (evt) => {
            evt.preventDefault();
            $('#form-edit-goal').valid();
            if (!$('#form-edit-goal').valid()) return;
            $('#editVolunteerGoalModal').modal('hide');
            $.ajax({
                type: 'PUT',
                url: editGoalUrl + 'Put',
                data: $('#form-edit-goal').serialize(),
                success: function (result) {
                    $('#vol-timesheet-goal').html(result);
                    displayActionMessageSweetAlert('Entry edited!', 'Changes have been sent to admin for approval!', 'success');
                    addClickEventListenerToAddButtons();
                },
                error: ajaxErrorSweetAlert
            });
        }
    );
}