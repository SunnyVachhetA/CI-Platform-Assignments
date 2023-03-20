let missionId = 0;
let userId = $('#logged-user-id').val();
let loginPageLink = $('#login-page-link').val();
let favLink = $('#add-fav-link').val();
let isFav = $('#is-favourite').val();
const btnFavourite = document.querySelector('#btn-add-to-favourite');
let isLoggedUserVolunteer = $('#isLoggedUserVolunteer').val();
let loggedUserRating = 0;
let starClickFlag = false;
$(document).ready(
    () =>
    {
        missionId = $('#msn-details-id').val();
        loggedUserRating = $('#logged-user-rating').val();
        starClickFlag = (loggedUserRating != 0) ? true : false;
        toggleBtnFavourite();

        loadMissionRatingAjax();
    }
);

function loadMissionRatingAjax() {
    console.log( missionId );
    $.ajax({
        type: 'GET',
        data: { missionId },
        url: '/Volunteer/Mission/MissionRating',
        success: function (result) { $('#star-rating-box').html(result); },
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
        loginRequiredSweetAlert(loginPageLink);
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
                console.log('Created');
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
                loginRequiredSweetAlert(loginPageLink);
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

