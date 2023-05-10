let missionId = 0;
let userId = $('#logged-user-id').val();
let loginPageLink = $('#login-page-link').val();
let favLink = $('#add-fav-link').val();
let isFav = $('#is-favourite').val();
const btnFavourite = document.querySelector('#btn-add-to-favourite');
let isLoggedUserVolunteer = $('#isLoggedUserVolunteer').val();
let loggedUserRating = 0;
let starClickFlag = false;
const btnPostComment = document.querySelector('#btn-comment-post');
const commentBox = document.querySelector('#comment-text');
const commentErr = document.querySelector('#err-comment');
let isCommentExists = $('#isCommentExists').val() === 'True' ? true : false;
let returnUrl = window.location.href;
let currentVolunteerPage = 1;
let totalVolunteersCount = $('#total-volunteer-count').val();
let displayVolunteerCount = 2;
const totalVlPage = Math.ceil(totalVolunteersCount / displayVolunteerCount);
if (totalVolunteersCount === undefined || totalVolunteersCount == 0) {
    $('#no-recent-vl').removeClass('d-none');
    $('#vl-list').addClass('d-none');
    $('#vl-pagination').addClass('d-none');
}

$(document).ready(
    () =>
    {
        missionId = $('#msn-details-id').val();
        returnUrl = window.location.href;

        loggedUserRating = $('#logged-user-rating').val();
        starClickFlag = (loggedUserRating != 0) ? true : false;
        toggleBtnFavourite();
        loadMissionRatingAjax();
        let themeId = $('#theme-id').val();
        loadMissionCommentsAjax();
        loadRelatedMissionsAjax(themeId);
        loadRecentVolunteers();
    }
);
function loadMissionCommentsAjax() {
    console.log(isCommentExists);
    $.ajax({
        type: 'GET',
        data: { missionId, userId, isCommentExists},
        url: '/Volunteer/Mission/MissionComments',
        success: function (result) {
            $('#msn-comment').html(result);
        },
        error: ajaxErrorSweetAlert
    });
}

function loadMissionRatingAjax() {
    console.log( missionId );
    $.ajax({
        type: 'GET',
        data: { missionId },
        url: '/Volunteer/Mission/MissionRating',
        success: function (result) {
            $('#star-rating-box').html(result);
        },
        error: ajaxErrorSweetAlert
    });
}

function toggleBtnFavourite() {
    let favImage = btnFavourite.getElementsByTagName("img")[0];
    $('#btn-add-to-favourite').empty();
    if (isFav === 'True') {
        $('#btn-add-to-favourite').addClass('active-fav');
        favImage.src = '/assets/heart-992.png';
        favImage.style.height = '22px';
        favImage.style.width = '22px';
        $('#btn-add-to-favourite').append(favImage);
        $('#btn-add-to-favourite').append('Remove From Favourite');
    }
    else {
        
        $('#btn-add-to-favourite').removeClass('active-fav');
        favImage.src = '/assets/heart1.png';
        $('#btn-add-to-favourite').append(favImage);
        $('#btn-add-to-favourite').append('Add To Favourite');
    }

}
btnFavourite.addEventListener('click', () => {
    if (userId == 0) {
        loginRequiredSweetAlert(loginPageLink, returnUrl, 'You need to login before adding favourite!');
    }
    else
    {
        addMissionToFavouritesAjax();
    }
});

function addMissionToFavouritesAjax()
{
    $.ajax({
        type: 'GET',
        url: favLink,
        data: { missionId, userId, isFav },
        success: function (result, status, jqXHR) {
            let message = '';
            if (jqXHR.status === 201) {
                isFav = 'True';
                message = 'Mission added to your favourites..';
            }
            else {
                message = 'Mission removed from your favourites!';
                isFav = 'False';
            }
            toggleBtnFavourite();
            successMessageSweetAlert(message);
        },
        error: ajaxErrorSweetAlert
    });
}


//Star rating js
const stars = document.querySelectorAll('.star-rating img');

stars.forEach((star, starIndex1) => {
    //mouseover
    star.addEventListener(
        'mouseover',
        () => {
            if (!starClickFlag)
                starToggleSource(starIndex1);
        }
    );

    //mouseout
    star.addEventListener(
        'mouseout',
        () => {
            if (!starClickFlag) {
                stars.forEach((star) => {
                    removeActiveStarSource(star);
                });
            }
        }
    );
    //click
    star.addEventListener(
        'click',
        () => {
            if (userId == 0) {
                loginRequiredSweetAlert(loginPageLink, returnUrl);
                return;
            }
            if (isLoggedUserVolunteer === 'False') {
                title = 'Volunteer Registration Required';
                subTitle = 'You need to register as volunteer to give rating!';
                displayActionMessageSweetAlert(title, subTitle, 'info');
                return;
            }
            starToggleSource(starIndex1);

            Swal.fire({
                title: 'Are you sure?',
                text: "You can change rating any time!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: 'orange',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Confirm'
            }).then((result) => {
                if (result.isConfirmed) {
                    let rating = star.dataset.star;
                    if (starClickFlag) {
                        updateMissionRatingAjax(rating);
                    }
                    else {
                        saveMissionRatingAjax( rating );
                    }
                }
            })
        }
    );
});

function starToggleSource(starIndex1) {
    stars.forEach((star, starIndex2) => {
        starIndex1 >= starIndex2 ? addActiveStarSource(star) : removeActiveStarSource(star);
    });
}
function addActiveStarSource(star) {
    star.src = '/assets/selected-star.png';
}
function removeActiveStarSource(star) {
    star.src = '/assets/star-empty.png';
}

function saveMissionRatingAjax(rating) {
    let ratingLink = '/Volunteer/Mission/MissionRating';
    $.ajax({
        type: 'POST',
        url: ratingLink,
        data: { missionId, userId, rating },
        success: function (result) {
            let title = 'Thank you for taking time to rate mission!';
            let subTitle = 'Your rating has been saved.';
            displayActionMessageSweetAlert(title, subTitle, 'success');
            starClickFlag = true;
            starToggleSource(rating - 1);

            $('#star-rating-box').html(result);
        },
        error: ajaxErrorSweetAlert
    });
}

function updateMissionRatingAjax(rating) {
    let ratingLink = '/Volunteer/Mission/UpdateMissionRating';
    $.ajax({
        type: 'PUT',
        url: ratingLink,
        data: { missionId, userId, rating },
        success: function (result) {
            let title = 'Thank you for taking time to rate mission!';
            let subTitle = 'Your rating has been updated.';
            displayActionMessageSweetAlert(title, subTitle, 'success');
            starClickFlag = true;
            starToggleSource(rating - 1);
            $('#star-rating-box').html(result);
        },
        error: ajaxErrorSweetAlert
    });
}

function loadRelatedMissionsAjax( themeId ) {
    $.ajax({
        type: 'GET',
        url: '/Volunteer/Mission/RelatedMissionByTheme',
        data: { missionId, themeId },
        success: function (result) {
            $('#related-missions-section').html(result);
        },
        error: ajaxErrorSweetAlert
    });
}

//Comment Form
(btnPostComment).addEventListener('click', handleCommentPost);

function handleCommentPost() {

    if (userId == 0) {
        loginRequiredSweetAlert(loginPageLink, returnUrl);
        return;
    }
    if (isLoggedUserVolunteer === 'False') {
        title = 'Volunteer Registration Required';
        subTitle = 'You need to register as volunteer to comment!';
        displayActionMessageSweetAlert(title, subTitle, 'info');
        return;
    }

    if (isCommentExists) {
        let title = 'Already commented!';
        let subTitle = 'Your comment is pending for review. Please wait for approval!';
        displayActionMessageSweetAlert(title, subTitle, 'info');
        return;
    }
    let comment = commentBox.value;
    if (comment === null || comment.trim() === '' || comment.length <= 8) {
        commentErr.classList.remove('d-none');
        commentErr.textContent = 'Comment should be more than 8 character!';
        return;
    }
    commentErr.classList.add('d-none');

    addMissionCommentAjax( comment.trim() );
}

function addMissionCommentAjax(commentText) {
    $.ajax({
        type: 'POST',
        url: '/Volunteer/Mission/MissionComments',
        data: {
            userId, missionId, commentText
        },
        success: function (result) {
            let title = 'Comment sent for approval';
            let subTitle = 'Admin will approve your comment after review.';
            displayActionMessageSweetAlert(title, subTitle, 'success');
            $('#msn-comment').html(result);
        },
        error: ajaxErrorSweetAlert
    });
}

//Volunteer Pagination
$('#btn-vl-prev').on
(
        'click',
        () =>
        {
            if (currentVolunteerPage == 1) return;
            currentVolunteerPage--;
            loadRecentVolunteers();
        }
);

$('#btn-vl-next').on
(
    'click',
    () =>
    {
        if (currentVolunteerPage == totalVlPage) return;
        currentVolunteerPage++;
        loadRecentVolunteers();
    }
);

function loadRecentVolunteers() {

    if (totalVlPage == 0) { $().show(); }

    let low = (currentVolunteerPage - 1) * displayVolunteerCount;
    let high = currentVolunteerPage * displayVolunteerCount;
    let count = 0;
    $.each($('[data-vlNumber]'), (index, item) => {
        let vlNumber = $(item).data('vlnumber');
       
        if (vlNumber >= low && vlNumber < high) {
            count++;
            $(item).show();
        }
        else {
            $(item).hide();
        }
    });
    $('#recent-volunteers-count').text(`${low + 1} - ${low+count} Volunteers of ${totalVolunteersCount}`);
}

/*function handleVolunteerPaginationAjax()
{
    $.ajax({
        type: 'GET',
        url: '/Volunteer/Mission/RecentVolunteers',
        data:
        {
            missionId: missionId,
            page: currentVolunteerPage
        },
        success: function (result) {
            $('#msn-recent-volunteers').html(result);
        },
        error: ajaxErrorSweetAlert
    });
}*/

//Recommend to co-worker
const missionTitle = $('#mission-title').val();
$('#msn-recommend').on
(
    'click',
    () =>
    {
        if (userId == 0) {
            loginRequiredSweetAlert(loginPageLink, returnUrl);
            return;
        }
        handleRecommendToCoWorkerAjax();
    }
);

function handleRecommendToCoWorkerAjax() {
    $.ajax({
        type: 'GET',
        url: '/Volunteer/MissionInvite/MissionUsersInviteAsync',
        data: { userId, missionId },
        success: function (result) {
            $('#recommend-msn-modal').html(result);
            $('#recommendModal').modal('show');
            modalEventListener();
        },
        error: ajaxErrorSweetAlert
    });
}

function modalEventListener()
{
            let recommendList = [];
            $('#btn-recommend').on('click',
            () => {
                let count = $("input:checkbox[name='recommend-list']:checked").length;

                if (count == 0) {
                    let title = 'No co-worker selected';
                    let subTitle = 'You need to select at least one co-worker!';
                    displayActionMessageSweetAlert(title, subTitle, 'info');
                    return;
                }

                $("input:checkbox[name='recommend-list']:checked").each(function () {
                    recommendList.push($(this).val());
                });

                handleUserRecommendAjax(recommendList);
            });
}

function handleUserRecommendAjax(recommendList) {
    $('#recommendModal').modal('hide');
    displayActionMessageSweetAlert('Invite sent!', 'Mission invitation will be sent to your co-worker.', 'info');
    $.ajax({
        type: 'POST',
        data: {  userId, missionId, recommendList },
        url: '/Volunteer/MissionInvite/SendMissionInviteAsync',
        success:
        function (result) 
        {
        },
        error: ajaxErrorSweetAlert
    });
}


const btnContainer = document.querySelector('#btnContainer');

const editProfileLink = $('#editProfileUrl').val();

buttonApplyEvent();
buttonCancelEvent();

function buttonCancelEvent() {
    const btnApplicationCancel = document.querySelector('#btn-msn-cancel');
    if (btnApplicationCancel == undefined || btnApplicationCancel == null) return;
    btnApplicationCancel.addEventListener('click', () => {
        let missionId = $('#btn-msn-cancel').data('missionid');
        let userId = loggedUserId;
        Swal.fire({
            title: 'Are you sure?',
            text: "You may reapply for the mission till its application period is open!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#f88634',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Confirm'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'PATCH',
                    url: '/Volunteer/User/CancelMissionApplication',
                    data: { missionId, userId },
                    success: (_, __, xhr) => {
                        if (xhr.status === 204) {
                            displayActionMessageSweetAlert('Cancelled!', 'You application for mission is cancelled.', 'success');
                            createApplyButton();
                            buttonApplyEvent();
                            return;
                        }
                        errorMessageSweetAlert('Something went wrong during cancelling application!');
                    },
                    error: ajaxErrorSweetAlert
                });
            }
        })
    });

}

function buttonApplyEvent() {
    
    const btnApplicationApply = document.querySelector('#btn-msn-apply');
    if (btnApplicationApply == undefined || btnApplicationApply == null) return;

    btnApplicationApply.addEventListener('click', () => {
        let missionId = $('#btn-msn-apply').data('missionid');
        if (loggedUserId == 0) {
            loginRequiredSweetAlert(userLoginPageLink, returnUrl);
            return;
        }
        let userId = loggedUserId;    

        Swal.fire({
            title: 'Are you sure?',
            text: "You may cancel application for the mission before admin approves!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#f88634',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Confirm'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/Volunteer/User/ApproveMissionApplication',
                    data: { missionId, userId },
                    success: (result, _, xhr) => {
                        if (xhr.status === 204) {
                            requiredSweetAlert('Update Profile!', 'You need to edit your profile before applying for mission!', editProfileLink, 'Update Profile');
                            return;
                        }

                        displayActionMessageSweetAlert('Applied!', 'Admin will review your application for mission.', 'success');
                        createCancelButton();
                        buttonCancelEvent();
                    },
                    error: ajaxErrorSweetAlert
                });
            }
        })
    });
}


function createCancelButton() {
    $(btnContainer).empty();
    var button = document.createElement("button");
    button.setAttribute("class", "my-2 my-md-3 mx-auto");
    button.setAttribute("id", "btn-msn-cancel");
    button.setAttribute("data-missionid", missionId);
    button.innerHTML = "Cancel Application <img src='/assets/cancel-application.svg' alt='Cancel' height='15px' width='19px'>";

    // Append the button to a container element
    btnContainer.append(button);
}

function createApplyButton() {
    $(btnContainer).empty();
    var button = document.createElement("button");
    button.setAttribute("class", "my-2 my-md-3 mx-auto btn-g-orange");
    button.setAttribute("id", "btn-msn-apply");
    button.setAttribute("data-missionid", missionId);
    button.innerHTML = "Apply Now <img src='/assets/right-arrow.png' alt='Right Arrow'>";

    // Append the button to a container element
    btnContainer.append(button);
}


