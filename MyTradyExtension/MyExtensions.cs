using System.Globalization;
using Trady.Core.Infrastructure;

namespace MyTradyExtension
{
    public static class MyExtensions
    {
        public static string GetPersianDate(this IOhlcv candle)
        {
            var date = candle.DateTime.DateTime;
            var p = new PersianCalendar();
            return p.GetYear(date) + "/" + p.GetMonth(date) + "/" + p.GetDayOfMonth(date);
        }
    }
}