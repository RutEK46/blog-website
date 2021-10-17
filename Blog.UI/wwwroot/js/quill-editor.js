function htmlDecode(input) {
    var doc = new DOMParser().parseFromString(input, "text/html");
    return doc.documentElement.textContent;
}

var toolbarOptions = [
    [{ 'font': [] }, { 'size': [] }],
    ['bold', 'italic', 'underline', 'strike'],
    [{ 'color': [] }, { 'background': [] }],
    [{ 'script': 'super' }, { 'script': 'sub' }],
    [{ 'header': '1' }, { 'header': '2' }, 'blockquote', 'code-block'],
    [{ 'list': 'ordered' }, { 'list': 'bullet' }, { 'indent': '-1' }, { 'indent': '+1' }],
    ['direction', { 'align': [] }],
    ['link', 'image', 'video', 'formula'],
    ['clean']
]

var quill = new Quill('#editor', {
    modules: {
        toolbar: toolbarOptions
    },
    theme: 'snow'
});

quill.root.innerHTML = '';
quill.clipboard.dangerouslyPasteHTML(0, htmlDecode(document.getElementById('Body').innerHTML));

quill.on('text-change', function (delta, source) {
    document.getElementById('Body').innerHTML = quill.root.innerHTML;
    document.getElementById('Body').dispatchEvent(new UIEvent("change", {
        "view": window,
        "bubbles": true,
        "cancelable": true
    }));
})


