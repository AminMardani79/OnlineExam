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
/*************/
document.addEventListener("DOMContentLoaded", function () {
    
});

function AddToDelete(id) {
    Swal.fire({
        title: 'آیا پایه تحصیلی به لیست حذفیات اضافه شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله اضافه شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/Grade/AddDeleteList/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' پایه تحصیلی به لیست حذفیات اضافه شد',
                'success'
            );
        }
    });
}

function DeleteGrade(id, count) {
    if (count == 0) {
        Swal.fire({
            title: 'آیا پایه تحصیلی پاک شود؟',
            text: "توجه داشته باشید موارد مرتبط به این پایه نیز حذف میگردند",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'بله پاک شود',
            cancelButtonText: 'انصراف'
        }).then((result) => {
            if (result.isConfirmed) {
                $.get("/AdminPanel/Grade/DeleteDataBase/" + id);
                $("#item_" + id).hide('slow');
                Swal.fire(
                    'عملیات موفقیت آمیز بود',
                    ' پایه تحصیلی از سیستم پاک شد',
                    'success'
                );
            }
        });
    } else {
        Swal.fire({
            title: 'خطا در حذف',
            text: "توجه داشته باشید که برای حذف این پایه آزمون های مرتبط با آن باید حذف شوند",
            icon: 'error',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'باشه',
        });
    }
}
function RestoreDelete(id) {
    $.get("/AdminPanel/Grade/RestoreDelete/" + id, function () {
        $("#item_" + id).hide('slow');
    });
}
/*************/
function TrashLesson(id) {

    Swal.fire({
        title: 'آیا درس به لیست حذفیات اضافه شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله اضافه شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/Lesson/AddDeleteList/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' درس به لیست حذفیات اضافه شد',
                'success'
            );
        }
    });
}

function DeleteLesson(id,count) {
    if (count == 0) {
        Swal.fire({
            title: 'آیا درس پاک شود؟',
            text: "توجه داشته باشید موارد مرتبط به این درس نیز حذف میگردند",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'بله پاک شود',
            cancelButtonText: 'انصراف'
        }).then((result) => {
            if (result.isConfirmed) {
                $.get("/AdminPanel/Lesson/DeleteLesson/" + id);
                $("#item_" + id).hide('slow');
                Swal.fire(
                    'عملیات موفقیت آمیز بود',
                    'درس از سیستم پاک شد',
                    'success'
                );
            }
        });
    } else {
        Swal.fire({
            title: 'خطا در حذف درس',
            text: "توجه داشته باشید که قبل از حذف درس آزمون های مرتبط با آن را نیز حذف کنید",
            icon: 'error',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'باشه',
        });
    }
    
}
function RestoreLesson(id) {
    $.get("/AdminPanel/Lesson/RestoreDelete/" + id, function () {
        $("#item_" + id).hide('slow');
    });
}
/***********************/
function TrashTeacher(id) {

    Swal.fire({
        title: 'آیا مدرس به لیست حذفیات اضافه شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله اضافه شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/Teacher/AddDeleteList/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' مدرس به لیست حذفیات اضافه شد',
                'success'
            );
        }
    });
}
function DeleteTeacher(id, count) {
    if (count == 0) {
        Swal.fire({
            title: 'آیا مدرس پاک شود؟',
            text: "توجه داشته باشید موارد مرتبط به این درس نیز حذف میگردند",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'بله پاک شود',
            cancelButtonText: 'انصراف'
        }).then((result) => {
            if (result.isConfirmed) {
                $.get("/AdminPanel/Teacher/DeleteDataBase/" + id);
                $("#item_" + id).hide('slow');
                Swal.fire(
                    'عملیات موفقیت آمیز بود',
                    'مدرس از سیستم پاک شد',
                    'success'
                );
            }
        });
    } else {
        Swal.fire({
            title: 'خطا در حذف',
            text: "توجه داشته باشید که قبل از حذف مدرس آزمون های مرتبط با او را حذف کنید",
            icon: 'error',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'باشه',
        });
    }
    
}
function RestoreTeacher(id) {
    $.get("/AdminPanel/Teacher/RestoreDelete/" + id, function () {
        $("#item_" + id).hide('slow');
    });
}
//********* Student Alerts ***************
function AddToStudentDeletes(id) {
    Swal.fire({
        title: 'آیا دانش آموز به لیست حذفیات اضافه شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله اضافه شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/Student/AddDeleteList/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' دانش آموز به لیست حذفیات اضافه شد',
                'success'
            );
        }
    });
}
function DeleteStudent(id) {
    Swal.fire({
        title: 'آیا دانش آموز پاک شود؟',
        text: "توجه داشته باشید موارد مرتبط به این دانش آموز نیز حذف میگردند",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله پاک شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/Student/DeleteStudent/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' دانش آموز از سیستم پاک شد',
                'success'
            );
        }
    });
}
function RestoreStudent(id) {
    $.get("/AdminPanel/Student/RestoreDelete/" + id, function () {
        $("#item_" + id).hide('slow');
    });
}
//**************Admin***********************

function RemoveAdmin(id) {
    Swal.fire({
        title: 'آیا مدیر پاک شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله پاک شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/DeleteAdmin/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' مدیر به لیست حذفیات اضافه شد',
                'success'
            );
        }
    });
}

function BackAdmin(id) {
    Swal.fire({
        title: 'آیا مدیر بازگردانی شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله بازگردانی شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/BackAdmin/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' مدیر به لیست  اضافه شد',
                'success'
            );
        }
    });
}

function DeleteAdmin(id) {
    Swal.fire({
        title: 'آیا مدیر پاک شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله پاک شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/RemoveAdmin/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' مدیر پاک  شد',
                'success'
            );
        }
    });
}
//************** Test Alerts ***************
function AddToTestDelete(id) {
    Swal.fire({
        title: 'آیا این آزمون به لیست حذفیات اضافه شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله اضافه شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/Test/AddDeleteList/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' آزمون به لیست حذفیات اضافه شد',
                'success'
            );
        }
    });
}

function DeleteTest(id) {
    Swal.fire({
        title: 'آیا این آزمون پاک شود؟',
        text: "توجه داشته باشید موارد مرتبط به این آزمون نیز حذف میگردند",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله پاک شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/Test/DeleteDataBase/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' آزمون از سیستم پاک شد',
                'success'
            );
        }
    });
}
function RestoreTest(id) {
    $.get("/AdminPanel/Test/RestoreDelete/" + id, function () {
        $("#item_" + id).hide('slow');
    });
}

function RemoveQuestion(id) {
    Swal.fire({
        title: 'آیا سوال پاک شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله پاک شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/Question/Remove/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' سوال پاک شد',
                'success'
            );
        }
    });
}
//*******************Remove Filters***********************
var testTitle = document.getElementById('testTitle');
var testCode = document.getElementById('testCode');
var fromDate = document.getElementById('fromDate');
var toDate = document.getElementById('toDate');
var IsFinish = document.getElementById('IsFinish');
function RemoveFilters() {
    fromDate.value = "";
    toDate.value = "";
    testTitle.value = "";
    testCode.value = "";
    IsFinish.checked = false;
}
//******************Remove Student Filters****************
const studentName = document.getElementById('studentName');
const selectGrade = document.getElementById('gradeId');
function RemoveStudentFilters() {
    studentName.value = "";
    selectGrade.value = 0;
}
//******************Show Alert****************************
document.addEventListener("DOMContentLoaded",
    function () {
        const submitAnswer = document.getElementById('submitAnswer').value;
        if (submitAnswer == 1) {
            Swal.fire({
                title: 'پاسخ های شما با موفقیت ثبت شد',
                icon: 'info',
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'متوجه شدم'
            });
        }
    });
document.addEventListener("DOMContentLoaded",
    function () {
        const workBookSubmit = document.getElementById('workBookSubmit').value;
        if (workBookSubmit == 1) {
            Swal.fire({
                title: 'کارنامه دانش آموز با موفقیت ثبت شد',
                icon: 'info',
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'متوجه شدم'
            });
        }
    });
document.addEventListener("DOMContentLoaded",
    function () {
        const TestClosed = document.getElementById('TestClosed').value;
        if (TestClosed) {
            Swal.fire({
                title: 'شما بیش از حد مجاز در آزمون شرکت کردید.اجازه شرکت در آزمون را ندارید',
                icon: 'error',
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'متوجه شدم'
            });
        }
    });