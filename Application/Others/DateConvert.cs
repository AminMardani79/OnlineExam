using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Others
{
    public static class DateConvert
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString("00") + "/" +
                   pc.GetDayOfMonth(date).ToString("00");
        }

        public static string GetEnglishNumbers(this string s)
        {
            return s.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
        }
        public static string GetPersianNumbers(this string s)
        {
            return s.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
        }

        public static DateTime ToMiladiDateTime(this string ts)
        {
            var spliteDate = ts.GetEnglishNumbers().Split('/');

            int year = int.Parse(spliteDate[0]);
            int month = int.Parse(spliteDate[1]);
            int day = int.Parse(spliteDate[2]);
            DateTime currentDate = new DateTime(year, month, day, new PersianCalendar());
            return currentDate;
        }
    }
}
