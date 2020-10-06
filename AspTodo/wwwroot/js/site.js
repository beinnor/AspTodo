// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelectorAll('.done-checkbox').forEach(item => {
    item.addEventListener('click', e => markComplete(e.target))
})


const markComplete = (checkbox) => {
   
    const row = checkbox.closest('tr');

    row.classList.add('done');

    const form = checkbox.closest('form');    
    form.submit();

};