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
        console.log('hello');
        loginRequiredSweetAlert();
    });

//Share Story Button Complete