const dateContainer = document.getElementById('current-date');
const btnHamburger = document.getElementById('btn-sidebar-hamburger');
const adminSidebar = document.querySelector('.sidebar');
const sidebarClose = document.querySelector('#sidebar-close');
let isSidebarOpen = true;
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

function loadUsersAjax() {
    $.ajax({
        type: 'GET',
        url: '/Admin/Home/Users',
        success: function (result) {
            $('#admin-menu-content').html(result);
        },
        error: ajaxErrorSweetAlert
    });
}
