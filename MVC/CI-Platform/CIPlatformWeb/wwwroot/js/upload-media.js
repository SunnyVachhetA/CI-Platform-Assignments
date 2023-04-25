//Script for Upload Files
let fileUpload;
let fileList;
let uploadArea;
var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png)$/;
let validUploadFiles = [];
let fileIndex = 0;

function uploadImages() {
    fileUpload = document.querySelector('#file-upload');
    fileList = document.querySelector('#file-list');
    uploadArea = document.querySelector('#upload-area');
    fileIndex = 0;
    validUploadFiles = [];
    registerUploadImagesEvents();
}


function registerUploadImagesEvents() {
    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        uploadArea.addEventListener(eventName, preventDefaults, false)
    });
    uploadArea.addEventListener('click', () => fileUpload.click());
    uploadArea.addEventListener('drop', handleDrop, false);
    fileUpload.addEventListener('change', handleFiles(fileUpload.files));
}

const addDom = (imgSrc, fileIndex) => {
    const template = document.createElement("li");

    // create the div element
    const div = document.createElement("div");
    div.classList.add("position-relative");

    // create the image element
    const uploadImg = document.createElement("img");
    uploadImg.src = imgSrc;
    uploadImg.classList.add("upload-img");
    uploadImg.classList.add("object-fit-cover");

    // create the button element
    const button = document.createElement("button");
    button.classList.add("btn-cancel");
    const crossImage = document.createElement("img");
    crossImage.src = "/assets/cross.png";
    button.append(crossImage);

    button.addEventListener('click', (e) => {
        e.preventDefault();
        if (validUploadFiles.length == 1) validUploadFiles = [];
        else validUploadFiles.splice(fileIndex, 1);
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

function preventDefaults(e) {
    e.preventDefault();
    e.stopPropagation();
}

function handleDrop(e) {
    let dt = e.dataTransfer;
    let files = dt.files;
    handleFiles(files);
}
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


function fileErrorOutput() {
    if (validUploadFiles.length == 0) {
        validUploadFiles = [];
        $('#err-media').show();
        $('#err-media').text('Upload at least 1 image!');
    }
    else {
        $('#err-media').hide();
    }
    return (validUploadFiles.length == 0);
}

//Stackoverflow code
function FileListItems(files) {
    var b = new ClipboardEvent("").clipboardData || new DataTransfer()
    for (var i = 0, len = files.length; i < len; i++) b.items.add(files[i])
    return b.files
}


//js for document upload
let docUpload;
let documentList = [];
function uploadDocuments() {
    documentList = [];
    docUpload = document.getElementById('doc-upload');
    docUpload.addEventListener('change', handleDocumentFiles);
    $('#doc-list').empty();
}

function handleDocumentFiles(files) {
    if (files[0]) {
        let len = files.length;
        for (let i = 0; i < len; i++) {
            let file = files[i];

            let reader = new FileReader();
            reader.onload = function (evt) {
                const url = URL.createObjectURL(file);
                addDocumentPreview(url, file.name);
                documentList.push(file);
            }
            reader.readAsDataURL(file);
        }
    }
}

function addDocumentPreview(docSrc, fileName) {
    const li = document.createElement("li");
    const link = document.createElement("a");
    link.setAttribute("href", docSrc);
    link.setAttribute("target", "_blank");
    link.classList.add("text-black-1");
    li.classList.add("doc-file", "px-4", "py-2");
    link.textContent = fileName;
    li.appendChild(link);
    document.getElementById("doc-list").appendChild(li); // Append the list item to the list
}


//Re-Render Uploaded Images
let preloadedImagePathList = [];
let preloadedMedia = [];
function handleFilePreloadMission() {

    preloadedImagePathList = [];
    preloadedMedia = [];
    uploadImages();
    preloadedMedia = $("[data-media]");
    let promises = [];

    $.each(preloadedMedia, (_, item) => {
        let path = $(item).data('media');
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
        addDom(url, i);
    }
}

let preloadedDocumentPathList = [];
let preloadedDocument = [];
let titleList = [];
function handleDocumentPreloadMission() {

    preloadedDocumentPathList = [];
    preloadedDocument = [];
    titleList = [];
    uploadDocuments();
    preloadedDocument = $("[data-doc]");
    let promises = [];
    $.each(preloadedDocument, (_, item) => {
        let path = $(item).data('doc');
        let title = $(item).data('title');

        titleList.push(title);
        preloadedDocumentPathList.push(path);
    });

    for (var i = 0; i < preloadedDocumentPathList.length; i++) {
        let path = preloadedDocumentPathList[i];
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
            documentList = files;
            renderDocumentOnHTML(files)
        })
        .catch(error => {
            console.error('Error loading files:', error);
        });
}

function renderDocumentOnHTML(files) {
    for (let i = 0; i < files.length; i++) {
        let file = files[i];
        let url = URL.createObjectURL(file);
     
        addDocumentPreview(url, titleList[i]);
    }
}