const gridView = document.querySelector('#grid-view-msn');
const listView = document.querySelector('#list-view-msn');

const btnGrid = document.querySelector('#grid-view');
const btnList = document.querySelector('#list-view');


const displayClass = "d-none";


$(btnGrid).on('click', () => {
    if ($('#grid-view-msn').hasClass(displayClass)) {

        $('#grid-view-msn').toggleClass(displayClass);

        $('#list-view-msn').addClass(displayClass);
    }
});

$(btnList).on('click', () => {

    if ($('#list-view-msn').hasClass(displayClass)) {
        
        $('#list-view-msn').toggleClass(displayClass);

        $('#grid-view-msn').addClass(displayClass);
    }
});

//Add and remove from favourite

function addToFavouriteHandler() {
    const favButtons = $('.msn-favourite');
    const favListBtn = $('msn-list-favourite');
    handleFavouriteButtonClick(favButtons);
    handleFavouriteButtonClick(favListBtn);
}

function handleFavouriteButtonClick(btnFav) {
    $.each(btnFav, (_, item) => {
        $(item).click
            (
                () => {
                    if (loggedUserId == 0) {
                        loginRequiredSweetAlert(userLoginPageLink);
                        return;
                    }
                    let missionId = $(item).data('missionid');
                    let isFav = $(item).hasClass('bg-danger');
                    if ($(item).hasClass('bg-danger'))
                        $(item).removeClass('bg-danger');
                    else
                        $(item).addClass('bg-danger')

                    handleAddToFavouriteAjax(missionId, loggedUserId, isFav, item);
                }

            );
    });
}

function handleAddToFavouriteAjax(missionId, loggedUserId, isFav, item)
{
    $.ajax({
        type: 'GET',
        url: '/Volunteer/User/AddMissionToFavourite',
        data: {
            missionId: missionId,
            userId: loggedUserId,
            isFav: isFav
        },
        success: function (_, _, jqXHR)
        {
            if (jqXHR.status === 201)
                $(item).addClass('bg-danger')
            else
                $(item).removeClass('bg-danger');
        },
        error: ajaxErrorSweetAlert
        });
}


