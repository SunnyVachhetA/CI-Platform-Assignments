﻿const dateContainer = document.getElementById('current-date');
const btnHamburger = document.getElementById('btn-sidebar-hamburger');
const adminSidebar = document.querySelector('.sidebar');
const sidebarClose = document.querySelector('#sidebar-close');
const rightContent = document.querySelector('#admin-right-content');
const overlayContainer = document.querySelector('#admin-overlay');
const modalContainer = $('#partial-modal-container');
const adminMenuContent = $('#admin-menu-content');

const resizeObserver = new ResizeObserver(entries => {
    for (let entry of entries) {
        updateSidebarHeight();
    }
});

// Start observing the element
resizeObserver.observe(rightContent);
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
            if (isToggle) {
                src = '/assets/user-fill.svg';
                loadUsersAjax();
            }
            else
                src = '/assets/user-empty.svg';
            changeMenu(menu, src);
            break;

        case "cms":
            src = '';
            if (isToggle) {
                src = '/assets/page.png';
                loadCMSAjax();
            }
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
            if (isToggle) {
                src = '/assets/banner-fill.svg';
                loadBannersAjax();
            }
            else
                src = '/assets/banner-empty.svg';
            changeMenu(menu, src);
            break;

        case "theme":
            src = '';
            if (isToggle) {
                src = '/assets/theme-fill.svg';
                loadThemesAjax();
            }
            else
                src = '/assets/themes.png';
            changeMenu(menu, src);
            break;

        case "story":
            src = '';
            if (isToggle) {
                src = '/assets/story-fill.svg';
                loadStoriesAjax();
            }
            else
                src = '/assets/tale.png';
            changeMenu(menu, src);
            break;

        case "skill":
            src = '';
            if (isToggle) {
                src = '/assets/tools-fill.svg';
                loadSkillsAjax();
            }
            else
                src = '/assets/tools-empty.svg';
            changeMenu(menu, src);
            break;

        case "contact":
            src = '';
            if (isToggle) {
                src = '/assets/contact-fill.svg';
                loadContactUsAjax();
            }
            else {
                src = '/assets/contact-empty.svg';
            }
            changeMenu(menu, src);
            break;

        case "timesheet":
            src = '';
            if (isToggle) {
                src = '/assets/timesheet-fill.svg';
                loadHourTimesheetAjax();
            }
            else {
                src = '/assets/timesheet-empty.svg';
            }
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
        loadUsersAjax();


function loadUsersOnDOM(result) {
    $('#admin-menu-content').html(result);
    createPagination(5);
    //updateSidebarHeight();
    registerUserDeleteAndRestoreClickEvents();
    registerUserSearchEvent();
    //registerAllUsersButton();
    $('#usr-search').val(searchText);
}

function registerAllUsersButton() {
    $('#btn-load-users').click(() => { $('#usr-search').val(''); loadUsersAjax(); });
}

const searchUserWithDebounceAjax = debounce(
    (searchText) => {
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
    tinymce.remove('#description');
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

//-----------------------------------------
//Generic methods
function addAllEvents(action) {
    switch (action) {
        case "cms":
            break;
        case "theme":
            loadThemesOnDOM();
            break;
        case "skill":
            loadSkillsOnDOM();
            break;

        case "story":
            loadStorysOnDOM();
            break;

        case "timesheet":
            if (isGoalClicked) loadGoalTimesheetOnDOM();
            else loadHourTimesheetOnDOM();
            break;
    }
}


//Generic search
const genericSearch = debounce(
    (searchText, url, action) => {
        $.ajax({
            type: 'GET',
            url: url,
            data: { searchKey: searchText.trim() },
            success: function (result) {
                $('#admin-menu-content').html(result);
                $('.spinner-control').removeClass('opacity-1');
                $('.spinner-control').addClass('opacity-0');
                addAllEvents(action);
            },
            error: ajaxErrorSweetAlert
        });
    })

//-------------------------------------------


//CMS Begin

function loadCMSAjax() {

    $.ajax({
        type: 'GET',
        url: '/Admin/CMSPage/Index',
        success: function (result) {
            loadCMSPagesonDOM(result);
            registerCmsAddAndSearchEvent();
        },
        error: ajaxErrorSweetAlert
    });
}

function loadCMSPagesonDOM(result) {
    tinymce.remove('#description');
    $('#admin-menu-content').html(result);
    createPagination(5);
    registerCmsEditAndDeleteButton();
}

function registerCmsEditAndDeleteButton() {
    $('.cms-edit').each((_, item) => {
        $(item).click(() => {

            let cmsId = $(item).data('cmsid');
            handleCmsPageEditAjax(cmsId);
        });
    });

    $('.cms-delete').each((_, item) => {
        $(item).click(() => {
            let cmsId = $(item).data('cmsid');
            handleCmsPageDeleteAjax(cmsId);
        });
    });

    $('.cms-restore').each((_, item) => {
        $(item).click(() => {
            let cmsId = $(item).data('cmsid');
            handleCMSPageRestore(cmsId);
        });
    });
}

function handleCmsPageDeleteAjax(cmsId) {

    Swal.fire({
        title: 'Are you sure?',
        text: "You can activate CMS Page any time!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#f88634',
        cancelButtonColor: '#d33',
        confirmButtonText: 'De-Activate CMS Page'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: '/Admin/CMSPage/Delete',
                data: { cmsId },
                success: function (_, _, status) {

                    successMessageSweetAlert('CMS Page De-activated successfully!');
                    loadCMSAjax();
                    
                },
                error: ajaxErrorSweetAlert
            });
        }
    })

}

function handleCMSPageRestore(cmsId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You can de-activate CMS Page any time!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#f88634',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Activate CMS Page'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'PATCH',
                url: '/Admin/CMSPage/Restore',
                data: { cmsId },
                success: function (_, _, status) {

                    successMessageSweetAlert('CMS Page Activated successfully!');
                    loadCMSAjax();

                },
                error: ajaxErrorSweetAlert
            });
        }
    })
}

function handleCmsPageEditAjax(cmsId) {
    $.ajax(
        {
            type: 'GET',
            url: '/Admin/CMSPage/Edit',
            data: { cmsId },
            success: function (result) {
                loadCMSPagesonDOM(result);
                cmsEditFormEvents();
            },
            error: ajaxErrorSweetAlert
        });
}
function cmsEditFormEvents() {
    $.getScript('/js/rich-editor-tiny.js');
    cmsEditFormSubmitEvent();
    registerCancelButton();
}
function cmsEditFormSubmitEvent() {
    $('#form-edit-cms').on('submit', e => cmsFormSubmit(e, '#form-edit-cms', '/Admin/CMSPage/Edit', 'PUT'));
}

function registerCmsAddAndSearchEvent() {
    registerCmsSearchEvent();
    registerCmsAddButton();   
    $('#cms-search').val(searchText);
}

function cmsAddFormEvents() {
    $.getScript('/js/rich-editor-tiny.js');
    registerAddCmsFormSubmitEvent();
    registerCancelButton();
}

function registerAddCmsFormSubmitEvent() {
    $('#form-add-cms').on('submit', e => cmsFormSubmit(e, '#form-add-cms', '/Admin/CMSPage/AddCMS', 'POST'));
}

function checkIsSlugUnique(slug, id) {
    obj = {}
    obj.slug = slug;
    obj.id = id;
    let isSlugUnique = false;
    $.ajax({
        async: false,
        type: 'GET',
        url: '/Admin/CMSPage/IsSlugUnique',
        data: obj,
        success: (result) => isSlugUnique = result 
    })
    return isSlugUnique;
}
function registerCancelButton() {
    $('#btn-cancel').click(() => loadCMSAjax());
    updateSidebarHeight();
}
function registerCmsSearchEvent() {
    let searchBox = document.getElementById('cms-search');

    searchBox.addEventListener('input', e => {
        searchText = e.target.value;
        $('.spinner-control').removeClass('opacity-0');
        $('.spinner-control').addClass('opacity-1');
        searchCMSWithDebounceAjax(searchText);
    });
}

const searchCMSWithDebounceAjax = debounce(
    (searchText) => {

        $.ajax({
            type: 'GET',
            url: '/Admin/CMSPage/SearchCMS',
            data: { searchKey: searchText.trim() },
            success: function (result) {
                loadCMSPagesonDOM(result);
                registerCmsAddAndSearchEvent();
                $('.spinner-control').removeClass('opacity-1');
                $('.spinner-control').addClass('opacity-0');
            },
            error: ajaxErrorSweetAlert
        });
    });

function registerCmsAddButton() {
    $('#btn-cms-add').click(() => {
        $.ajax({
            type: 'GET',
            url: '/Admin/CMSPage/AddCMS',
            success: function (result) {
                loadCMSPagesonDOM(result);
                cmsAddFormEvents();
            },
            error: ajaxErrorSweetAlert
        });
    });
}

function cmsFormSubmit(e, form, url, requestType) 
    {
        e.preventDefault();
        $(form).validate();

        if (!$(form).valid()) return;

        let description = tinymce.get('description').getContent();


        if (description.length < 20) {
            $('#err-desc').text('Description should contain at least 20 characters!');
            return;
        }
        $('#err-desc').text('');

        let slugText = $('#input-slug').val().trim();
        let isSlugUnique = checkIsSlugUnique(slugText, $('#cmsPageId').val());

        if (!isSlugUnique) {
            $('#err-slug').text('Slug URL should be unique!').show();
            displayActionMessageSweetAlert('Unique slug required', '"' + slugText.trim() + '"' + ' slug already exists!', 'error');
            return false;
        }
        $('#err-slug').text('');

        const cmsPage = {};
        cmsPage.CmsPageId = $('#cmsPageId').val();

        cmsPage.Title = $('#title').val();
        cmsPage.Description = description;
        cmsPage.Status = $('#status').find(':selected').val();
        cmsPage.Slug = slugText;
        $.ajax({
            type: requestType,
            url: url,
            data: cmsPage,
            success: function (result) {
                loadCMSPagesonDOM(result);
                registerCmsAddAndSearchEvent();
            },
            error: ajaxErrorSweetAlert
        })
    }

//CMS End

//Theme Start
function loadThemesAjax() {
    tinymce.remove('#description');
    $.ajax({
        type: 'GET',
        url: '/Admin/MissionTheme/Index',
        success: function (result) {
            $('#admin-menu-content').html(result);
            loadThemesOnDOM(result);
        },
        error: ajaxErrorSweetAlert
    });
}

function loadThemesOnDOM() {
    $('#msn-theme-search').val(searchText);
    $('#msn-theme-search').focus();
    createPagination(5);
    registerThemeSearchAndAdd();
    registerThemeEditAndDelete();
}

function registerThemeSearchAndAdd() {
    $('#btn-theme-add').click(handleThemeAddEvent);
    let searchBox = document.getElementById('msn-theme-search');
    searchBox.addEventListener('input', e => {
        searchText = e.target.value;
        $('.spinner-control').removeClass('opacity-0');
        $('.spinner-control').addClass('opacity-1');
        genericSearch(searchText, '/Admin/MissionTheme/SearchTheme', "theme");
    });
}



function handleThemeAddEvent() {
    $.ajax({
        type: 'GET',
        url: '/Admin/MissionTheme/AddTheme',
        success: function (result) {
            modalContainer.html(result);
            $('#addThemeModal').modal('show');
            registerThemeFormSubmitEvent('#form-add-theme', 'POST', '/Admin/MissionTheme/AddTheme', '#addThemeModal', 'added successfully!');
        },
        error: ajaxErrorSweetAlert
    });
}

function registerThemeFormSubmitEvent(form, type, url, bModal, message,themeId = 0) {
    $(form).on('submit', (e) => {
        e.preventDefault();
        $(form).validate();
        if ($(form).valid()) {
           
            let themeVm = new URLSearchParams($(form).serialize());
            
            let themeName = themeVm.get('Title'); 
            
            let isThemeUnique = checkIsThemeUnique(themeName, themeId);
            if (!isThemeUnique) {
                displayActionMessageSweetAlert(`Theme '${themeName}' Already Exists!`, 'Please enter unique theme name.', 'error');
                return;
            }
            $(bModal).modal('hide');
            $.ajax({
                type: type,
                url: url,
                data: $(form).serialize(),
                success: function (result) {
                    $('#admin-menu-content').html(result);
                    loadThemesOnDOM();
                    $(form)[0].reset();
                    successMessageSweetAlert(themeName + " " + message);
                },
                error: ajaxErrorSweetAlert
            });
        }
    });
}

function checkIsThemeUnique(themeName, themeId = 0) {
    let isThemeUnique = false;
    $.ajax({
        async: false,
        type: 'GET',
        data: { themeName: themeName, themeId: themeId },
        url: '/Admin/MissionTheme/CheckIsThemeUnique',
        success: function (result) {
            isThemeUnique = result;
        },
        error: ajaxErrorSweetAlert
    });
    return isThemeUnique;
}


function registerThemeEditAndDelete() {
    $('.theme-delete').each((_, item) => {
        $(item).click(() => {
            let themeId = $(item).data('themeid');
            handleThemeDelete(themeId);
        });
    });

    $('.theme-edit').each((_, item) => {
        $(item).click(() => {
            let themeId = $(item).data('themeid');
            handleThemeEdit(themeId);
        });
    });

    $('.theme-restore').each((_, item) => {
        $(item).click(() => {
            let themeId = $(item).data('themeid');
            handleThemeRestore(themeId);
        });
    });
}

function handleThemeRestore(themeId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You can de-activate theme any time!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#f88634',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Activate Theme'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'PATCH',
                url: '/Admin/MissionTheme/Restore',
                data: { themeId },
                success: function (_, _, status) {

                    displayActionMessageSweetAlert('Theme Activated', 'Theme Activated successfully!', 'success');
                    loadThemesAjax();

                },
                error: ajaxErrorSweetAlert
            });
        }
    })

}
function handleThemeDelete(themeId) {
    Swal.fire({
        title: 'Do you want to delete theme?',
        icon: 'warning',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Delete Theme',
        denyButtonText: `De-Activate Theme, Instead`,
        confirmButtonColor: '#5cb85c',
        denyButtonColor: '#f88634'
    }).then((result) => {
        if (result.isConfirmed) {
            handleThemeDeleteMenuAjax(themeId, 'DELETE', '/Admin/MissionTheme/DeleteTheme', 'Theme Deleted Successfully!');
        } else if (result.isDenied) {
            handleThemeDeleteMenuAjax(themeId, 'PATCH', '/Admin/MissionTheme/DeactivateTheme', 'Theme De-Activated Successfully!');
        }
    })
}
function handleThemeEdit(themeId) {
    $.ajax({
        type: 'GET',
        url: '/Admin/MissionTheme/Edit',
        data: { themeId },
        success: (result) => {
            modalContainer.html(result);
            $('#editThemeModal').modal('show');
            registerThemeFormSubmitEvent('#form-edit-theme', 'PUT', '/Admin/MissionTheme/Edit', '#editThemeModal', 'updated successfully', themeId);
        },
        error: ajaxErrorSweetAlert
    });
}

function handleThemeDeleteMenuAjax(themeId, type, url, message) {
    $.ajax({
        type: type,
        url: url,
        data: { themeId },
        success: (result, _, status) => {
            if (status.status === 204) {
                displayActionMessageSweetAlert(`Can't delete theme!'`, 'Theme is already in use!', 'error');
                return;
            }
            $('#admin-menu-content').html(result);
            loadThemesOnDOM();
            successMessageSweetAlert(message);
        },
        error: ajaxErrorSweetAlert
    });
}
//Theme end



//Skill Start
function loadSkillsAjax() {
    tinymce.remove('#description');
    $.ajax({
        type: 'GET',
        url: '/Admin/MissionSkill/Index',
        success: function (result) {
            $('#admin-menu-content').html(result);
            loadSkillsOnDOM(result);
        },
        error: ajaxErrorSweetAlert
    });
}

function loadSkillsOnDOM() {
    $('#msn-skill-search').val(searchText);
    $('#msn-skill-search').focus();
    createPagination(5);
    registerSkillSearchAndAdd();
    registerSkillEditAndDelete();
}

function registerSkillSearchAndAdd() {
    $('#btn-skill-add').click(handleSkillAddEvent);
    let searchBox = document.getElementById('msn-skill-search');
    searchBox.addEventListener('input', e => {
        searchText = e.target.value;
        $('.spinner-control').removeClass('opacity-0');
        $('.spinner-control').addClass('opacity-1');
        genericSearch(searchText, '/Admin/MissionSkill/Search', "skill");
    });
}


function handleSkillAddEvent() {
    $.ajax({
        type: 'GET',
        url: '/Admin/MissionSkill/AddSkill',
        success: function (result) {
            modalContainer.html(result);
            $('#addSkillModal').modal('show');
            registerSkillFormSubmitEvent('#form-add-skill', 'POST', '/Admin/MissionSkill/AddSkill', '#addSkillModal', 'added successfully!');
        },
        error: ajaxErrorSweetAlert
    });
}

function registerSkillFormSubmitEvent(form, type, url, bModal, message, skillId = 0) {
    $(form).on('submit', (e) => {
        e.preventDefault();
        $(form).validate();
        if ($(form).valid()) {

            let skillVm = new URLSearchParams($(form).serialize());

            let skillName = skillVm.get('Name');

            let isSkillUnique = checkIsSkillNameUnique(skillName, skillId);
            
            if (isSkillUnique) {
                $('#err-title').text(`${skillName} already exists!`).show();
                displayActionMessageSweetAlert(`Skill '${skillName}' Already Exists!`, 'Please enter unique skill name.', 'error');
                return;
            }
            $(bModal).modal('hide');
            $.ajax({
                type: type,
                url: url,
                data: $(form).serialize(),
                success: function (result) {
                    $('#admin-menu-content').html(result);
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
        url: '/Admin/MissionSkill/CheckIsSkillUnique',
        success: function (result) {
            isSkillUnique = result;
        },
        error: ajaxErrorSweetAlert
    });
    return isSkillUnique;
}


function registerSkillEditAndDelete() {
    $('.skill-delete').each((_, item) => {
        $(item).click(() => {
            let skillId = $(item).data('skillid');
            let skillName = $(item).data('skillname');
            
            handleSkillDelete(skillId, skillName);
        });
    });

    $('.skill-edit').each((_, item) => {
        $(item).click(() => {
            let skillId = $(item).data('skillid');
            handleSkillEdit(skillId);
        });
    });

    $('.skill-restore').each((_, item) => {
        $(item).click(() => {
            let skillId = $(item).data('skillid');
            let skillName = $(item).data('skillname');
            handleSkillRestore(skillId, skillName);
        });
    });
}

function handleSkillRestore(skillId, skillName) {
    Swal.fire({
        title: 'Are you sure?',
        text: `You can de-activate ${skillName} any time!`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#f88634',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Activate Skill'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'PATCH',
                url: '/Admin/MissionSkill/Restore',
                data: { skillId },
                success: function (_, _, status) {

                    successMessageSweetAlert(`${skillName} Activated successfully!`);
                    loadSkillsAjax();

                },
                error: ajaxErrorSweetAlert
            });
        }
    })

}
function handleSkillDelete(skillId, skillName) {
    Swal.fire({
        title: `Do you want to delete ${skillName}?`,
        icon: 'warning',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Delete Skill',
        denyButtonText: `De-Activate, Instead`,
        confirmButtonColor: '#5cb85c',
        denyButtonColor: '#f88634'
    }).then((result) => {
        if (result.isConfirmed) {
            handleSkillDeleteMenuAjax(skillId, 'DELETE', '/Admin/MissionSkill/DeleteSkill', `${skillName} Deleted Successfully!`);
        } else if (result.isDenied) {
            handleSkillDeleteMenuAjax(skillId, 'PATCH', '/Admin/MissionSkill/DeactivateSkill', `${skillName} De-Activated Successfully!`);
        }
    })
}
function handleSkillEdit(skillId) {
    $.ajax({
        type: 'GET',
        url: '/Admin/MissionSkill/Edit',
        data: { skillId },
        success: (result) => {
            modalContainer.html(result);
            $('#editSkillModal').modal('show');
            registerSkillFormSubmitEvent('#form-edit-skill', 'PUT', '/Admin/MissionSkill/Edit', '#editSkillModal', 'updated successfully', skillId);
        },
        error: ajaxErrorSweetAlert
    });
}

function handleSkillDeleteMenuAjax(skillId, type, url, message) {
    $.ajax({
        type: type,
        url: url,
        data: { skillId },
        success: (result, _, status) => {
            if (status.status === 204) {
                displayActionMessageSweetAlert(`Can't delete skill!`, 'Skill is already in use!', 'error');
                return;
            }
            $('#admin-menu-content').html(result);
            loadSkillsOnDOM();
            successMessageSweetAlert(message);
        },
        error: ajaxErrorSweetAlert
    });
}
//Skill end


//Story Begin

function loadStoriesAjax() {

    tinymce.remove('#description');
    $.ajax({
        type: 'GET',
        url: '/Admin/Story/Index',
        success: (result) => {
            $('#admin-menu-content').html(result);
            loadStorysOnDOM();
        },
        error: ajaxErrorSweetAlert
    });
}

function loadStorysOnDOM() {
    $('#story-search').val(searchText);
    $('#story-search').focus();
    createPagination(5);
    registerAllStoryEvents();
}

function registerAllStoryEvents() {

    let searchBox = document.getElementById('story-search');
    searchBox.addEventListener('input', e => {
        searchText = e.target.value;
        $('.spinner-control').removeClass('opacity-0');
        $('.spinner-control').addClass('opacity-1');
        genericSearch(searchText, '/Admin/Story/Search', "story");
    });

    $('.story-delete').each((_, item) => {
        $(item).click(() => {
            let storyId = $(item).data('storyid');
            handleStoryStatus(storyId, 'De-Activate Story', 'Story De-Activated Successfully!','/Admin/Story/DeActivate');
        });
    });
    $('.story-restore').each((_, item) => {
        $(item).click(() => {
            let storyId = $(item).data('storyid');
            handleStoryStatus(storyId, 'Restore Story', 'Story Activated Successfully', '/Admin/Story/Restore');
        });
    });
    $('.story-approve').each((_, item) => {
        $(item).click(() => {
            let storyId = $(item).data('storyid');
            handleStoryStatus(storyId, 'Approve Story', 'Story Approved Successfully!', '/Admin/Story/ApproveStory');
        });
    });
    $('.story-decline').each((_, item) => {
        $(item).click(() => {
            let storyId = $(item).data('storyid');
            handleStoryStatus(storyId, 'Decline Story', 'Story Declined Successfully!', '/Admin/Story/DeclineStory');
        });
    });
}

function handleStoryStatus(storyId, btnText, message, url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You can change story approval anytime!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#f88634',
        cancelButtonColor: '#d33',
        confirmButtonText: btnText
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'PATCH',
                url: url,
                data: { storyId },
                success: (result) => {
                    $('#admin-menu-content').html(result);
                    loadStorysOnDOM();
                    successMessageSweetAlert(message);
                },
                error: ajaxErrorSweetAlert
            });
        }
    })
}

//Story End


//Contact Us Begin

function loadContactUsAjax() {
    tinymce.remove('#description');
    $.ajax({
        type: 'GET',
        url: '/Admin/ContactUs/Index',
        success: (result) => {
            adminMenuContent.html(result);
            loadContactUsOnDOM();
        },
        error: ajaxErrorSweetAlert
    });
}

function loadContactUsOnDOM() {
    $('#contact-us-search').val('');
    $('#contact-us-search').focus();
    createPagination(5);
    registerAllContactUsEvents();
}

function registerAllContactUsEvents() {
    $('.contact-reply').each((_, item) => {
        $(item).click(() => {
            let contactId = $(item).data('contactid');
            $.ajax({
                type: 'GET',
                url: '/Admin/ContactUs/ContactQuery',
                data: { contactId },
                success: (result) => {
                    modalContainer.html(result);
                    $('#contactUsFormModal').modal('show');
                    registerContactUsFormSubmit();
                },
                error: ajaxErrorSweetAlert
            });
        });
    });

    $('.contact-restore').each((_, item) => {
        $(item).click(() => {
            let contactId = $(item).data('contactid');
        });
    });
    $('.contact-delete').each((_, item) => {
        $(item).click(() => {
            let contactId = $(item).data('contactid');
            handleContactEntryDelete(contactId);
        });
    });
}

function handleContactEntryDelete(contactId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You can't revert back!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#f88634',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete Inquiry'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: '/Admin/ContactUs/DeleteContact',
                data: { contactId },
                success: function (_, __, xhr) {
                    if (xhr.status === 204) {
                        errorMessageSweetAlert('Something went during delete contact entry!');
                        return;
                    }
                    loadContactUsAjax();
                },
                error: ajaxErrorSweetAlert
            });
        }
    })
}

function registerContactUsFormSubmit() {
    const response = $('#response');
    $(response).on('input', () => {
        let val = $(response).val();
        if (val.length < 15)
            $('#err-response').text('Reponse should at least have 15 character!');
        else
            $('#err-response').text('');
    });

    $('#contact-reply-form').on('submit', e => {
        e.preventDefault();
        let reply = $(response).val().trim().length;
        if ( reply < 15) {
            $('#err-response').text('Reponse should at least have 15 character!');
            return;
        }
        let userName = $('#user-name').val();
        let contactId = $('#contact-id').val();
        $('#contactUsFormModal').modal('hide');
        successMessageSweetAlert(`Response message will be sent to user ${userName} through email!`);
        $.ajax({
            type: 'PATCH',
            url: '/Admin/ContactUs/ContactResponse',
            data: $('#contact-reply-form').serialize(),
            success: (_, __, xhr) => {
                if (xhr.status === 204) {
                    errorMessageSweetAlert('Something went wrong! Please try again.');
                    return;
                }
                loadContactUsAjax();
            },
            error: ajaxErrorSweetAlert
        });
    });
}
//Contact Us End

//Banner Start
function loadBannersAjax() {
    searchText = '';
    tinymce.remove('#description');
    $.ajax({
        type: 'GET',
        url: '/Admin/Banner/Index',
        success: (result) => {
            adminMenuContent.html(result);
            loadBannerOnDOM();
        },
        error: ajaxErrorSweetAlert
    });
}
function loadBannerOnDOM() {
    $('#banner-search').val(searchText);
    $('#banner-search').focus();
    createPagination(5);
    $('#btn-banner-add').click(handleBannerAdd);
    registerBannerListEvents();
}

function registerBannerListEvents() {
    $('.banner-edit').each((_, item) => {
        $(item).click(() => {
            let bannerId = $(item).data('bannerid');
            handleBannerEdit(bannerId);
        });
    });
    $('.banner-delete').each((_, item) => {
        $(item).click(() => {
            let bannerId = $(item).data('bannerid');
            Swal.fire({
                title: 'Are you sure?',
                text: "You can activate banner status anytime!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#f88634',
                cancelButtonColor: '#d33',
                confirmButtonText: 'De-Activate Banner'
            }).then((result) => {
                if (result.isConfirmed) {
                    handleBannerStatus(bannerId, '/Admin/Banner/DeActivate', 'Banner De-Activated Successfully!');
                }
            });
        });
    });
    $('.banner-restore').each((_, item) => {
        $(item).click(() => {
            let bannerId = $(item).data('bannerid');
            Swal.fire({
                title: 'Are you sure?',
                text: "You can de-activate banner status anytime!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#f88634',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Activate Banner'
            }).then((result) => {
                if (result.isConfirmed) {
                    handleBannerStatus(bannerId, '/Admin/Banner/Restore', 'Banner Activated Successfully!');
                }
            });
        });
    });
}

function handleBannerAdd() {
    $.ajax({
        type: 'GET',
        url: 'Admin/Banner/Add',
        success: (result) => {
            adminMenuContent.html(result);
            regsterBannerAddFormEvents();
        },
        error: ajaxErrorSweetAlert
    });
}

function regsterBannerAddFormEvents() {
    handleImageUploadPreview();
    registerBannerFormCancel(loadBannersAjax);
    handleBannerFormSubmit('#form-add-banner', 'POST', '/Admin/Banner/Add', 'Banner Added Successfully!');
}

function registerBannerFormCancel(cb) {
    $('#btn-cancel').click(e => {
        e.preventDefault();
        cb();
    });
}

function handleImageUploadPreview() {
    const fileUpload = document.getElementById('banner');
    const previewContainer = document.getElementById("banner-preview");

    fileUpload.addEventListener('change', () => {
        $(previewContainer).empty();
        const file = fileUpload.files[0];

        let src = URL.createObjectURL(file);

        let img = $('<img>');
        img.attr('src', src);
        img.attr('alt', 'Banner Image Preview');
        img.attr('height', '250px');
        img.attr('width', '250px');

        previewContainer.append(img.get(0));
    });
}
function handleBannerFormSubmit(form, type, url, message) {

    $(form).on('submit', e => {
        e.preventDefault();

        $(form).valid();

        if (!$(form).valid()) return;
        const formData = new FormData($(form)[0]);
        $.ajax({
            type: type,
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            success: (_, __, xhr) => {
                successMessageSweetAlert(message);
                loadBannersAjax();
            },
            error: ajaxErrorSweetAlert
        });

    });

}

function handleBannerEdit(bannerId) {
    $.ajax({
        type: 'GET',
        url: '/Admin/Banner/Edit',
        data: { bannerId },
        success: (result) => {
            $(adminMenuContent).html(result);
            registerEditBannerFormEvents();
        },
        error: ajaxErrorSweetAlert
    });
}

function registerEditBannerFormEvents() {
    handleEditBannerImagePreview();
    handleImageUploadPreview();
    registerBannerFormCancel(loadBannersAjax);
    handleBannerFormSubmit('#form-edit-banner', 'PUT', '/Admin/Banner/Edit', 'Banner updated successfully!');
}
function handleEditBannerImagePreview()
{
    const path = $('#banner-path').val();
    const fileName = path.split("\\").pop();
    const preview = document.getElementById('img-preview');
    fetch(path)
        .then(response => response.arrayBuffer())
        .then(buffer => {
            const extension = fileName.split('.').pop();
            const type = `image/${extension}`;
            const myFile = new File([buffer], fileName, { type: `image/${type}` });
            preview.src = URL.createObjectURL(myFile);
            preview.style.height = "250px";

            let myFileList = new DataTransfer();
            myFileList.items.add(myFile);
            document.querySelector("#banner").files = myFileList.files;
        });
}

function handleBannerStatus(bannerId, url, message) {
    $.ajax({
        type: 'PATCH',
        url: url,
        data: { bannerId },
        success: () => {
            successMessageSweetAlert(message);
            loadBannersAjax();
        },
        error: ajaxErrorSweetAlert
    });
}
//Banner end


//Timesheet Begin
let isHourClicked = true;
let isGoalClicked = false;
function loadHourTimesheetAjax() {
    tinymce.remove('#description');
    searchText = '';
    $.ajax({
        type: 'GET',
        url: '/Admin/Timesheet/LoadHourTimesheet',
        success: (result) => {
            adminMenuContent.html(result);
            loadHourTimesheetOnDOM();
        },
        error: ajaxErrorSweetAlert
    });
}

function loadGoalTimesheetAjax() {
    searchText = '';
      $.ajax({
            type: 'GET',
            url: '/Admin/Timesheet/LoadGoalTimesheet',
            success: (result) => {
                adminMenuContent.html(result);
                loadGoalTimesheetOnDOM();
            },
            error: ajaxErrorSweetAlert
      });
}

function toggleTimesheetButton(btnAdd, btnRemove) {
    $(btnAdd).addClass('active');
    $(btnRemove).removeClass('active');
}

function loadGoalTimesheetOnDOM() {
    $('#timesheet-goal-search').focus();

    toggleTimesheetButton('#btn-goal-entry', '#btn-hour-entry');
    $('#timesheet-goal-search').val(searchText);
    createPagination(5);
    registerTimesheetHourGoalEvents('goal');
    registerTimesheetSearch('timesheet-goal-search', 'Goal');
}

function loadHourTimesheetOnDOM() {
    toggleTimesheetButton('#btn-hour-entry', '#btn-goal-entry');
    
    $('#timesheet-hour-search').focus();
    $('#timesheet-hour-search').val(searchText);
    createPagination(5);
    registerTimesheetHourGoalEvents('hour');
    registerTimesheetSearch('timesheet-hour-search', 'Hour');
}


function registerTimesheetSearch(id, type) {
    let searchBox = document.getElementById(id);
    searchBox.addEventListener('input', e => {
        searchText = e.target.value;
        $('.spinner-control').removeClass('opacity-0');
        $('.spinner-control').addClass('opacity-1');
        genericSearch(searchText, '/Admin/Timesheet/Search'+type, "timesheet");
    });
}

function registerTimesheetHourGoalEvents(timesheetType) {
    $('#btn-hour-entry').click(() => {
        if (isHourClicked) return;
        loadHourTimesheetAjax(); isGoalClicked = false;
        isHourClicked = !isHourClicked;
    });

    $('#btn-goal-entry').click(() => {
        if (isGoalClicked) return;
        loadGoalTimesheetAjax(); isHourClicked = false;
        isGoalClicked = !isGoalClicked;
    });

    $('.entry-approve').each((_, item) => {
        $(item).click(() => {
            let timesheetId = $(item).data('timesheetid');
            handleTimesheetApprove(timesheetId, timesheetType);
        });
    });

    $('.entry-decline').each((_, item) => {
        $(item).click(() => {
            let timesheetId = $(item).data('timesheetid');
            handleTimesheetDecline(timesheetId, timesheetType);
        });
    });

    $('.btn-story-view').each((_, item) => {
        $(item).click(() => {
            let timesheetId = $(item).data('timesheetid');
        });
    });
}

function handleTimesheetApprove(id, type) {
    let message = (type === 'hour') ? 'Volunteer hour entry approved successfully!' : 'Volunteer goal entry approved successfully!';

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#f88634',
        cancelButtonColor: '#d33',
        confirmButtonText: `Approve ${type} entry`
    }).then((result) => {
        if (result.isConfirmed) {
            updateTimesheetApprovalStatusAjax(id, message, '/Admin/Timesheet/Approve',type);
        }
    })
}

function handleTimesheetDecline(id, type) {
    let message = (type === 'hour') ? 'Volunteer hour entry declined successfully!' : 'Volunteer goal entry declined successfully!';
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#f88634',
        cancelButtonColor: '#d33',
        confirmButtonText: `Decline ${type} entry`
    }).then((result) => {
        if (result.isConfirmed) {
            updateTimesheetApprovalStatusAjax(id, message, '/Admin/Timesheet/Decline', type);
        }
    })
}

function updateTimesheetApprovalStatusAjax(timesheetId, message, url, type) {
    $.ajax({
        type: 'PATCH',
        url: url,
        data: { timesheetId },
        success: function (result) {
            successMessageSweetAlert(message);
            if (type === 'hour') {
                loadHourTimesheetAjax();
                return;
            }
            loadGoalTimesheetAjax();
        },
        error: ajaxErrorSweetAlert
    });
}
//Timesheet End