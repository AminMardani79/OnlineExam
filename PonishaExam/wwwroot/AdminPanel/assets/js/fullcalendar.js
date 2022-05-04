$(function () {

  // sample calendar events data

  var curYear = moment().format('YYYY');
  var curMonth = moment().format('MM');

  // Calendar Event Source
  var calendarEvents = {
    id: 1,
    backgroundColor: 'rgba(1,104,250, .15)',
    borderColor: '#0168fa',
    events: [{
      id: '1',
      start: curYear + '-' + curMonth + '-08T08:30:00',
      end: curYear + '-' + curMonth + '-08T13:00:00',
      title: 'ملاقات با توسعه دهندگان گوگل',
      description: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم ...'
    }, {
      id: '2',
      start: curYear + '-' + curMonth + '-10T09:00:00',
      end: curYear + '-' + curMonth + '-10T17:00:00',
      title: 'نوشتن/ بازنویسی کد ها',
      description: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم ...'
    }, {
      id: '3',
      start: curYear + '-' + curMonth + '-13T12:00:00',
      end: curYear + '-' + curMonth + '-13T18:00:00',
      title: 'کنفرانس تلفنی',
      description: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم متن ساختگی ...'
    }, {
      id: '4',
      start: curYear + '-' + curMonth + '-15T07:30:00',
      end: curYear + '-' + curMonth + '-15T15:30:00',
      title: 'سفر هفتگی با تیم',
      description: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم ...'
    }, {
      id: '5',
      start: curYear + '-' + curMonth + '-17T10:00:00',
      end: curYear + '-' + curMonth + '-19T15:00:00',
      title: 'فستیوال طراحی وب',
      description: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم ...'
    }, {
      id: '6',
      start: curYear + '-' + curMonth + '-08T13:00:00',
      end: curYear + '-' + curMonth + '-08T18:30:00',
      title: 'مطاسم ازدواج رضا افشار',
      description: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم ...'
    }]
  };

  // Birthday Events Source
  var birthdayEvents = {
    id: 2,
    backgroundColor: 'rgba(16,183,89, .25)',
    borderColor: '#10b759',
    events: [{
        id: '7',
        start: curYear + '-' + curMonth + '-01T18:00:00',
        end: curYear + '-' + curMonth + '-01T23:30:00',
        title: 'مهمانی پدرام',
        description: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم ...'
      },
      {
        id: '8',
        start: curYear + '-' + curMonth + '-21T15:00:00',
        end: curYear + '-' + curMonth + '-21T21:00:00',
        title: 'جشن تولد مریم',
        description: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم ...'
      },
      {
        id: '9',
        start: curYear + '-' + curMonth + '-23T15:00:00',
        end: curYear + '-' + curMonth + '-23T21:00:00',
        title: 'جشن تولد فرهاد',
        description: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم ...'
      }
    ]
  };


  var holidayEvents = {
    id: 3,
    backgroundColor: 'rgba(241,0,117,.25)',
    borderColor: '#f10075',
    events: [{
        id: '10',
        start: curYear + '-' + curMonth + '-04',
        end: curYear + '-' + curMonth + '-06',
        title: 'روز جشن'
      },
      {
        id: '11',
        start: curYear + '-' + curMonth + '-26',
        end: curYear + '-' + curMonth + '-27',
        title: 'روز یادبود'
      },
      {
        id: '12',
        start: curYear + '-' + curMonth + '-28',
        end: curYear + '-' + curMonth + '-29',
        title: 'روز ولنتاین'
      }
    ]
  };

  var discoveredEvents = {
    id: 4,
    backgroundColor: 'rgba(0,204,204,.25)',
    borderColor: '#00cccc',
    events: [{
      id: '13',
      start: curYear + '-' + curMonth + '-17T08:00:00',
      end: curYear + '-' + curMonth + '-18T11:00:00',
      title: 'سمینار ورکشاپ طراحی وب'
    }]
  };

  var meetupEvents = {
    id: 5,
    backgroundColor: 'rgba(91,71,251,.2)',
    borderColor: '#5b47fb',
    events: [{
        id: '14',
        start: curYear + '-' + curMonth + '-03',
        end: curYear + '-' + curMonth + '-05',
        title: 'کنفراس ویدیویی آموزش UI/UX'
      },
      {
        id: '15',
        start: curYear + '-' + curMonth + '-18',
        end: curYear + '-' + curMonth + '-20',
        title: 'کنفرانس آموزش انگولار'
      }
    ]
  };


  var otherEvents = {
    id: 6,
    backgroundColor: 'rgba(253,126,20,.25)',
    borderColor: '#fd7e14',
    events: [{
        id: '16',
        start: curYear + '-' + curMonth + '-06',
        end: curYear + '-' + curMonth + '-08',
        title: 'روز استراحت'
      },
      {
        id: '17',
        start: curYear + '-' + curMonth + '-29',
        end: curYear + '-' + curMonth + '-31',
        title: 'روز استراحت'
      }
    ]
  };


  // initialize the external events
  $('#external-events .fc-event').each(function () {
    // store data so the calendar knows to render an event upon drop
    $(this).data('event', {
      title: $.trim($(this).text()), // use the element's text as the event title
      stick: true // maintain when user navigates (see docs on the renderEvent method)
    });
    // make the event draggable using jQuery UI
    $(this).draggable({
      zIndex: 999,
      revert: true, // will cause the event to go back to its
      revertDuration: 0 //  original position after the drag
    });

  });


  // initialize the calendar
  $('#fullcalendar').fullCalendar({
    header: {
      left: 'prev,today,next',
      center: 'title',
      right: 'month,agendaWeek,agendaDay,listMonth'
    },
    editable: true,
    droppable: true, // this allows things to be dropped onto the calendar
    dragRevertDuration: 0,
    defaultView: 'month',
    eventLimit: true, // allow "more" link when too many events
    eventSources: [calendarEvents, birthdayEvents, holidayEvents, discoveredEvents, meetupEvents, otherEvents],
    eventClick: function (event, jsEvent, view) {
      $('#modalTitle1').html(event.title);
      $('#modalBody1').html(event.description);
      $('#eventUrl').attr('href', event.url);
      $('#fullCalModal').modal();
    },
    dayClick: function (date, jsEvent, view) {
      $("#createEventModal").modal("show");
    },
    // defaultDate: '2019-07-12',
    // events: [{
    //     title: 'All Day Event',
    //     start: '2019-07-08'
    //   },
    //   {
    //     title: 'Long Event',
    //     start: '2019-07-01',
    //     end: '2019-07-07',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'
    //   },
    //   {
    //     id: 999,
    //     title: 'Repeating Event',
    //     start: '2019-07-09T16:00:00',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'

    //   },
    //   {
    //     id: 999,
    //     title: 'Repeating Event',
    //     start: '2019-07-16T16:00:00',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'
    //   },
    //   {
    //     title: 'Conference',
    //     start: '2019-07-11',
    //     end: '2019-07-13',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'        
    //   },
    //   {
    //     title: 'Meeting',
    //     start: '2019-07-12T10:30:00',
    //     end: '2019-07-12T12:30:00',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'
    //   },
    //   {
    //     title: 'Lunch',
    //     start: '2019-07-12T12:00:00',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'
    //   },
    //   {
    //     title: 'Meeting',
    //     start: '2019-07-12T14:30:00',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'
    //   },
    //   {
    //     title: 'Happy Hour',
    //     start: '2019-07-12T17:30:00',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'
    //   },
    //   {
    //     title: 'Dinner',
    //     start: '2019-07-12T20:00:00',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'
    //   },
    //   {
    //     title: 'Birthday Party',
    //     start: '2019-07-13T07:00:00',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'
    //   },
    //   {
    //     title: 'Team Lunch',
    //     start: '2019-07-28',
    //     description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus adipisci explicabo magnam molestiae libero.'
    //   }
    // ],

    // drop: function() {
    //   // is the "remove after drop" checkbox checked?
    //   if ($('#drop-remove').is(':checked')) {
    //     // if so, remove the element from the "Draggable Events" list
    //     $(this).remove();
    //   }
    // },
    eventDragStop: function (event, jsEvent, ui, view) {
      if (isEventOverDiv(jsEvent.clientX, jsEvent.clientY)) {
        // $('#calendar').fullCalendar('removeEvents', event._id);
        var el = $("<div class='fc-event'>").appendTo('#external-events-listing').text(event.title);
        el.draggable({
          zIndex: 999,
          revert: true,
          revertDuration: 0
        });
        el.data('event', {
          title: event.title,
          id: event.id,
          stick: true
        });
      }
    }
  });


  var isEventOverDiv = function (x, y) {
    var external_events = $('#external-events');
    var offset = external_events.offset();
    offset.right = external_events.width() + offset.left;
    offset.bottom = external_events.height() + offset.top;

    // Compare
    if (x >= offset.left &&
      y >= offset.top &&
      x <= offset.right &&
      y <= offset.bottom) {
      return true;
    }
    return false;
  }

});