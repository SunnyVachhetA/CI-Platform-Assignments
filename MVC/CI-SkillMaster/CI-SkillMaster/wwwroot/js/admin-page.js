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

//$(overlayContainer).click(() => {
//    overlayContainer.classList.add('d-none');
//    adminSidebar.classList.remove('show');
//    adminSidebar.classList.add('hide');
//});

function hideSpinner() {
    $(".spinner-overlay").addClass('d-none');
    $('#load-spinner').addClass('d-none');
}

function showSpinner() {
    $(".spinner-overlay").removeClass('d-none');
    $('#load-spinner').removeClass('d-none');
}



const genericSearch = debounce(
    (url, action) => {
        paginationQuery.PageNumber = 1;
        currentPage = 1;
        $.ajax({
            type: 'GET',
            url: url,
            data: paginationQuery,
            success: function (result) {
                $('#table-data').html(result);
                $('.spinner-control').removeClass('opacity-1');
                $('.spinner-control').addClass('opacity-0');
                addAllEvents(action);
            },
            error: (xhr, status, error) => {
                $('.spinner-control').removeClass('opacity-1');
                $('.spinner-control').addClass('opacity-0');
                ajaxErrorSweetAlert(xhr, status, error);
            }
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

function addAllEvents(menu) {
    const skillCount = $('#skill-count').val();
    registerSkillTableEvent();
    
    createPagination(skillCount, registerSkillTableEvent);
}

function registerSkillTableEvent() {
    registerToolTips();
    registerEditAndDeleteEvents();
}

//Skill Ajax

const loadAllSkillUrl = '/Admin/Skill/Index';
const addSkillUrl = '/Admin/Skill/Add';
const editSkillUrl = '/Admin/Skill/Edit';
const checkUniqueSkillTitleUrl = '/Admin/Skill/UniqueSkill';

const paginationQuery = {
    PageNumber: 1,
    PageSize: 5
};
loadSkillsAjax();
function loadSkillsAjax() {
    paginationQuery.IsPaging = false;
    paginationQuery.PageNumber = 1;
    currentPage = 1;
    $.ajax(
        {
            type: 'GET',
            url: loadAllSkillUrl,
            data: paginationQuery,
            beforeSend: showSpinner,
            success: (result) => {
                adminMenuContent.html(result);
                loadSkillOnDOM();
            },
            complete: hideSpinner
        });
}

function loadSkillOnDOM() {
    const skillCount = $('#skill-count').val();
    //if (skillCount == 0) {
    //    $('#msn-skill-search').prop('readonly', true);
    //    return;
    //}
    createPagination(skillCount, registerSkillTableEvent);
    registerToolTips();
    registerSkillSearchEvent('msn-skill-search');
    registerBtnAddSkill();
    registerEditAndDeleteEvents();
}

function registerSkillSearchEvent(id) {
    let searchBox = document.getElementById(id);
    searchBox.addEventListener('input', e => {
        searchText = e.target.value;
        $('.spinner-control').removeClass('opacity-0');
        $('.spinner-control').addClass('opacity-1');
        paginationQuery.Key = searchText.trim();
        genericSearch('/Admin/Skill/Search', 'skill');
    });
}

function registerBtnAddSkill() {
    $('#btn-skill-add').click(() => {
        $.ajax({
            type: 'GET',
            url: addSkillUrl,
            success: (result) => {
                modalContainer.html(result);
                $('#addSkillModal').modal('show');
                registerSkillFormSubmit('#form-add-skill', 'POST', addSkillUrl, '#addSkillModal', 'Skill Added Successfully.');
            },
            error: ajaxErrorSweetAlert
        });
    });
}

function registerEditAndDeleteEvents() {
    $('.skill-edit').each((_, item) => {
        $(item).click(() => {
            const skillId = $(item).data('skillid');
            $.ajax({
                type: 'GET',
                url: editSkillUrl,
                data: { skillId },
                success: (result) => {
                    modalContainer.html(result);
                    $('#editSkillModal').modal('show');
                    registerSkillFormSubmit('#form-edit-skill', 'PUT', editSkillUrl, '#editSkillModal', 'Skill Updated Successfully.', skillId);
                },
                error: ajaxErrorSweetAlert
            });
        });
    });
}

function registerSkillFormSubmit(form, type, url, bModal, message, skillId = 0) {
    $(form).on('submit', (e) => {
        e.preventDefault();
        $(form).validate();
        if (!$(form).valid()) return;

        let skillVm = new URLSearchParams($(form).serialize());

        console.log(skillVm);
        let skillName = skillVm.get('Title').trim();

        let isSkillUnique = checkIsSkillNameUnique(skillName, skillId);

        if (!isSkillUnique) {
            $('#skill-title-err').text(`${skillName} already exists. Enter unique skill.`).show();
            $('#title').val(skillName);
            //displayActionMessageSweetAlert(`Skill '${skillName}' Already Exists!`, 'Please enter unique skill name.', 'error');
            return;
        }
        skillVm.set('Title', skillName);
        $(bModal).modal('hide');
        $.ajax({
            type: type,
            url: url,
            data: skillVm.toString(),
            success: function (result) {
                $(form)[0].reset();
                successMessageSweetAlert(skillName + " " + message);
                loadSkillsAjax();
            },
            error: ajaxErrorSweetAlert
        });

    });
}

function checkIsSkillNameUnique(skillName, skillId = 0) {
    let isSkillUnique = false;
    $.ajax({
        async: false,
        type: 'GET',
        data: { title: skillName, id: skillId },
        url: checkUniqueSkillTitleUrl,
        success: function (result) {
            isSkillUnique = result;
        },
        error: ajaxErrorSweetAlert
    });
    return isSkillUnique;
}
function registerToolTips() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))
}