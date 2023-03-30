
const mediaCount = $('#story-media-count').val();
if (mediaCount == 1) {
    $('#msn-media-carousel').hide();
}
else if (mediaCount <= 3) {
    $('#scroll-prev').hide();
    $('#scroll-next').hide();
}
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


//Recommend to Co-Worker

$('#btn-story-recommend').click
(
    () =>
    {
        if (loggedUserId == 0) {
            loginRequiredSweetAlert(loginPageLink);
            return;
        }

        handleRecommendToCoWorkerModalAjax();
    }
);

function handleRecommendToCoWorkerModalAjax() {
    console.log("Handle Recommend called!");
    let storyId = $('#story-id').val();
    $.ajax({
        type: 'GET',
        url: '/Volunteer/Story/StoryUsersInvite',
        data: { userId:loggedUserId, storyId:storyId },
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $('#story-recommend-modal').html(result);
            $('#recommendModal').modal('show');
            modalEventListener();
        },
        error: ajaxErrorSweetAlert
    });
}

function modalEventListener() {
    let recommendList = [];
    $('#btn-recommend').on('click',
        () => {
            let count = $("input:checkbox[name='recommend-list']:checked").length;

            if (count == 0) {
                let title = 'No co-worker selected';
                let subTitle = 'You need to select at least one co-worker!';
                displayActionMessageSweetAlert(title, subTitle, 'info');
                return;
            }

            $("input:checkbox[name='recommend-list']:checked").each(function () {
                recommendList.push($(this).val());
            });

            handleUserRecommendAjax(recommendList);
        });
}

function handleUserRecommendAjax(recommendList) {
    alert("here");
}

//Recommend to Co-Worker
