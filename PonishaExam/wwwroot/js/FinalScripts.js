// Send Work Book
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

// Report Charts
window.onload = function () {
    let participated = document.getElementById("participated").value;
    let notParticipated = document.getElementById("notParticipated").value;
    let finishedStudents = document.getElementById("finishedStudents").value;
    let unFinishedStudents = document.getElementById("unFinishedStudents").value;
    if (participated == '0' && notParticipated == '0') {
        notParticipated = '100'
    }
    if (finishedStudents == '0' && unFinishedStudents == '0') {
        unFinishedStudents = '100'
    }
    var firstOptions = {
        animationEnabled: true,
        data: [{
            type: "pie",
            startAngle: 40,
            toolTipContent: "<b>%{label}</b>: {y}",
            showInLegend: "true",
            explodeOnClick: true,
            legendText: "{label}",
            indexLabelFontSize: 16,
            indexLabel: "%{label} - {y}",
            dataPoints: [
                { y: +participated, label: "شرکت کرده" },
                { y: +notParticipated, label: "شرکت نکرده" }
            ]
        }]
    };
    $("#particChartContainer").CanvasJSChart(firstOptions);

    var secondOptions = {
        animationEnabled: true,
        data: [{
            type: "pie",
            startAngle: 40,
            toolTipContent: "<b>%{label}</b>: {y}",
            showInLegend: "true",
            explodeOnClick: true,
            legendText: "{label}",
            indexLabelFontSize: 16,
            indexLabel: "%{label} - {y}",
            dataPoints: [
                { y: +finishedStudents, label: "تکمیل شده" },
                { y: +unFinishedStudents, label: "تکمیل نشده" }
            ]
        }]
    };
    $("#finishedChartContainer").CanvasJSChart(secondOptions);

}


// Select All Students
$('#selectAll').change(function () {
    if ($('#selectAll').is(':checked')) {
        $('#selectBox option').prop('selected', true);
    } else {
        $('#selectBox option').prop('selected', false);
    }
});

// CountDown Timer
const startingHour = document.querySelector('#Hours').value;
const startingMinutes = document.querySelector('#Minutes').value;
let time = (startingHour * 3600) + (startingMinutes * 60);
const countDownEl = document.getElementById('counter');
var interval = setInterval(UpdateCountDown, 1000);

function UpdateCountDown() {
    const hours = Math.floor(time / 3600);
    const minutes = Math.floor((time % 3600) / 60);
    let seconds = (time % 3600) % 60;
    seconds = seconds < 10 ? '0' + seconds : seconds;
    countDownEl.innerHTML = `${hours}:${minutes}:${seconds}`;
    if (time === 0) {
        document.examForm.submit();
    }
    time--;
}
// Set As Checked
const radioChecks = document.querySelectorAll('input[type="radio"]');
radioChecks.forEach(item => {
    const name = item.getAttribute('name');
    let questionRadios = $(`input[name*=${name}]`);
    item.addEventListener('change',
        () => {
            for (n = 0; n < questionRadios.length; n++) {
                questionRadios[n].setAttribute("name", `${name}`);
            }
            if (item.checked == true) {
                item.setAttribute("name", `${name}-checked`);
            }
        });
});
// Show Unchecked Options
const showUnchecked = document.getElementById('showUnchecked');
const questionCounts = document.getElementById('questionCounts').value;
showUnchecked.addEventListener('change',
    () => {
        if (showUnchecked.checked == true) {
            var number = 0;
            for (j = 0; j < questionCounts; j++) {
                const radiosRow = document.querySelector(`.number-${j + 1}`);
                const radioChecks = document.querySelector(`.radioChecks-${j + 1}`);
                const radios = radioChecks.children;
                for (x = 0; x < radios.length; x++) {
                    if (radios[x].getAttribute('name') == `radio-${j + 1}-checked`) {
                        radiosRow.classList.remove('unChecked');
                        number++;
                    } else if (radios[x].getAttribute('name') == `radio-${j + 1}` && number == 0) {
                        radiosRow.classList.add('unChecked');
                    }
                }
                number = 0;
            }
        } else {
            for (j = 0; j < questionCounts; j++) {
                const radiosRow = document.querySelector(`.number-${j + 1}`);
                radiosRow.classList.remove('unChecked');
            }
        }
    });

// Uncheck Row
function UnCheckRow(num) {
    const rowRadios = document.getElementsByClassName(`radio-${num + 1}`);
    for (y = 0; y < rowRadios.length; y++) {
        rowRadios[y].setAttribute('name', `radio-${num + 1}`);
        if (rowRadios[y].checked == true) {
            rowRadios[y].checked = false;
        }
    }
}
