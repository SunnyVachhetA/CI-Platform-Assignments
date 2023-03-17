let missionId = 0;
let userId = $('#logged-user-id').val();
let loginPageLink = $('#login-page-link').val();
let favLink = $('#add-fav-link').val();
let isFav = $('#is-favourite').val();
const btnFavourite = document.querySelector('#btn-add-to-favourite');
$(document).ready(
    () =>
    {
        missionId = $('#msn-details-id').val();   

        toggleBtnFavourite();
    }
);

function toggleBtnFavourite() {
    let favImage = btnFavourite.getElementsByTagName("img")[0];
    if (isFav) {
        $('#btn-add-to-favourite').addClass('active-fav');
        favImage.src = '/assets/heart-992.png';
        favImage.style.height = '22px';
        favImage.style.width = '22px';
    }
    else {
        $('#btn-add-to-favourite').removeClass('active-fav');
        favImage.src = '/assets/heart1.png';
    }

}

btnFavourite.addEventListener('click', () => {
    if (userId == 0) {
        Swal.fire({
            icon: 'info',
            title: 'Login Required!',
            text: 'You need to login before adding favourite mission.',
            footer: `<a href="${loginPageLink}">Login Here</a>`
        });
    }
    else
    {
        addMissionToFavouritesAjax();
    }
});

function addMissionToFavouritesAjax()
{
    console.log(favLink);
    console.log(isFav);
    $.ajax({
        type: 'GET',
        url: favLink,
        data: { missionId, userId, isFav },
        success: function (result) {
            console.log(result);
            let message = 'Mission added to your favourites..';
            successMessageSweetAlert(message);
        },
        error: function (xhr, status, error) {
            console.log("Error: " + status + " - " + error);
            errorMessageSweetAlert();
        }
    });
}

