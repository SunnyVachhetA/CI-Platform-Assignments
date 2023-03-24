storyCards = document.querySelectorAll('.card');
viewStoryBtn = document.querySelector('.btn-story-center');

storyCards.forEach
    (card => {
        card.addEventListener('mouseover', () => {
            let btn = card.querySelector('.btn-story-center');
            btn.classList.remove("d-none");
        });
        card.addEventListener('mouseleave', () => {
            let btn = card.querySelector('.btn-story-center');
            btn.classList.add("d-none");
        });
    });



//For Share Story Button

$('#btn-share-story').on('click',
    () => {
        if (userId == 0) {
            loginRequiredSweetAlert();
            return;
        }

        handleShareStoryAjax();
    });

function handleShareStoryAjax() {
    $.ajax({
        type: 'GET',
        url: '/Volunteer/Story/AddStoryPage',
        data: { userId },
        success: function () { },
        error: ajaxErrorSweetAlert
        });
}

//Share Story Button Complete