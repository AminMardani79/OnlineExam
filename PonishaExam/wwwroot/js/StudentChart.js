// Student Scripts
var scoresCount = document.querySelector('#scoresCount').value;
var scores = document.querySelector('#scores').value.replace('[', '').replace(']', '').split(',');
window.onload = function () {
    var chart = new CanvasJS.Chart("StudentChart", {
        title: {
            text: "نمودار پیشرفت نمره"
        },
        animationEnabled: true,
        exportEnabled: true,
        axisY: {
            title: "نمره آزمون (%)",
        },
        axisX: {
            title: "شماره آزمون",
            margin: 10,
            gridThickness: 1,
            logarithmic: true,
            logarithmBase: 2,
            labelFormatter: function (e) {
                return e.value;
            }
        },
        data: [
            {
                type: "line",
                lineColor: "red",
                dataPoints: []
            }
        ]
    });
    var length = chart.options.data[0].dataPoints.length;
    for (let i = 0; i < scoresCount; i++) {
        chart.options.data[0].dataPoints.push({ x: length + i + 1, y: +scores[i] });
    }
    chart.render();
}