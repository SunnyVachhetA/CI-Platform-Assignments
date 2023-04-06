addVolunteerHourBtnClick();
addVolunteerGoalBtnClick();
function addVolunteerHourBtnClick() {
    $('#btnAddVolunteerHour').click
        (
            () => {
                $.ajax({
                    method: 'GET',
                    url: '/Volunteer/User/AddHourModal',
                    success: function (result) {
                        $('#partial-modal-container').html(result);
                        $('#addVolunteerHourModal').modal('show');
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
                    success: function (result) {
                        $('#partial-modal-container').html(result);
                        $('#addVolunteerGoalModal').modal('show');
                    }
                });
            }
    );
}