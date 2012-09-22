using System;

namespace Voting.Helpers
{
    public static class DateTimeExtensions
    {
        public static bool SortaEquals(this DateTime dateTime, DateTime otherDateTime)
        {
            return Math.Abs((dateTime - otherDateTime).TotalSeconds) < 1;
        }

        public static bool SortaEquals(this DateTime? dateTime, DateTime? otherDateTime)
        {
            if (dateTime == null && otherDateTime == null)
                return true;

            if (dateTime == null || otherDateTime == null)
                return false;

            return Math.Abs((dateTime.Value - otherDateTime.Value).TotalSeconds) < 1;
        }

        public static DateTime Combine(DateTime datePart, DateTime timePart)
        {
            return new DateTime(datePart.Year, datePart.Month, datePart.Day,
                timePart.Hour, timePart.Minute, timePart.Second, timePart.Millisecond);
        }

        public static DateTime StartMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime EndMonth(DateTime date)
        {
            return StartMonth(date).AddMonths(1).AddDays(-1);
        }

        public static DateTime Next(DayOfWeek dayOfWeek)
        {
            return dayOfWeek <= DateTime.Today.DayOfWeek ? DateTime.Today.Date.AddDays((dayOfWeek - DateTime.Today.DayOfWeek) + 7) : DateTime.Today.Date.AddDays((dayOfWeek - DateTime.Today.DayOfWeek));
        }

        public static DateTime Previous(DayOfWeek dayOfWeek)
        {
            return dayOfWeek >= DateTime.Today.DayOfWeek ? DateTime.Today.Date.AddDays((dayOfWeek - DateTime.Today.DayOfWeek) - 7) : DateTime.Today.Date.AddDays((dayOfWeek - DateTime.Today.DayOfWeek));
        }

        public static DateTime StartWeek(DateTime time)
        {
            return time.Date.AddDays(DayOfWeek.Sunday - time.DayOfWeek);
        }

        public static DateTime EndWeek(DateTime time)
        {
            return time.Date.AddDays(DayOfWeek.Saturday - time.DayOfWeek);
        }

        public static DateTime FromUnixTime(this Int64 self)
        {
            var ret = new DateTime(1970, 1, 1);
            return ret.AddSeconds(self);
        }

        public static Int64 ToUnixTime(this DateTime self)
        {
            var epoc = new DateTime(1970, 1, 1);
            var delta = self - epoc;
            if (delta.TotalSeconds < 0) throw new ArgumentOutOfRangeException("Unix epoc starts January 1st, 1970");
            return (long)delta.TotalSeconds;
        }
    }
}
