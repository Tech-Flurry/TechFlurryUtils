using System;

namespace TechFlurry.Utils.Extensions.DateTime
{
    public static class Extensions
    {
        /// <summary>
        /// Returns a passed time from a particular date 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetPassedTimeSpanFromNow(this System.DateTime time)
        {
            string s = time.ToShortDateString();
            TimeSpan span = System.DateTime.Now - time;
            var mins = decimal.Round(Convert.ToDecimal(span.TotalMinutes));
            var hrs = decimal.Round(Convert.ToDecimal(span.TotalHours));
            var days = decimal.Round(Convert.ToDecimal(span.TotalDays));
            if (mins < 1)
            {
                s = "Just Now";
            }
            else if (hrs < 1)
            {
                if (mins == 1)
                {
                    s = "1 minute ago";
                }
                else
                {
                    s = mins + " minutes ago";
                }
            }
            else if (days < 1)
            {
                if (hrs == 1)
                {
                    s = "1 hour ago";
                }
                else
                {
                    s = hrs + " hours ago";
                }
            }
            else if (days == 1)
            {
                s = "1 day ago";
            }
            else if (days < 31)
            {
                s = days + " days ago";
            }
            return s;
        }
        /// <summary>
        /// Get days passed from a particulr date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetPassedDateSpanFromNow(this System.DateTime date)
        {
            string s = date.ToShortDateString();
            TimeSpan span = System.DateTime.Now - date;
            var days = decimal.Round(Convert.ToDecimal(span.TotalDays));
            if (days < 1)
            {
                s = "Less than a day";
            }
            else if (days < 31)
            {
                s = days + " days ago";
            }
            return s;
        }
        /// <summary>
        /// Returns a remaining time in word from a date time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetComingTimeSpanFromNow(this System.DateTime time)
        {
            string s = "More than a Month";
            TimeSpan span = time - System.DateTime.Now;
            var mins = decimal.Round(Convert.ToDecimal(span.TotalMinutes));
            var hrs = decimal.Round(Convert.ToDecimal(span.TotalHours));
            var days = decimal.Round(Convert.ToDecimal(span.TotalDays));
            if (mins < 1)
            {
                s = "in a moment";
            }
            else if (hrs < 1)
            {
                if (mins == 1)
                {
                    s = "in a minute";
                }
                else
                {
                    s = "in " + mins + " minutes";
                }
            }
            else if (days < 1)
            {
                if (hrs == 1)
                {
                    s = "in an hour";
                }
                else
                {
                    s = "in " + hrs + " hours";
                }
            }
            else if (days == 1)
            {
                s = "in a day";
            }
            else if (days < 31)
            {
                s = "in " + days + " days";
            }
            return s;
        }
        public static string GetComingDateSpanFromNow(this System.DateTime date)
        {
            string s = "More than a Month";
            TimeSpan span = date - System.DateTime.Now;
            var days = decimal.Round(Convert.ToDecimal(span.TotalDays));
            if (days < 1)
            {
                s = "in a day";
            }
            else if (days < 31)
            {
                s = "in " + days + " days";
            }
            return s;
        }
        /// <summary>
        /// Returns a general short date string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToGeneralDateTimeString(this System.DateTime dateTime)
        {
            return dateTime.ToString("g");
        }
        /// <summary>
        /// Returns a Date to a long time format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToLongTimeString(this System.DateTime dateTime)
        {
            return dateTime.ToString("T");
        }
        /// <summary>
        /// Returns a date time string in format "MM/dd/yyyy HH:mm:ss tt"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToCompleteServerFormat(this System.DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy HH:mm:ss tt");
        }
        public static string ToWords(this TimeSpan span)
        {
            string s = "More than a month";
            var mins = decimal.Round(Convert.ToDecimal(span.TotalMinutes), 2);
            var hrs = decimal.Round(Convert.ToDecimal(span.TotalHours), 2);
            var days = decimal.Round(Convert.ToDecimal(span.TotalDays), 2);
            if (mins < 1)
            {
                s = "Less than a min";
            }
            else if (hrs < 1)
            {
                if (mins == 1)
                {
                    s = "1 min";
                }
                else
                {
                    s = mins + " mins";
                }
            }
            else if (days < 1)
            {
                if (hrs == 1)
                {
                    s = "1 hr";
                }
                else
                {
                    s = hrs + " hrs";
                }
            }
            else if (days == 1)
            {
                s = "1 day";
            }
            else if (days < 31)
            {
                s = days + " days";
            }
            return s;
        }
    }
}
