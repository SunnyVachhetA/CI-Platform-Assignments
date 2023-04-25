//Script for Upload Files
const fileUpload = document.querySelector('#file-upload');
const fileList = document.querySelector('#file-list');
const uploadArea = document.querySelector('#upload-area');
var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png)$/;
let validUploadFiles = [];
let fileIndex = 0;
const addDom = (imgSrc, fileIndex) => {
    const template = document.createElement("li");

    // create the div element
    const div = document.createElement("div");
    div.classList.add("position-relative");

    // create the image element
    const uploadImg = document.createElement("img");
    uploadImg.src = imgSrc;
    uploadImg.classList.add("upload-img");

    // create the button element
    const button = document.createElement("button");
    button.classList.add("btn-cancel");
    const crossImage = document.createElement("img");
    crossImage.src = "/assets/cross.png";
    button.append(crossImage);

    button.addEventListener('click', (e) => {
        e.preventDefault();
        validUploadFiles.splice(fileIndex, 1);
        button.parentElement.parentElement.remove();
        fileErrorOutput();
    });

    // append the image and button elements to the div element
    div.appendChild(uploadImg);
    div.appendChild(button);

    // append the div element to the li element
    template.appendChild(div);

    fileList.appendChild(template);
};
['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
    uploadArea.addEventListener(eventName, preventDefaults, false)
});
uploadArea.addEventListener('click', () => fileUpload.click());
function preventDefaults(e) {
    e.preventDefault();
    e.stopPropagation();
}

uploadArea.addEventListener('drop', handleDrop, false);
function handleDrop(e) {
    let dt = e.dataTransfer;
    let files = dt.files;
    handleFiles(files);
}
fileUpload.addEventListener('change', handleFiles(fileUpload.files));
function handleFiles(files) {
    console.log(files);
    if (files[0]) {
        let len = files.length;
        for (let i = 0; i < len; i++) {
            let file = files[i];
            if (!regex.test(file.name.toLowerCase())) {
                alert("Invalid format!");
                return;
            }

            let reader = new FileReader();
            reader.onload = function (evt) {
                addDom(evt.target.result, fileIndex);
                validUploadFiles.push(file);
                fileIndex++;
                fileErrorOutput();
            }
            reader.readAsDataURL(file);
        }
    }
}

//Script for file upload complete

//Form fields validation

const storyForm = document.querySelector('#story-form');

storyForm.addEventListener('submit', (e) => {
    e.preventDefault();
    let isFormValid = true;
    let text = tinymce.get('description').getContent().trim();
    if (text === undefined || text === '' || text.length <= 30) {
        $('#err-story-desc').show();
        $('#err-story-desc').text('Story should have more than 30 characters!');
        isFormValid = false;        
    }

    if (validUploadFiles.length == 0) {
        $('#err-story-media').show();
        $('#err-story-media').text('Upload at least 1 image!');
        isFormValid = false;
    }
    $('#story-action').val(e.submitter.getAttribute("value"));
    if (isFormValid) {
        fileUpload.files = new FileListItems(validUploadFiles);
        console.log(fileUpload.files);
        storyForm.submit();
    }
});

/*$('#btn-submit').click((e) => {
    e.preventDefault();

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to change content of story!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: 'orange',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Submit',
    }).then((result) => {
        if (result.isConfirmed) {
            $('#btn-submit').trigger('click');
            successMessageSweetAlert( 'Your story sent to admin for approval' );
        }
    })
});*/

//Stackoverflow code
function FileListItems(files) {
    var b = new ClipboardEvent("").clipboardData || new DataTransfer()
    for (var i = 0, len = files.length; i < len; i++) b.items.add(files[i])
    return b.files
}

function fileErrorOutput() {
    if (validUploadFiles.length == 0) {

        $('#err-story-media').show();
        $('#err-story-media').text('Upload at least 1 image!');
    }
    else {
        $('#err-story-media').hide();
    }
}


//For story edit loading selected media

let storyListingPage = '';
$(document).ready
(
    () =>
    {
        let storyMode = $('#story-mode').val();
        if (storyMode === undefined || storyMode !== '') {
            handleFilePreloadStory();
        }

        storyListingPage = $('#story-listing-page-url').val();
    }
);


let preloadedImagePathList = [];
let preloadedMedia = [];
function handleFilePreloadStory()
{
    let preloadedMedia = $("[data-storymedia]");
    let promises = [];

    $.each(preloadedMedia, (_, item) => {
        let path = $(item).data('storymedia');
        preloadedImagePathList.push(path);
    });

   
    for (var i = 0; i < preloadedImagePathList.length; i++) {
        let path = preloadedImagePathList[i];
        let promise = fetch(path)
            .then(response => response.arrayBuffer())
            .then(buffer => {
                return new File([buffer], path);
            })
            .catch(error => {
                console.error('Failed to load file:', path, error);
            });
        promises.push(promise);
    }

    Promise.all(promises)
        .then(files => {
            validUploadFiles = files;
            renderMediaOnHTML(files);
        })
        .catch(error => {
            console.error('Error loading files:', error);
        });
}

function renderMediaOnHTML(files) {
    for (let i = 0; i < files.length; i++) {
        let file = files[i];
        let url = URL.createObjectURL(file);
        let image = new Image();
        image.onload = function () {
            URL.revokeObjectURL(url);
        };

        addDom( url, fileIndex );
    }
}

//Story cancel button
let storyId = 0;
const redirectToStoryListing = () => window.location.href = storyListingPage;
$('#btn-story-cancel').click(
    (e) =>
    {
        e.preventDefault();
        storyId = $('#user-story-id').val();
        console.log(storyId);
        if (storyId === undefined || storyId == 0) {
            handleStoryCancel();
            return;
        }
        else
        {
            handleUserStoryDraftCancel();
        }
    }
);

function handleStoryCancel() {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: 'orange',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Back To Story Listing',
    }).then((result) => {
        if (result.isConfirmed) { 
            redirectToStoryListing();
        }
    })
}

function handleUserStoryDraftCancel()
{
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: 'orange',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete Story',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: '/Volunteer/Story/RemoveStory',
                data: { storyId },
                success: function (result) {
                    redirectToStoryListing();
                },
                error: ajaxErrorSweetAlert
            });
        }
    })
}