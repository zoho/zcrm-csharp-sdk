using System.Text.RegularExpressions;

namespace ZCRMSDK.CRM.Library.Common
{
    public class DateTimeUtil
    {
        public static bool IsISO8601Time(string time)
        {
            string yearRegex = @"((20|19)[0-9]{2})";
            string monthRegex = @"((0[1-9]{1})|(1[1-2]{1}))";
            string dayRegex = @"((0[1-9]{1})|([1-2]{1}[0-9]{1})|30|31)";
            string timeRegex = @"(([0-1]{1}[0-9]{1})|(2[0-3]{1}))";
            string minRegex = @"([0-5]{1}[0-9]{1})";
            string offsetRegex = @"((z|Z)|((\+|\-)" + minRegex + ":" + minRegex + "))";
            string pattern = "^" + yearRegex + "-" + monthRegex + "-" + dayRegex + "T" + timeRegex + ":" + minRegex + ":" + minRegex + offsetRegex + "$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(time);
        }
    }
}
