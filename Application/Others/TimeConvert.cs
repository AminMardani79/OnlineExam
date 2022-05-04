using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Others
{
    public static class TimeConvert
    {
        public static string PersianTime(this DateTime time)
        {
            PersianCalendar calendar=new PersianCalendar();
            var result = calendar.GetHour(time).ToString() + ":" + calendar.GetMinute(time).ToString() + ":" +
                         calendar.GetSecond(time).ToString();
            return result;
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
