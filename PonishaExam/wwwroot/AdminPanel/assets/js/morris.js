$(function () {
  'use strict';

  var gridLineColor = 'rgba(77, 138, 240, .2)';

  new Morris.Line({
    element: 'morrisLine',
    data: [{
        year: '1395',
        value: 2
      },
      {
        year: '1396',
        value: 9
      },
      {
        year: '1397',
        value: 5
      },
      {
        year: '1398',
        value: 12
      },
      {
        year: '1399',
        value: 5
      }
    ],
    xkey: 'year',
    ykeys: ['value'],
    labels: ['مقدار'],
    lineColors: ['#f77eb9'],
    gridLineColor: [gridLineColor]
  });

  Morris.Area({
    element: 'morrisArea',
    data: [{
        y: '1393',
        a: 100,
        b: 90
      },
      {
        y: '1394',
        a: 75,
        b: 65
      },
      {
        y: '1395',
        a: 50,
        b: 40
      },
      {
        y: '1396',
        a: 75,
        b: 65
      },
      {
        y: '1397',
        a: 50,
        b: 40
      },
      {
        y: '1398',
        a: 75,
        b: 65
      },
      {
        y: '1399',
        a: 100,
        b: 90
      }
    ],
    xkey: 'y',
    ykeys: ['a', 'b'],
    labels: ['سری الف', 'سری ب'],
    lineColors: ["#f77eb9", "#7ee5e5"],
    gridLineColor: [gridLineColor]
  });

  Morris.Bar({
    element: 'morrisBar',
    data: [{
        y: '1393',
        a: 100,
        b: 90
      },
      {
        y: '1394',
        a: 75,
        b: 65
      },
      {
        y: '1395',
        a: 50,
        b: 40
      },
      {
        y: '1396',
        a: 75,
        b: 65
      },
      {
        y: '1397',
        a: 50,
        b: 40
      },
      {
        y: '1398',
        a: 75,
        b: 65
      },
      {
        y: '1399',
        a: 100,
        b: 90
      }
    ],
    xkey: 'y',
    ykeys: ['a', 'b'],
    labels: ['سری الف', 'سری ب'],
    barColors: ["#f77eb9", "#7ee5e5"],
    gridLineColor: [gridLineColor]
  });

  Morris.Donut({
    element: 'morrisDonut',
    data: [{
        label: "فروش دانلودی",
        value: 12
      },
      {
        label: "فروش در فروشگاه",
        value: 30
      },
      {
        label: "فروش با سفارش ایمیلی",
        value: 20
      }
    ],
    colors: ["#f77eb9", "#7ee5e5", "#4d8af0"],
    labelColor: 'rgba(77, 138, 240, .3)'
  });

});