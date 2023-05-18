const { Modal } = require("../lib/bootstrap/dist/js/bootstrap.esm");

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

//function hideSpinner() {
//    $("#spinner").addClass('d-none');
//    $('#loader').addClass('d-none');
//}

//function showSpinner() {
//    $("#spinner").removeClass('d-none');
//    $('#loader').addClass('d-none');
//}



const genericSearch = debounce(
    (pagedQuery, url, action) => {
        console.log(pagedQuery);
        $.ajax({
            type: 'GET',
            url: url,
            data: pagedQuery,
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
    loadSkillOnDOM();
    registerEditAndDeleteEvents();
    registerToolTips();
}

//Skill Ajax

const loadAllSkillUrl = '/Admin/Skill/Index';
const addSkillUrl = '/Admin/Skill/Add';
const editSkillUrl = '/Admin/Skill/Edit';
const checkUniqueSkillTitleUrl = '/Admin/Skill/UniqueSkill';
loadSkillsAjax();

function loadSkillsAjax() {
    $.ajax(
        {
            type: 'GET',
            url: loadAllSkillUrl,
            success: (result) => {
                adminMenuContent.html(result);
                loadSkillOnDOM();
            }
        });
}

function loadSkillOnDOM() {
    const skillCount = $('#skill-count').val();
    //if (skillCount == 0) {
    //    $('#msn-skill-search').prop('readonly', true);
    //    return;
    //}
    registerToolTips();
    registerSkillSearchEvent('msn-skill-search');
    registerBtnAddSkill();
    registerEditAndDeleteEvents();
}

function registerSkillSearchEvent(id) {
    let searchBox = document.getElementById(id);
    const pagedQuery = {};
    searchBox.addEventListener('input', e => {
        searchText = e.target.value;
        $('.spinner-control').removeClass('opacity-0');
        $('.spinner-control').addClass('opacity-1');
        pagedQuery.Key = searchText.trim();
        genericSearch(pagedQuery, '/Admin/Skill/Search', 'skill');
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
        if ($(form).valid()) {

            let skillVm = new URLSearchParams($(form).serialize());

            let skillName = skillVm.get('Title');

            let isSkillUnique = checkIsSkillNameUnique(skillName, skillId);

            if (isSkillUnique) {
                $('#skill-title-err').text(`${skillName} already exists. Enter unique skill.`).show();
                //displayActionMessageSweetAlert(`Skill '${skillName}' Already Exists!`, 'Please enter unique skill name.', 'error');
                return;
            }
            $(bModal).modal('hide');
            $.ajax({
                type: type,
                url: url,
                data: $(form).serialize(),
                success: function (result) {
                    adminMenuContent.html(result);
                    loadSkillsOnDOM();
                    $(form)[0].reset();
                    successMessageSweetAlert(skillName + " " + message);
                },
                error: ajaxErrorSweetAlert
            });
        }
    });
}

function checkIsSkillNameUnique(skillName, skillId = 0) {
    let isSkillUnique = false;
    $.ajax({
        async: false,
        type: 'GET',
        data: { skillName: skillName, skillId: skillId },
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