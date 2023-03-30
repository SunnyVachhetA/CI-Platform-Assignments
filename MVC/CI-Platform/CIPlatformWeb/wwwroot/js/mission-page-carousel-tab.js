// For Tab Interaction
const msnTab = document.querySelector('#tab-msn');
const orgTab = document.querySelector('#tab-org');
const commentsTab = document.querySelector('#tab-comments');

const tabMsnContent = document.querySelector('#tab-msn-content');
const tabOrgContent = document.querySelector('#tab-org-content');
const tabCommentsContent = document.querySelector('#tab-comments-content');

const displayNone = "d-none";
const activeClass = "active";

const mediaCount = $('#msn-media-count').val();
if (mediaCount == 1) {
    $('#msn-media-carousel').hide();
}
else if (mediaCount <= 3) {
    $('#scroll-prev').hide();
    $('#scroll-next').hide();
}
msnTab.addEventListener(
    'click',
    () => {
        displayContent(tabMsnContent, msnTab);
        removeContent(tabOrgContent, tabCommentsContent);
        removeActive(orgTab, commentsTab);
    }
);

orgTab.addEventListener(
    'click',
    () => {
        displayContent(tabOrgContent, orgTab);
        removeContent(tabMsnContent, tabCommentsContent);
        removeActive(msnTab, commentsTab);
        //console.log(tabOrgContent);
    }
);

commentsTab.addEventListener(
    'click',
    () => {
        displayContent(tabCommentsContent, commentsTab);
        removeContent(tabOrgContent, tabMsnContent);
        removeActive(orgTab, msnTab);
        //console.log(commentsTab.classList);
    }
);
function displayContent(tabContent, tab) {
    classList = tabContent.classList;
    //console.log(classList);

    if (tab.classList.contains(activeClass)) return;
    classList.remove(displayNone);
    tab.classList.add(activeClass);

}
function removeContent(tab1, tab2) {
    tab1.classList.add(displayNone);
    tab2.classList.add(displayNone);
}

function removeActive(tab1, tab2) {
    tab2.classList.remove(activeClass);
    tab1.classList.remove(activeClass);
}

//Tab Interaction JS Complete

//Scroll 
const slider = document.querySelector('.slider');
const scrollPrev = document.querySelector('#scroll-prev');
const scrollNext = document.querySelector('#scroll-next');


scrollPrev.addEventListener(
    'click',
    () => {
        let scrollWidth = slider.offsetWidth;
        const scrollValue = slider.scrollLeft - scrollWidth;
        //console.log(scrollValue, slider.scrollLeft);
        slider.scroll(scrollValue, 0);
    }
);

scrollNext.addEventListener(
    'click',
    () => {
        let scrollWidth = slider.offsetWidth;
        const scrollValue = scrollWidth + slider.scrollLeft;
        slider.scroll(scrollValue, 0);
    }
);
//Scroll Fin