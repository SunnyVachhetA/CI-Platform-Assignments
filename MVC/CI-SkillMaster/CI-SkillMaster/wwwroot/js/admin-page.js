const dateContainer = document.getElementById('current-date');
const btnHamburger = document.getElementById('btn-sidebar-hamburger');
const adminSidebar = document.querySelector('.sidebar');
const sidebarClose = document.querySelector('#sidebar-close');
const rightContent = document.querySelector('#admin-right-content');
const overlayContainer = document.querySelector('#admin-overlay');
const modalContainer = $('#partial-modal-container');
const adminMenuContent = $('#admin-menu-content');

let isSidebarOpen = true;
let searchText = '';
function vhToPixels(vh) {
    return Math.round(window.innerHeight / (100 / vh));
}
function updateSidebarHeight() {
    // Get the height of the content
    const contentHeight = rightContent.offsetHeight;
    let height = (vhToPixels(100) > contentHeight) ? vhToPixels(100) : contentHeight;
    adminSidebar.style.height = `${height}px`;
}

const formatter = new Intl.DateTimeFormat('en-US', {
    weekday: 'long',
    month: 'long',
    day: 'numeric',
    year: 'numeric',
    hour: 'numeric',
    minute: 'numeric',
    hour12: true
});
getCurrentDateTime();
function getCurrentDateTime() {
    const date = new Date();
    const formattedDate = formatter.format(date);
    dateContainer.textContent = formattedDate;
}
setInterval(getCurrentDateTime, 1000);

btnHamburger.addEventListener('click', () => {
    adminSidebar.classList.remove('hide');
    adminSidebar.classList.add('show');
    overlayContainer.classList.remove('d-none');
});

sidebarClose.addEventListener('click', () => {
    adminSidebar.classList.remove('show');
    adminSidebar.classList.add('hide');
    overlayContainer.classList.add('d-none');
});

$(overlayContainer).click(() => {
    overlayContainer.classList.add('d-none');
    adminSidebar.classList.remove('show');
    adminSidebar.classList.add('hide');
});

function hideSpinner() {
    $("#spinner").addClass('d-none');
    $('#loader').addClass('d-none');
}

function showSpinner() {
    $("#spinner").removeClass('d-none');
    $('#loader').addClass('d-none');
}