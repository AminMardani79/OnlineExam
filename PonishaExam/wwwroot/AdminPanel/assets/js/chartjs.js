$(function () {
  'use strict';

  // Bar chart
  if ($('#chartjsBar').length) {
    new Chart($("#chartjsBar"), {
      type: 'bar',
      data: {
        labels: ["ایران", "چین", "ترکیه", "آلمان", "استرالیا"],
        datasets: [{
          label: "جمعیت",
          backgroundColor: ["#b1cfec", "#7ee5e5", "#66d1d1", "#f77eb9", "#4d8af0"],
          data: [2478, 5267, 734, 2084, 1433]
        }]
      },
      options: {
        legend: {
          display: false
        },
      }
    });
  }

  if ($('#chartjsLine').length) {
    new Chart($('#chartjsLine'), {
      type: 'line',
      data: {
        labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
        datasets: [{
          data: [86, 114, 106, 106, 107, 111, 133, 221, 783, 2478],
          label: "آفریقا",
          borderColor: "#7ee5e5",
          backgroundColor: "rgba(0,0,0,0)",
          fill: false
        }, {
          data: [282, 350, 411, 502, 635, 809, 947, 1402, 3700, 5267],
          label: "آسیا",
          borderColor: "#f77eb9",
          backgroundColor: "rgba(0,0,0,0)",
          fill: false
        }]
      }
    });
  }

  if ($('#chartjsDoughnut').length) {
    new Chart($('#chartjsDoughnut'), {
      type: 'doughnut',
      data: {
        labels: ["آفریقا", "آسیا", "اروپا"],
        datasets: [{
          label: "جمعیت (میلیون)",
          backgroundColor: ["#7ee5e5", "#f77eb9", "#4d8af0"],
          data: [2478, 4267, 1334]
        }]
      }
    });
  }

  if ($('#chartjsArea').length) {
    new Chart($('#chartjsArea'), {
      type: 'line',
      data: {
        labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
        datasets: [{
          data: [86, 114, 106, 106, 107, 111, 133, 221, 783, 2478],
          label: "آفریقا",
          borderColor: "#7ee5e5",
          backgroundColor: "#c2fdfd",
          fill: true
        }, {
          data: [282, 350, 411, 502, 635, 809, 947, 1402, 3700, 5267],
          label: "آسیا",
          borderColor: "#f77eb9",
          backgroundColor: "#ffbedd",
          fill: true
        }]
      }
    });
  }

  if ($('#chartjsPie').length) {
    new Chart($('#chartjsPie'), {
      type: 'pie',
      data: {
        labels: ["آفریقا", "آسیا", "اروپا"],
        datasets: [{
          label: "جمعیت (میلیون)",
          backgroundColor: ["#7ee5e5", "#f77eb9", "#4d8af0"],
          data: [2478, 4267, 1334]
        }]
      }
    });
  }

  if ($('#chartjsBubble').length) {
    new Chart($('#chartjsBubble'), {
      type: 'bubble',
      data: {
        labels: "آفریقا",
        datasets: [{
          label: ["چین"],
          backgroundColor: "#c2fdfd",
          borderColor: "#7ee5e5",
          data: [{
            x: 21269017,
            y: 5.245,
            r: 15
          }]
        }, {
          label: ["دانمارک"],
          backgroundColor: "#ffbedd",
          borderColor: "#f77eb9",
          data: [{
            x: 258702,
            y: 7.526,
            r: 10
          }]
        }, {
          label: ["آلمان"],
          backgroundColor: "#bbd4ff",
          borderColor: "#4d8af0",
          data: [{
            x: 3979083,
            y: 6.994,
            r: 15
          }]
        }, {
          label: ["ژاپن"],
          backgroundColor: "#ffe69d",
          borderColor: "#fbbc06",
          data: [{
            x: 4931877,
            y: 5.921,
            r: 15
          }]
        }]
      },
      options: {
        scales: {
          yAxes: [{
            scaleLabel: {
              display: true,
              labelString: "شادی"
            }
          }],
          xAxes: [{
            scaleLabel: {
              display: true,
              labelString: "جمعیت آماری"
            }
          }]
        }
      }
    });
  }

  if ($('#chartjsRadar').length) {
    new Chart($('#chartjsRadar'), {
      type: 'radar',
      data: {
        labels: ["آفریقا", "آسیا", "اروپا", "آمریکا لاتین", "آمریکا شمالی"],
        datasets: [{
          label: "1380",
          fill: true,
          backgroundColor: "#ffbedd",
          borderColor: "#f77eb9",
          pointBorderColor: "#f77eb9",
          pointBackgroundColor: "#ffbedd",
          data: [8.77, 55.61, 21.69, 6.62, 6.82]
        }, {
          label: "1400",
          fill: true,
          backgroundColor: "#c2fdfd",
          borderColor: "#7ee5e5",
          pointBorderColor: "#7ee5e5",
          pointBackgroundColor: "#c2fdfd",
          pointBorderColor: "#fff",
          data: [25.48, 54.16, 7.61, 8.06, 4.45]
        }]
      }
    });
  }

  if ($('#chartjsPolarArea').length) {
    new Chart($('#chartjsPolarArea'), {
      type: 'polarArea',
      data: {
        labels: ["آفریقا", "آسیا", "اروپا", "آمریکا لاتین"],
        datasets: [{
          label: "Population (millions)",
          backgroundColor: ["#f77eb9", "#7ee5e5", "#4d8af0", "#fbbc06"],
          data: [2478, 5267, 734, 784]
        }]
      }
    });
  }

  if ($('#chartjsGroupedBar').length) {
    new Chart($('#chartjsGroupedBar'), {
      type: 'bar',
      data: {
        labels: ["1370", "1380", "1390", "1400"],
        datasets: [{
          label: "آفریقا",
          backgroundColor: "#f77eb9",
          data: [133, 221, 783, 2478]
        }, {
          label: "اروپا",
          backgroundColor: "#7ee5e5",
          data: [408, 547, 675, 734]
        }]
      }
    });
  }

  if ($('#chartjsMixedBar').length) {
    new Chart($('#chartjsMixedBar'), {
      type: 'bar',
      data: {
        labels: ["1370", "1380", "1390", "1400"],
        datasets: [{
          label: "اروپا",
          type: "line",
          borderColor: "#66d1d1",
          backgroundColor: "rgba(0,0,0,0)",
          data: [408, 547, 675, 734],
          fill: false
        }, {
          label: "آفریقا",
          type: "line",
          borderColor: "#ff3366",
          backgroundColor: "rgba(0,0,0,0)",
          data: [133, 221, 783, 2478],
          fill: false
        }, {
          label: "اروپا",
          type: "bar",
          backgroundColor: "#f77eb9",
          // backgroundColor: "rgba(0,0,0,0)",
          data: [408, 547, 675, 734],
        }, {
          label: "آفریقا",
          type: "bar",
          backgroundColor: "#7ee5e5",
          backgroundColorHover: "#3e95cd",
          // backgroundColor: "rgba(0,0,0,0)",
          data: [133, 221, 783, 2478]
        }]
      }
    });
  }

});