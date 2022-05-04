//Change Selected option by another
$(document).ready(function () {
    $("#grade").change(function () {
        var gradeId = $("#grade").val();
        $.getJSON("/AdminPanel/Test/GetLesson/" + gradeId,
            function (res) {
                var item = "";
                $.each(res,
                    function (i, sub) {
                        item += '<option value="' + sub.value + '">' + sub.text + '</option>';
                    });
                $("#lesson").html(item);
            });
    });
    $("#lesson").change(function () {
        var lessonId = $("#lesson").val();
        $.getJSON("/AdminPanel/Test/GetTeacher/" + lessonId,
            function (res) {
                var item = "";
                $.each(res,
                    function (i, sub) {
                        item += '<option value="' + sub.value + '">' + sub.text + '</option>';
                    });
                $("#teacher").html(item);
            });
    });
});
// Question Scripts
const questionBox = document.querySelector('.questionBoxs').children;
const indicator = document.querySelectorAll('.indicator-btn');
for (i = 0; i < indicator.length; i++) {
    indicator[i].addEventListener('click', function () {
        for (n = 0; n < indicator.length; n++) {
            indicator[n].classList.remove('active');
        }
        this.classList.add('active');
        for (j = 0; j < questionBox.length; j++) {
            questionBox[j].classList.remove('active');
        }
        let index = this.getAttribute('dataQuestion-id');
        questionBox[index].classList.add('active');
    });
}

function AdminSendWorkBook(testId) {
    Swal.fire({
        title: 'آیا کارنامه ها برای دانش آموزان ارسال شوند؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/EnableWorkBookShowingAll/" + testId);
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' کارنامه ها برای دانش آموزان ارسال شدند',
                'success'
            );
        }
    });
}
function AdminSendAllWorkBooks() {
    Swal.fire({
        title: 'آیا کارنامه ها برای دانش آموزان ارسال شوند؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/EnableWorkBookShowingAll");
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' کارنامه ها برای دانش آموزان ارسال شدند',
                'success'
            );
        }
    });
}


function AdminActiveShowing(studentId, testId) {
    Swal.fire({
        title: 'آیا کارنامه برای دانش آموز ارسال شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/EnableWorkBookShowing/" + studentId + "/" + testId);
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' کارنامه برای دانش آموز ارسال شد',
                'success'
            );
        }
    });
}