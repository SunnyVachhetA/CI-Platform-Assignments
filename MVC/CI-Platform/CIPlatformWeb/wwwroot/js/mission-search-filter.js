﻿const searchMission = document.querySelector("#search-mission");
const countryFilter = document.querySelector('#country-filter');
const cityFilter = document.querySelector('#city-filter');
const themeFilter = document.querySelector('#theme-filter');
const skillFilter = document.querySelector('#skill-filter');
const filterOptionArea = document.querySelector('#filter-option-area');
const userFilterList = document.querySelector('#selected-filter-list'); //Filter option list
const clearAllFilter = document.querySelector('#clear-all-filter');
const sortBy = document.querySelector('.btn-sort-by');
const sortOptions = document.querySelectorAll( '#sort-options' );
let searchText = '';
let sortByOption = 0;
const countryList = [];
const cityList = [];
const themeList = [];
const skillList = [];

function addFilterToHtmlList(id, item, type) {
    filterOptionArea.classList.remove('d-none');
    let li = document.createElement('li');
    li.classList.add(`filter-option-value`);
    li.classList.add(`px-2`);
    li.dataset.id = id;
    li.dataset.type = type;

    let image = document.createElement('img');
    image.src = "./assets/cancel1.png";
    image.alt = "Cancel";
    image.classList.add('cancel-filter');
    image.classList.add( 'cursor-pointer' );

    
    li.textContent = item.trim();
    li.append(image);

    image.addEventListener('click', () => {
        let id = image.parentElement.dataset.id;
        let type = image.parentElement.dataset.type;

        if (type === "country")
            removeElement(id, countryList);
        else if (type === "city")
            removeElement(id, cityList);
        else if (type === "theme")
            removeElement(id, themeList);
        else
            removeElement(id, skillList);
        image.parentElement.remove();
        filterMissionCardAjax();
        if (userFilterList.childElementCount === 0) { clearAllFilter.click(); }
    });
    userFilterList.appendChild(li);
}

function removeElement(id, list) { list.splice(list.indexOf(id), 1) }

const countryOptions = document.querySelectorAll('#country-filter li');

$(countryOptions).click((evt) => {
    let id = evt.currentTarget.dataset.id;
    let country = evt.currentTarget.textContent;
    if (countryList.indexOf(id) == -1) {
        addFilterToHtmlList(id,country,"country");
        countryList.push(id);
        filterMissionCardAjax();
    }
});

const cityOptions = cityFilter.querySelectorAll('li');
$(cityOptions).click((evt) => {
    let id = evt.currentTarget.dataset.id;
    let city = evt.currentTarget.textContent;
    if (cityList.indexOf(id) == -1) {
        addFilterToHtmlList(id, city, "city");
        cityList.push(id);
        filterMissionCardAjax();
    }
});

const themeOptions = themeFilter.querySelectorAll('li');
$(themeOptions).click((evt) => {
    let id = evt.currentTarget.dataset.id;
    let theme = evt.currentTarget.textContent;
    if (themeList.indexOf(id) == -1) {
        addFilterToHtmlList(id, theme, "theme");
        themeList.push(id);
        filterMissionCardAjax();
    }
});

const skillOptions = skillFilter.querySelectorAll('li');
$(skillOptions).click((evt) => {
    let id = evt.currentTarget.dataset.id;
    let skill = evt.currentTarget.textContent;
    if (skillList.indexOf(id) == -1) {
        addFilterToHtmlList(id, skill, "skill");
        skillList.push(id);
        filterMissionCardAjax();
    }
});

clearAllFilter.addEventListener(
    'click',
    () => {
        filterOptionArea.classList.add('d-none');
        countryList.splice(0, countryList.length);
        cityList.splice(0, cityList.length);
        themeList.splice(0, themeList.length);
        skillList.splice(0, skillList.length);
        userFilterList.innerHTML = '';
        filterMissionCardAjax();    
    }
);

searchMission.addEventListener('input', () => {
    searchText = searchMission.value;
    filterMissionCardAjax();
});

$('#sort-options').change((e) => {
    sortByOption = $('#sort-options').val();
    filterMissionCardAjax();
});

$(document).ready(() => {
    $.ajax({
        type: "GET",
        url: "Volunteer/Home/LoadMissionsIndexAjax",
        data: currentPageNumber,    
        success: function (result) {
            console.log("Data sent successfully!");
            $('#partial-mission-listing').html(result);
            missionDisplay();
            missionPagination();
        },
        error: function (xhr, status, error) {
            console.log("Error sending data: " + error);
        }
    });
});

function filterMissionCardAjax() {

    const filterList = {
        countryList: countryList,
        cityList: cityList,
        searchKeyword: searchText,
        themeList: themeList,
        skillList: skillList,
        sortBy: sortByOption,
        page: currentPageNumber
    };

    console.log(filterList);
    $.ajax({
        type: "POST",
        url: "Volunteer/Home/TestAjax",
        data: filterList,
        success: function (result) {
            console.log("Data sent successfully!");
            console.log(result);
            $('#partial-mission-listing').html(result);
            missionDisplay();
            missionPagination();
        },
        error: function (xhr, status, error) {
            console.log("Error sending data: " + error);
        }
    });
}



//MissionCount
const hasMission = document.querySelector('#has-mission');
const noMission = document.querySelector('#no-mission');
const msnNumber = document.querySelector('#number-of-mission');
const displayClass1 = "d-none";




function missionDisplay() {
    const missionCount = document.querySelector('#mission-count');
    let count = missionCount.value;
    if (count > 0) {
        hasMission.classList.remove(displayClass1);
        noMission.classList.add(displayClass1);
        msnNumber.textContent = count;
    }
    else {
        hasMission.classList.add(displayClass1);
        noMission.classList.remove(displayClass1);
    }
}