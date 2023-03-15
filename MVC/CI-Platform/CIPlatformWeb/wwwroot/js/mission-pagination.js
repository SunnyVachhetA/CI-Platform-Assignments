
const paginationContainer = $('#msn-pagination');
let prevPage;
let prevBtn;
let totalPage;
let currentPageNumber = 1;

function missionPagination() {
    $(paginationContainer).empty();
    const missionCount = document.querySelector('#mission-count');
    let count = missionCount.value;
    if ( count <= 9 )
        return;

    createLeftButton(createHTMLPageButton());
    createPageNumberedButton( count );
    createRightButton(createHTMLPageButton());
}

function createPageNumberedButton(count)
{
    totalPage = Math.ceil(count / 9);

    if (totalPage <= 5)
    {
        for (let i = 1; i <= totalPage; i++)
        {
            let btnPage = createHTMLPageButton();
            btnPage.text(i);
            btnPage.attr('data-page', i);
            paginationContainer.append(btnPage);
            
            $(btnPage).click( onPageNumberButtonClick )
        }
    }
}

function onPageNumberButtonClick() {
    let currentBtn = $(this);
    if (prevBtn !== undefined) {
        if (currentBtn == prevBtn) return;
        $(prevBtn).removeClass('active');
        $(currentBtn).addClass('active');
        currentPageNumber = $(this).data('page');
        prevBtn = currentBtn;
    }

    else {
        currentPageNumber = $(this).data('page');
        prevBtn = $(this);
        $(this).addClass('active');
    }
    filterMissionCardAjax();
}

function createHTMLPageButton() {
    var btnStyle = $('<button>', { 'class': 'btn p-2 px-3 d-flex align-items-center border bg-transparent' });
    return btnStyle;
}

function createLeftButton(btnLeft) {
    var imgLeft = $('<img>');
    imgLeft.attr('src', './assets/left.png');
    btnLeft.append(imgLeft);
    paginationContainer.append(btnLeft);
    $(btnLeft).click(() => {

        if (currentPageNumber == 1) return;

        currentPageNumber--;
        filterMissionCardAjax(currentPageNumber);
    });
}

function createRightButton(btnRight) {
    var imgLeft = $('<img>');
    imgLeft.attr('src', './assets/right-arrow1.png');
    btnRight.append(imgLeft);
    paginationContainer.append(btnRight);
    $(btnRight).click(() => {

        if (currentPageNumber == totalPage) return;

        currentPageNumber++;
        filterMissionCardAjax(currentPageNumber);
    });
}
