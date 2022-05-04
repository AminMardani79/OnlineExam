﻿var kamaDatepicker = function (a, b) { function H(a, b, c) { a = parseInt(a), b = parseInt(b), c = parseInt(c); for (var d = a - 1600, e = b - 1, f = c - 1, g = 365 * d + parseInt((d + 3) / 4) - parseInt((d + 99) / 100) + parseInt((d + 399) / 400), h = 0; e > h; ++h)g += I[h]; e > 1 && (d % 4 == 0 && d % 100 != 0 || d % 400 == 0) && ++g, g += f; var i = g - 79, j = parseInt(i / 12053); i %= 12053; var k = 979 + 33 * j + 4 * parseInt(i / 1461); i %= 1461, i >= 366 && (k += parseInt((i - 1) / 365), i = (i - 1) % 365); for (var h = 0; 11 > h && i >= J[h]; ++h)i -= J[h]; var l = h + 1, m = i + 1; return [k, l, m] } function L(a, b, c) { return T(Q(a, b, c)) } function P(a) { var b, c, d, e, f, g, h, i = [-61, 9, 38, 199, 426, 686, 756, 818, 1111, 1181, 1210, 1635, 2060, 2097, 2192, 2262, 2324, 2394, 2456, 3178], j = i.length, k = a + 621, l = -14, m = i[0]; if (m > a || a >= i[j - 1]) throw new Error("Invalid Jalaali year " + a); for (h = 1; j > h && (b = i[h], c = b - m, !(b > a)); h += 1)l = l + 8 * U(c, 33) + U(V(c, 33), 4), m = b; return g = a - m, l = l + 8 * U(g, 33) + U(V(g, 33) + 3, 4), 4 === V(c, 33) && c - g === 4 && (l += 1), e = U(k, 4) - U(3 * (U(k, 100) + 1), 4) - 150, f = 20 + l - e, 6 > c - g && (g = g - c + 33 * U(c + 4, 33)), d = V(V(g + 1, 33) - 1, 4), -1 === d && (d = 4), { leap: d, gy: k, march: f } } function Q(a, b, c) { var d = P(a); return S(d.gy, 3, d.march) + 31 * (b - 1) - U(b, 7) * (b - 7) + c - 1 } function S(a, b, c) { var d = U(1461 * (a + U(b - 8, 6) + 100100), 4) + U(153 * V(b + 9, 12) + 2, 5) + c - 34840408; return d = d - U(3 * U(a + 100100 + U(b - 8, 6), 100), 4) + 752 } function T(a) { var b, c, d, e, f; return b = 4 * a + 139361631, b = b + 4 * U(3 * U(4 * a + 183187720, 146097), 4) - 3908, c = 5 * U(V(b, 1461), 4) + 308, d = U(V(c, 153), 5) + 1, e = V(U(c, 153), 12) + 1, f = U(b, 1461) - 100100 + U(8 - e, 6), { gy: f, gm: e, gd: d } } function U(a, b) { return ~~(a / b) } function V(a, b) { return a - ~~(a / b) * b } if ("string" != typeof a || 0 === a.length) return void console.error("kamadatepicker error: input ID is not string or is empty"); var g, h, j, n, o, p, q, r, s, t, c = b || {}, d = !1, e = !1, u = { 1: "فروردین", 2: "اردیبهشت", 3: "خرداد", 4: "تیر", 5: "مرداد", 6: "شهریور", 7: "مهر", 8: "آبان", 9: "آذر", 10: "دی", 11: "بهمن", 12: "اسفند" }, v = { "شنبه": "ش", "یکشنبه": "ی", "دوشنبه": "د", "سه شنبه": "س", "چهارشنبه": "چ", "پنج شنبه": "پ", "جمعه": "ج" }, w = ["٠", "١", "٢", "٣", "۴", "۵", "۶", "٧", "٨", "٩", "١٠", "١١", "١٢", "١٣", "١۴", "١۵", "١۶", "١٧", "١٨", "١٩", "٢٠", "٢١", "٢٢", "٢٣", "٢۴", "٢۵", "٢۶", "٢٧", "٢٨", "٢٩", "٣٠", "٣١", "٣٢"]; c.placeholder = void 0 !== c.placeholder ? c.placeholder : "", c.twodigit = void 0 === c.twodigit || c.twodigit, c.closeAfterSelect = void 0 === c.closeAfterSelect || c.closeAfterSelect, c.nextButtonIcon = void 0 !== c.nextButtonIcon && c.nextButtonIcon, c.previousButtonIcon = void 0 !== c.previousButtonIcon && c.previousButtonIcon, c.buttonsColor = void 0 !== c.buttonsColor && c.buttonsColor, c.forceFarsiDigits = void 0 !== c.forceFarsiDigits && c.forceFarsiDigits, c.markToday = void 0 !== c.markToday && c.markToday, c.markHolidays = void 0 !== c.markHolidays && c.markHolidays, c.highlightSelectedDay = void 0 !== c.highlightSelectedDay && c.highlightSelectedDay, c.sync = void 0 !== c.sync && c.sync, c.gotoToday = void 0 !== c.gotoToday && c.gotoToday; var x = $("#" + a); void 0 === x.attr("placeholder") && x.attr("placeholder", c.placeholder), x.wrap("<div id='bd-root-" + a + "' style='position: relative;'></div>"), x.after("<div id='bd-main-" + a + "' class='bd-main bd-hide' style='position: absolute; direction: rtl;'></div>"); var y = $("#bd-main-" + a); y.append("<div class='bd-calendar'></div>"); var z = y.find(".bd-calendar"); z.append("<div class='bd-title'></div>"); var A = z.find(".bd-title"); z.append("<table class='bd-table' dir='rtl' cellspacing='0' cellpadding='0'></table>"), A.append("<button id='bd-next-" + a + "' class='bd-next' type='button' title='ماه بعدی' data-toggle='tooltip'><span>بعدی</span></button>"); var B = $("#bd-next-" + a); c.nextButtonIcon && (B.find("span").css("display", "none"), c.nextButtonIcon.indexOf(".") !== -1 ? B.css("background-image", "url(" + c.nextButtonIcon + ")") : B.addClass(c.nextButtonIcon)), A.append("<div class='bd-dropdown'></div><div class='bd-dropdown'></div>"), A.find(".bd-dropdown:nth-child(2)").append("<select id='bd-month-" + a + "' class='bd-month'></select>"); var C = $("#bd-month-" + a); $.each(u, function (a, b) { C.append($("<option></option>").attr("value", a).text(b)) }), A.find(".bd-dropdown:nth-child(3)").append("<select id='bd-year-" + a + "' class='bd-year'></select>"); var D = $("#bd-year-" + a); A.append("<button id='bd-prev-" + a + "' class='bd-prev' type='button' title='ماه قبلی' data-toggle='tooltip'><span>قبلی</span></button>"); var E = $("#bd-prev-" + a); c.nextButtonIcon && (E.find("span").css("display", "none"), c.previousButtonIcon.indexOf(".") !== -1 ? E.css("background-image", "url(" + c.previousButtonIcon + ")") : E.addClass(c.previousButtonIcon)), c.buttonsColor && (B.css("color", c.buttonsColor), B.find("span").css("color", c.buttonsColor), E.css("color", c.buttonsColor), E.find("span").css("color", c.buttonsColor)), z.find(".bd-table").append("<thead><tr></tr></thead>"), $.each(v, function (a, b) { z.find(".bd-table thead tr").append($("<th></th>").text(b)) }), z.find(".bd-table").append("<tbody id='bd-table-days-" + a + "' class='bd-table-days'></tbody>"); var F = $("#bd-table-days-" + a); if (c.gotoToday) { z.append("<div class='bd-goto-today'>برو به امروز</div>"); var G = z.find(".bd-goto-today") } x.on("focus", function () { y.removeClass("bd-hide"), c.sync && e === !1 && (W(), e = !0) }).on("blur", function () { 0 == d ? (y.addClass("bd-hide"), e = !1) : (d = !1, x.focus(), event.preventDefault()) }), y.on("mousedown", function (a) { d = !0 }), C.on("change", function () { h = parseInt(this.value), r = aa(g, h), q = ca(g, h), da(r, q) }), D.on("change", function () { g = parseInt(this.value), r = aa(g, h), q = ca(g, h), da(r, q) }); var I = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31], J = [31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29], W = function () { var a = X(x.val()); "" !== a && (a = a.split("/"), C.val(parseInt(a[1])), C.trigger("change"), D.val(parseInt(a[0])), D.trigger("change"), c.highlightSelectedDay && (y.find(".bd-selected-day").removeClass("bd-selected-day"), y.find(".day-" + parseInt(a[2])).addClass("bd-selected-day"))) }, X = function (a) { return "" === a ? "" : (a = a.split("/"), a[1].length < 2 && a[1] < 10 && (a[1] = "0" + a[1]), a[2].length < 2 && a[2] < 10 && (a[2] = "0" + a[2]), a = a.join("/")) }, Y = function (a) { var b; return b = a < 6 ? a + 1 : 0 }, Z = function (a) { for (D.find("option").remove(), i = 0; i < 101; i++) { var b = a - 95 + i + ""; if (c.forceFarsiDigits) for (var d = 0; d < 10; d++) { var e = new RegExp(d, "g"); b = b.replace(e, w[d]) } D.append($("<option>", { value: a - 95 + i, text: b })) } }, _ = function (a) { var b; return a < 1343 && a > 1243 ? (b = a % 33, 1 == b || 5 == b || 9 == b || 13 == b || 17 == b || 22 == b || 26 == b || 30 == b) : a < 1473 && a > 1342 ? (b = a % 17, 1 == b || 5 == b || 9 == b || 13 == b || 18 == b || 22 == b || 26 == b || 30 == b) : "unknown" }, aa = function (a, b) { return b < 7 ? 31 : b < 12 ? 30 : _(a) ? 30 : 29 }; s = new Date, t = H(s.getFullYear(), s.getMonth() + 1, s.getDate()); var ba = []; for (i = 0; i < 3; i++)ba[i] = t[i]; n = t[0], o = t[1], p = t[2], g = ba[0], h = ba[1], j = ba[2], C.val(h), Z(g), D.val(g), r = aa(g, h); var ca = function (a, b) { var c = L(a, b, 1); return c = new Date(c.gy + "/" + c.gm + "/" + c.gd), Y(c.getDay()) }; q = ca(g, h); var da = function (b, d) { F.empty(); for (var e = 1, f = 1; e <= b;) { for (F.append($("<tr>", { class: "tr-" + f })), i = 0; i < 7; i++) { if (1 == e) for (var j = 0; j < d;)$("#bd-table-days-" + a + " .tr-1").append($("<td>", { class: "bd-empty-cell" })), j++ , i++; if (i < 7 && e <= b) { var k = '<td><button class="day day-' + e + '" type="button">' + (c.forceFarsiDigits ? w[e] : e) + "</button></td>"; if (c.markToday && e == p && o == h && n == g) { var l = k.indexOf("day day-"); k = k.slice(0, l) + " bd-today " + k.slice(l) } if (c.markHolidays && 6 == i) { var l = k.indexOf("day day-"); k = k.slice(0, l) + " bd-holiday " + k.slice(l) } $("#bd-table-days-" + a + " .tr-" + f).append(k), e++ } } f++ } if (c.highlightSelectedDay) { var m = x.val(); m = m.split("/"), m[0] == g && m[1] == h && (y.find(".bd-selected-day").removeClass("bd-selected-day"), y.find(".day-" + parseInt(m[2])).addClass("bd-selected-day")) } }; x.parent().on("click", "button.day", function () { var a = g + "/" + h + "/" + $(this).attr("class").split(" ")[$(this).attr("class").split(" ").indexOf("day") + 1].split("-")[1]; c.twodigit && (a = X(a)), x.val(a), x.trigger("change"), c.closeAfterSelect && (d = !1, x.trigger("blur")), c.highlightSelectedDay && (y.find(".bd-selected-day").removeClass("bd-selected-day"), $(this).addClass("bd-selected-day")) }), B.on("click", function () { C.val() < 12 ? (C.val(parseInt(C.val()) + 1), C.trigger("change")) : (C.val(1), C.trigger("change"), D.val(parseInt(D.val()) + 1), D.trigger("change")) }), E.on("click", function () { C.val() > 1 ? (C.val(parseInt(C.val()) - 1), C.trigger("change")) : (C.val(12), C.trigger("change"), D.val(parseInt(D.val()) - 1), D.trigger("change")) }), c.gotoToday && G.on("click", function () { C.val(o), C.trigger("change"), D.val(n), D.trigger("change") }), da(r, q), "function" == typeof $().modal && $('[data-toggle="tooltip"]').tooltip() };


var customOptions = {
    placeholder: "تاریخ شروع نمایش",
    twodigit: false,
    closeAfterSelect: false,
    nextButtonIcon: "fa fa-arrow-circle-right",
    previousButtonIcon: "fa fa-arrow-circle-left",
    buttonsColor: "blue",
    forceFarsiDigits: true,
    markToday: true,
    markHolidays: true,
    highlightSelectedDay: true, sync: true,
    gotoToday: true
}
kamaDatepicker('startdate', customOptions);

var customOptionsend = {
    placeholder: "تاریخ پایان نمایش",
    twodigit: false,
    closeAfterSelect: false,
    nextButtonIcon: "fa fa-arrow-circle-right",
    previousButtonIcon: "fa fa-arrow-circle-left",
    buttonsColor: "blue",
    forceFarsiDigits: true,
    markToday: true,
    markHolidays: true,
    highlightSelectedDay: true, sync: true,
    gotoToday: true
}

kamaDatepicker('enddate', customOptionsend);