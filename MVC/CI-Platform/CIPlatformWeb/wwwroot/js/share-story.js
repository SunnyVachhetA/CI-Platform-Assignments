//Script for Upload Files
const fileUpload = document.querySelector('#file-upload');
const fileList = document.querySelector('#file-list');
const uploadArea = document.querySelector('#upload-area');
const structure = `<div class = "position-relative" >     
                                        <img alt="uploaded file" class = "upload-img">
                                        <button class = "btn-cancel">
                                            <img src="~/assets/cross.png" alt="Cancel">
                                        </button>
                                    </div>`;
var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png)$/;

const addDom = (imgSrc) => {
    const template = document.createElement("li");
    let tempStructure = structure;

    let replace = `<img alt="uploaded file" class = "upload-img">`;
    let replaceBy = `<img src="${imgSrc}" alt = "uploaded file" class = "upload-img">`;
    tempStructure = tempStructure.replace(
        replace, replaceBy
    );
    console.log(tempStructure);
    template.innerHTML = tempStructure.trim();
    fileList.appendChild(template);
}

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
                addDom(evt.target.result);
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