using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Nucleus
{
    public static class DateUtil
    {
        public static string GetRelativeDate(DateTime date)
        {
            DateTime today = DateTime.Now;

            if (today.DayOfYear == date.DayOfYear)
            {
                if (today.Hour == date.Hour)
                {
                    int dif = today.Minute - date.Minute;
                    return dif.ToString(CultureInfo.InvariantCulture) + " minutos atrás";
                }
                else if (today.Hour - 12 >= date.Hour)
                {

                }

                return "Hoje às " + date.TimeOfDay.ToString(@"hh\:mm");
            }
            else if (today.DayOfYear - 1 == date.DayOfYear)
            {
                return "Ontem às " + date.TimeOfDay.ToString(@"hh\:mm");
            }
            else if (date.AddDays(7) > today)
            {
                return "Há " + (today.TimeOfDay - date.TimeOfDay) + " dias";
            }
            else
            {
                return date.ToShortDateString();
            }
        }
    }
}