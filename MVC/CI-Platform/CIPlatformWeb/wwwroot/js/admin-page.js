const dateContainer = document.getElementById('current-date');
const btnHamburger = document.getElementById('btn-sidebar-hamburger');
const adminSidebar = document.querySelector('.sidebar');
const sidebarClose = document.querySelector('#sidebar-close');
const rightContent = document.querySelector('#admin-right-content');
let isSidebarOpen = true;
let searchText = '';

function vhToPixels(vh) {
    return Math.round(window.innerHeight / (100 / vh));
}
function updateSidebarHeight() {
    // Get the height of the content
    const contentHeight = rightContent.offsetHeight;
    let height = (vhToPixels(100) > contentHeight) ? vhToPixels(100) : contentHeight;
    // Set the height of the sidebar to match the content height
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
});

sidebarClose.addEventListener('click', () => {
    adminSidebar.classList.remove('show');
    adminSidebar.classList.add('hide');
});

//sidebar menu selection
let prevMenu = "user";
const sidebarMenu = document.querySelectorAll('.admin-menu div');

$(sidebarMenu).each((_, item) => {
    $(item).click(() => {
        let currentMenu = $(item).data('menu');

        if (currentMenu === prevMenu)
            return;

        toggleMenuActive(prevMenu, false);
        toggleMenuActive(currentMenu, true);
        prevMenu = currentMenu;
    });
});

function toggleMenuActive(menu, isToggle) {
    let src = '';
    switch (menu) {
        case "user":
            src = '';
            if (isToggle)
                src = '/assets/user-fill.svg';
            else
                src = '/assets/user-empty.svg';
            changeMenu(menu, src);
            break;

        case "cms":
            src = '';
            if (isToggle)
                src = '/assets/page.png';
            else
                src = '/assets/empty-page.svg';
            changeMenu(menu, src);
            break;

        case "mission":
            src = '';
            if (isToggle)
                src = '/assets/target-fill.svg';
            else
                src = '/assets/empty-target.png';
            changeMenu(menu, src);
            break;

        case "application":
            src = '';
            if (isToggle)
                src = '/assets/folder-fill.svg';
            else
                src = '/assets/folder-empty.svg';
            changeMenu(menu, src);
            break;

        case "banner":
            src = '';
            if (isToggle)
                src = '/assets/banner-fill.svg';
            else
                src = '/assets/banner-empty.svg';
            changeMenu(menu, src);
            break;

        case "theme":
            src = '';
            if (isToggle)
                src = '/assets/theme-fill.svg';
            else
                src = '/assets/themes.png';
            changeMenu(menu, src);
            break;

        case "story":
            src = '';
            if (isToggle)
                src = '/assets/story-fill.svg';
            else
                src = '/assets/tale.png';
            changeMenu(menu, src);
            break;

        case "skill":
            src = '';
            if (isToggle)
                src = '/assets/tools-fill.svg';
            else
                src = '/assets/tools-empty.svg';
            changeMenu(menu, src);
            break;
    }

}

function changeMenu(menu, imgSrc) {
    let option = $('[data-menu=' + menu + ']');
    $(option).toggleClass('active');
    let img = $(option).children().closest('img');

    $(img).attr('src', imgSrc);
}


//Ajax and other important js 
$(document).ready
(
    () => {
        loadUsersAjax();
    }
);

function loadUsersOnDOM(result) {
    $('#admin-menu-content').html(result);
    createPagination(5);
    updateSidebarHeight();
    registerUserDeleteAndRestoreClickEvents();
    registerUserSearchEvent();
    registerAllUsersButton();
    
    $('#usr-search').val(searchText);
}

function registerAllUsersButton() {
    $('#btn-load-users').click(() => { $('#usr-search').val(''); loadUsersAjax(); });
}

const searchUserWithDebounceAjax = debounce(
    (searchText) => {

        if (searchText.trim().length == 0) {
            $('.spinner-control').removeClass('opacity-1');
            $('.spinner-control').addClass('opacity-0');
            return;
        }
    $.ajax({
        type: 'GET',
        url: '/Admin/User/SearchUser',
        data: { searchKey: searchText.trim() },
        success: function (result) {
            loadUsersOnDOM(result);
            $('.spinner-control').removeClass('opacity-1');
            $('.spinner-control').addClass('opacity-0');
        },
        error: ajaxErrorSweetAlert
    });
})
function debounce(cb, delay = 1000) {
    let timeout 

    return (...args) => {
        clearTimeout(timeout);
        timeout = setTimeout(() => {
            cb(...args)
        }, delay);
    }
}

function registerUserSearchEvent() {
    let searchBox = document.getElementById('usr-search');

    searchBox.addEventListener('input', e => {
        searchText = e.target.value;
        $('.spinner-control').removeClass('opacity-0');
        $('.spinner-control').addClass('opacity-1');
        searchUserWithDebounceAjax(searchText);
    });
}
function loadUsersAjax() {
    $.ajax({
        type: 'GET',
        url: '/Admin/User/Users',
        success: function (result) {
            loadUsersOnDOM(result);
        },
        error: ajaxErrorSweetAlert
    });
}

function registerUserDeleteAndRestoreClickEvents() {
    $('.tbl-user .usr-delete').each((_, item) => {

        $(item).click(() => {
            let userId = $(item).data('userid');
            Swal.fire({
                title: 'Are you sure?',
                text: "You can change user status anytime!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#f88634',
                cancelButtonColor: '#d33',
                confirmButtonText: 'De-Activate User Account'
            }).then((result) => {
                if (result.isConfirmed) {
                    handleUserDeleteAjax(userId);
                }
            })
        });

    })

    $('.tbl-user .usr-restore').each((_, item) => {
        
        $(item).click(() => {
            let userId = $(item).data('userid');
            Swal.fire({
                title: 'Are you sure?',
                text: "You can change user status anytime!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#f88634',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Activate User Account'
            }).then((result) => {
                if (result.isConfirmed) {
                    handleUserRestoreAjax(userId);
                }
            })
        });

    });
}

function handleUserDeleteAjax(userId) {
    changeUserStatus('/Admin/User/DeleteUser', userId, 'User account de-activated successfully!');
}

function handleUserRestoreAjax(userId) {
    changeUserStatus('/Admin/User/RestoreUser', userId, 'User account activated successfully!');
}

function changeUserStatus(url, userId, message) {
    $.ajax({
        type: 'PATCH',
        url: url,
        data: { userId: userId },
        success: function (result, _, status) {
            if (status === 204) { errorMessageSweetAlert(); }
            else {
                loadUsersOnDOM(result);
                successMessageSweetAlert(message);
            }
        },
        error: ajaxErrorSweetAlert
    });
}