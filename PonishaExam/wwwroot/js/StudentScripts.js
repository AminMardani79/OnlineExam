/* Change Image */
function ChangeImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            if ($("#NewImg").hasClass("d-none")) {
                $("#NewImg").removeClass(" d-none");
            }
            $('#NewImg')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
/*Toggle Pdf File*/
const showFile = document.querySelector('.toggleFile');
const arrow = showFile.children[0];
const file = document.querySelector('.File');
showFile.addEventListener('click', function () {
    arrow.classList.toggle('active');
    file.classList.toggle('hidden');
});