function DeleteOrder(id) {
    Swal.fire({
        title: 'آیا این فاکتور پاک شود؟',
        text: "توجه داشته باشید موارد مرتبط به این فاکتور نیز حذف میگردند",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله پاک شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/AdminPanel/RemoveOrder/" + id);
            $("#item_" + id).hide('slow');
            Swal.fire(
                'عملیات موفقیت آمیز بود',
                ' فاکتور از سیستم پاک شد',
                'success'
            );
        }
    });
}
function RemoveDetail(id) {
    Swal.fire({
        title: 'آیا از سبد خرید پاک شود؟',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'لغو',
        confirmButtonText: 'پاک کردن'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/ShowBasket/Remove/" + id);
            $("#card_" + id).hide('slow');
            Swal.fire(
                'پاک شد',
                'از سبد خرید پاک شد',
                'success'
            );
        }
    });
}
function AddToBasket(id) {
    $.ajax({
        url: "/AddToBasket", //1
        data: {
            id: id
        }, //2
        type: "Get", //3
        datatype: "Json", //4
        beforeSend: function () {
            //5
        },
        success: function (result) {
            if (result == 1) {
                Swal.fire({
                    icon: 'success',
                    title: 'این دوره قبلا به سبد خرید شما اضافه شده',
                    confirmButtonText: 'ممنون',
                });
            }
            if (result == 2) {
                Swal.fire({
                    icon: 'success',
                    title: 'به سبد خرید شما اضافه شد',
                    confirmButtonText: 'ممنون',
                });
            }
        },
        error: function () { //7

        }
    });
}