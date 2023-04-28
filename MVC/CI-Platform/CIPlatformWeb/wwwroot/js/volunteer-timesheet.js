const editHourUrl = '/Volunteer/User/EditVolunteerHour';
const editGoalUrl = '/Volunteer/User/EditVolunteerGoal';
const deleteHourUrl = '/Volunteer/User/DeleteVolunteerHour';
const deleteGoalUrl = '/Volunteer/User/DeleteVolunteerGoal';

const modalContainer = $('#partial-modal-container');
let goalCount = 0;
let timeCount = 0;
$(document).ready(() => {
    addClickEventListenerToAddButtons();
    goalCount = $('#goal-msn-count').val();
    timeCount = $('#time-msn-count').val();
    console.log(goalCount);
    console.log(timeCount);
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
                        hideModal();
                        $('#partial-modal-container').html(result);
                        handleCloseModal();
                        $('#addVolunteerHourModal').modal('show');
                        setDateRange();
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
            hideModal();
            $('#partial-modal-container').html(result);
            $('#editVolunteerHourModal').modal('show');
            setDateRange();
            handleCloseModal();
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
            hideModal();
            $('#partial-modal-container').html(result);
            handleCloseModal();
            $('#editVolunteerGoalModal').modal('show');
            setDateRange();
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
            displayActionMessageSweetAlert('Deleted!', 'Your timesheet entry has been deleted.', 'success');
        },
        error: ajaxErrorSweetAlert
    });
}

function addVolunteerGoalBtnClick() {
    $('#btnAddVolunteerGoal').click
        (
            () => {
                if (goalCount == 0) {
                    displayActionMessageSweetAlert('No goal Mission!', 'You need to be volunteer of goal mission to create entry!', 'info');
                    return;
                }
                
                $.ajax({
                    type: 'GET',
                    url: '/Volunteer/User/AddGoalModal',
                    data: { userId: loggedUserId },
                    success: function (result) {
                        hideModal();
                        $('#partial-modal-container').html(result);
                        handleCloseModal();
                        $('#addVolunteerGoalModal').modal('show');
                        setDateRange();
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
            $('#form-add-hour').valid();
            if ($('#form-add-hour').valid()) {
                $('#addVolunteerHourModal').modal('hide');
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
            else {
                $('#form-add-hour').valid();
            }
        }
    );
}

function registerAddVolunteerGoalsFormSubmit() {
    $('#form-add-goal').on('submit',
        (event) => {
            event.preventDefault();
            $('#form-add-goal').valid();
            if ($('#form-add-goal').valid()) {
                $.ajax({
                    type: 'POST',
                    url: '/Volunteer/User/AddVolunteerGoals',
                    data: $('#form-add-goal').serialize(),
                    success: function (result) {
                        hideModal();
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
            $('#form-add-hour').reset();
            $('#form-add-goal').reset();
            $('#form-edit-hour').reset();
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


function handleCloseModal() {

    $('#addVolunteerHourModal, #addVolunteerGoalModal').on('shown.bs.modal', function (e) {
        $(this).css('z-index', parseInt($('.modal.show').last().css('z-index')));
        $(this).find('.modal-content').css('background-color', '#fff');
    });


    $('#addVolunteerHourModal, #addVolunteerGoalModal').on('hide.bs.modal', function (e) {
        if ($('.modal:visible').length > 1) {
            $('body').addClass('modal-open');
        } else {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
    });
}

function hideModal() {
    $('#addVolunteerHourModal').modal('hide');
    $('#addVolunteerGoalModal').modal('hide');
    $('#editVolunteerHourModal').modal('hide');
    $('#editVolunteerGoalModal').modal('hide');
}

function setDateRange() {
    let startDate;
    startDate = $('#vol-msn').find(':selected').data('startdate');
    let endDate = $('#vol-msn').find(':selected').data('enddate');

    $('#vol-msn').on('change', function () {
        startDate = $('#vol-msn').find(':selected').data('startdate');
        console.log(startDate);
        setDate(startDate);
    });
    setDate(startDate, endDate);
}

function setDate(startDate, endDate) {
    var date = moment(startDate, 'DD-MM-YYYY HH:mm:ss Z');
    var formattedDate = date.format('YYYY-MM-DD');

    var current = new Date().toISOString().split('T')[0];
    
    var today = moment().startOf('day');
    var msnEnd = moment(endDate, 'DD-MM-YYYY HH:mm:ss Z');

    if (msnEnd.isBefore(today))
    {
        $('#vol-date').attr('max', msnEnd.format('YYYY-MM-DD'));
    } else if (msnEnd.isAfter(today))
    {
        $('#vol-date').attr('max', current);
    }

    $('#vol-date').attr('min', formattedDate);
}




