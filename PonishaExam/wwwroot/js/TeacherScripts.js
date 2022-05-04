function AddToDeleteStudents(id) {
    Swal.fire({
        title: 'آیا این دانش آموز به لیست حذفیات اضافه شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله اضافه شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/MasterPanel/Student/AddDeleteList/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' دانش آموز به لیست حذفیات اضافه شد',
                'success'
            );
        }
    });
}
function DeleteMasterStudent(id) {
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
            $.get("/MasterPanel/Student/DeleteStudent/" + id);
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
    $.get("/MasterPanel/Student/RestoreDelete/" + id, function () {
        $("#item_" + id).hide('slow');
    });
}
//******************************
function AddToDeleteTest(id) {
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
            $.get("/Master/Test/AddDeleteList/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' آزمون به لیست حذفیات اضافه شد',
                'success'
            );
        }
    });
}

function DeleteMasterTest(id) {
    Swal.fire({
        title: 'آیا  این آزمون پاک شود؟',
        text: "توجه داشته باشید موارد مرتبط به این آزمون نیز حذف میگردند",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله پاک شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/Master/Test/DeleteDataBase/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' آزمون از سیستم پاک شد',
                'success'
            );
        }
    });
}
function RestoreMasterTest(id) {
    $.get("/Master/Test/RestoreDelete/" + id, function () {
        $("#item_" + id).hide('slow');
    });
}
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

function MasterSendWorkBook(testId) {
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
function MasterSendAllWorkBooks() {
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
function MasterActiveShowing(studentId, testId) {
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
            $.get("/Master/EnableWorkBookShowing/" + studentId + "/" + testId);
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' کارنامه برای دانش آموز ارسال شد',
                'success'
            );
        }
    });
}