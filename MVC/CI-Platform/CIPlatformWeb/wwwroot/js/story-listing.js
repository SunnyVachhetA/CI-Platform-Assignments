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
let addStoryPageUrl = $('#add-story-page-url').val();
$('#btn-share-story').on('click',
    () => {
        if (loggedUserId === undefined || loggedUserId == 0) {
            loginRequiredSweetAlert(userLoginPageLink);
            return;
        }
        
        handleRedirectToAddStoryPage();
    });

function handleRedirectToAddStoryPage()
{
    window.location.href = addStoryPageUrl;
}
//Share Story Button Complete