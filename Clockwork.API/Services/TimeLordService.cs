using System;
using System.Collections.Generic;
using System.Linq;

namespace Clockwork.API.Services
{
    public static class TimeLordService
    {
        public static string CurrentTimeZone { get; set; }

        public static DateTime ConvertTime(DateTime dateTime, string timeZoneCode)
        {
            if (timeZoneCode == null)
                timeZoneCode = "GMT - Greenwich Mean Time";

            var offset = timeZones.First(x => x.Key == timeZoneCode).Value;

            if (TimeZoneInfo.Local.IsDaylightSavingTime(dateTime))
                offset++;

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, AddOffset(dateTime, offset), dateTime.TimeOfDay.Minutes, dateTime.TimeOfDay.Seconds);
        }

        private static int AddOffset(DateTime dateTime, int offset) => dateTime.TimeOfDay.Hours + offset % 24;

        public static List<string> GetAllTimeZones() => timeZones.Keys.ToList();

        private static Dictionary<string, int> timeZones = new Dictionary<string, int>()
        {
            { "GMT - Greenwich Mean Time", 0 },
            { "UTC - Universal Coordinated Time", 0 },
            { "ECT - European Central Time", 1 },
            { "EET - Eastern European Time", 2 },
            { "ART - (Arabic) Egypt Standard Time", 2 },
            { "EAT - Eastern African Time", 3 },
            { "NET - Near East Time", 4 },
            { "PLT - Pakistan Lahore Time", 5 },
            { "BST - Bangladesh Standard Time", 6 },
            { "VST - Vietnam Standard Time", 7 },
            { "CTT - China Taiwan Time", 8 },
            { "JST - Japan Standard Time", 9 },
            { "AET - Australia Eastern Time", 10},
            { "SST - Solomon Standard Time", 11},
            { "NST - New Zealand Standard Time", 12},
            { "MIT - Midway Islands Time", -11 },
            { "HST - Hawaii Standard Time", -10 },
            { "AST - Alaska Standard Time", -9},
            { "PST - Pacific Standard Time", -8},
            { "PNT - Phoenix Standard Time", -7},
            { "MST - Mountain Standard Time", -7},
            { "CST - Central Standard Time", -6},
            { "EST - Eastern Standard Time", -5},
            { "IET - Indiana Eastern Standard Time", -5},
            { "PRT - Puerto Rico and US Virgin Islands Time", -4},
            { "AGT - Argentina Standard Time", -3},
            { "BET - Brazil Eastern Time", -3},
            { "CAT - Central African Time", -1},
        };
    }
}
