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


//Story listing pagination

const totalStoryCount = $('#total-story-count').val();
let currentPageNumber = 1;
const storyPerPage = 2;
const paginationContainer = $('#pagination-container');
const totalPage = Math.ceil(totalStoryCount / storyPerPage);

createPaginationButton();
handleDisplayStoryCard();

function createPaginationButton() {
    if (totalStoryCount <= storyPerPage)
        return;

    createLeftButton(createHTMLPageButton());
    createPageNumberedButton();
    createRightButton(createHTMLPageButton());
}

function handleDisplayStoryCard() {
    let low = (currentPageNumber - 1) * storyPerPage;
    let high = (currentPageNumber) * storyPerPage;

    $.each($('[data-storynumber]'), (_, item) => {
        let storyNumber = $(item).data('storynumber');

        if (storyNumber >= low && storyNumber < high) {
            $(item).show();
        }
        else {
            $(item).hide();
        }
    });
    $(document).scrollTop(0);
    $(`[data-page='${currentPageNumber}'`).addClass('active');
}

function createHTMLPageButton() {
    var btnStyle = $('<button>', { 'class': 'btn p-2 px-3 d-flex align-items-center border bg-transparent' });
    return btnStyle;
}

function createLeftButton(btnLeft) {
    var imgLeft = $('<img>');
    imgLeft.attr('src', '/assets/left.png');
    btnLeft.append(imgLeft);
    paginationContainer.append(btnLeft);
    $(btnLeft).click(() => {
        if (currentPageNumber == 1) return;
        $(`[data-page='${currentPageNumber}'`).removeClass('active');
        currentPageNumber--;
        handleDisplayStoryCard();
    });
}

function createPageNumberedButton() {
    if (totalPage <= 5) {
        for (let i = 1; i <= totalPage; i++) {
            let btnPage = createHTMLPageButton();
            btnPage.text(i);
            btnPage.attr('data-page', i);
            paginationContainer.append(btnPage);
            $(btnPage).click(onPageNumberButtonClick);
       
        }
    }
}

function onPageNumberButtonClick() {

    $(`[data-page='${currentPageNumber}'`).removeClass('active');
    currentPageNumber = $(this).data('page');
    $(`[data-page='${currentPageNumber}'`).addClass('active');
    handleDisplayStoryCard();
}

function createRightButton(btnRight) {
    var imgLeft = $('<img>');
    imgLeft.attr('src', '/assets/right-arrow1.png');
    btnRight.append(imgLeft);
    paginationContainer.append(btnRight);
    $(btnRight).click(() => {
        if (currentPageNumber == totalPage) return;
        $(`[data-page='${currentPageNumber}'`).removeClass('active');
        currentPageNumber++;
        handleDisplayStoryCard();
    });
}

//Story listing pagination ends