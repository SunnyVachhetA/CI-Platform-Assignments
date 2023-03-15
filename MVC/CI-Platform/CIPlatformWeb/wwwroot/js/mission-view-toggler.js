const gridView = document.querySelector('#grid-view-msn');
const listView = document.querySelector('#list-view-msn');

const btnGrid = document.querySelector('#grid-view');
const btnList = document.querySelector('#list-view');


const displayClass = "d-none";

/*btnGrid.addEventListener(
    'click',
    () => {
        const classList = gridView.classList;
        if (classList.contains(displayClass)) {
            classList.toggle(displayClass);
            listView.classList.add(displayClass);
        }
    }
);

btnList.addEventListener(
    'click',
    () => {
        const classList = listView.classList;
        if (classList.contains(displayClass)) {
            classList.toggle(displayClass);
            gridView.classList.add(displayClass);
        }
    }
);*/

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

