tinymce.init({

    selector: 'textarea#description',
    menubar: false,
     plugins: [

        'a11ychecker', 'advlist', 'advcode', 'advtable', 'autolink', 'checklist', 'export',

        'lists', 'link', 'image', 'charmap', 'preview', 'anchor', 'searchreplace', 'visualblocks',

        'powerpaste', 'fullscreen', 'formatpainter', 'insertdatetime', 'media', 'table', 'help', 'wordcount'

    ],

    toolbar: 'undo redo | bold italic strikethrough | superscript subscript | alignleft aligncenter alignright alignjustify |' +

        'bullist numlist checklist outdent indent | removeformat | code'

})