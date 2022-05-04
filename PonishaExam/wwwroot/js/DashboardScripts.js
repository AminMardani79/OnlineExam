// Teacher Charts
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
            indexLabelFontSize: 12,
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
            indexLabelFontSize: 12,
            indexLabel: "%{label} - {y}",
            dataPoints: [
                { y: +finishedStudents, label: "تکمیل شده" },
                { y: +unFinishedStudents, label: "تکمیل نشده" }
            ]
        }]
    };
    $("#finishedChartContainer").CanvasJSChart(secondOptions);
}

// Admin Charts
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