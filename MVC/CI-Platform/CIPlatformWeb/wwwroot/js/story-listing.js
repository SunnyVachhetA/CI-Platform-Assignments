
const storySubmitRequest = $('#story-submit-request').val();

if (storySubmitRequest !== undefined && storySubmitRequest !== '')
    displayActionMessageSweetAlert('Story Sent!', storySubmitRequest, 'success');

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


//Story listing pagination start
const pageNumberDisplay = 5;
const totalStoryCount = $('#total-story-count').val();
let currentPageNumber = 1;
const storyPerPage = 2;
const paginationContainer = $('#pagination-container');
const totalPage = Math.ceil(totalStoryCount / storyPerPage);

let currentPageSet = 1;
const totalPageSet = Math.ceil(totalPage / pageNumberDisplay);

createPaginationButton();
handleDisplayStoryCard();

if (totalPageSet > 1) handleButtonDisplayPagination();

function createPaginationButton() {
    if (totalStoryCount <= storyPerPage)
        return;
    if (totalPageSet > 1) createPrevButton(createHTMLPageButton());
    createLeftButton(createHTMLPageButton());
    createPageNumberedButton();
    createRightButton(createHTMLPageButton());
    if (totalPageSet > 1) createNextButton(createHTMLPageButton());
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
    
        for (let i = 1; i <= totalPage; i++) {
            let btnPage = createHTMLPageButton();
            btnPage.text(i);
            btnPage.attr('data-page', i);
            paginationContainer.append(btnPage);
            $(btnPage).click(onPageNumberButtonClick);
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

function createPrevButton(btnPrev) {
    var imgPrev = $('<img>');
    imgPrev.attr('src', '/assets/previous.png');
    btnPrev.append(imgPrev);
    paginationContainer.append(btnPrev);
    $(btnPrev).click(() => {
        if (currentPageSet == 1) return;
        $(`[data-page='${currentPageNumber}'`).removeClass('active');
        currentPageSet--;
        handleButtonDisplayPagination();
        handleDisplayStoryCard();
    });
}

function createNextButton(btnNext) {
    var imgNext = $('<img>');
    imgNext.attr('src', '/assets/next.png');
    btnNext.append(imgNext);
    paginationContainer.append(btnNext);
    $(btnNext).click(() => {
        if (currentPageSet == totalPageSet) return;
        $(`[data-page='${currentPageNumber}'`).removeClass('active');
        currentPageSet++;
        handleButtonDisplayPagination();
        handleDisplayStoryCard();
    });
}

function handleButtonDisplayPagination() {
    let high = currentPageSet * pageNumberDisplay;
    let low = high - pageNumberDisplay + 1;
    currentPageNumber = low;

    $.each($('[data-page]'), (_, item) => {
        let btnNumber = $(item).data('page');
        if (btnNumber >= low && btnNumber <= high) {
            $(item).removeClass('d-none');
           
        }
        else {
            $(item).addClass('d-none');
        }
    });

}

//Story listing pagination ends