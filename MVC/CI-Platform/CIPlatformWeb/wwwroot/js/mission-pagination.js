
const paginationContainer = $('#msn-pagination');
let prevPage;
let prevBtn;
currentPageNumber = 1;

function missionPagination() {
const missionCount = document.querySelector('#mission-count');
    let count = missionCount.value;
    if (count <= 9) {
        $(paginationContainer).empty();
        return;
    }

    totalPage = Math.ceil(count / 9);
    $(paginationContainer).empty();
    createLeftButton(createHTMLPageButton());
    createPageNumberedButton(  );
    createRightButton(createHTMLPageButton());
    $(`[data-page='${currentPageNumber}'`).addClass('active');

}

function createPageNumberedButton()
{
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
    
            $(`[data-page='${currentPageNumber}'`).removeClass('active');
        currentPageNumber = $(this).data('page');
        $(`[data-page='${currentPageNumber}'`).addClass('active');
    filterMissionCardAjax();
    $(document).scrollTop(0);
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
