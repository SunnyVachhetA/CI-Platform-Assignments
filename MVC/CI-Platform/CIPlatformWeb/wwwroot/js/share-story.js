//Script for Upload Files
const fileUpload = document.querySelector('#file-upload');
const fileList = document.querySelector('#file-list');
const uploadArea = document.querySelector('#upload-area');
var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png)$/;
const validUploadFiles = [];
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


//For Share Story Button

$('#btn-share-story').on('click',
    () => {
        if (loggedUserId == 0) {

        }
    });


//Share Story Button Complete

//Form fields validation

const storyForm = document.querySelector('#story-form');

let action = '';

const btnSave = document.querySelector('#btn-save');
const btnSubmit = document.querySelector('#btn-submit');

btnSave.addEventListener('click', () => action = 'draft');
btnSubmit.addEventListener('click', () => action = 'share');


storyForm.addEventListener('submit', (e) => {
    e.preventDefault();
    let isFormValid = true;
    let text = tinymce.get('description').getContent().trim();
    if (text === undefined || text === '' || text.length <= 50) {
        $('#err-story-desc').show();
        $('#err-story-desc').text('Story should have more than 50 characters!');
        isFormValid = false;        
    }

    if (validUploadFiles.length == 0) {
        $('#err-story-media').show();
        $('#err-story-media').text('Upload at least 1 image!');
        isFormValid = false;
    }
    $('#story-action').val(e.submitter.getAttribute("value"))
    if (isFormValid) storyForm.submit();
});

function fileErrorOutput() {
    if (validUploadFiles.length == 0) {

        $('#err-story-media').show();
        $('#err-story-media').text('Upload at least 1 image!');
    }
    else {
        $('#err-story-media').hide();
    }
}