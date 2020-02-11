using System;

namespace BandApi.Helpers
{
    public static class BaseTypeExtension
    {
        public static int GetYearsAgo(this DateTime dt)
        {
            var currDate = DateTime.Now;
            int yearsAgo = currDate.Year - dt.Year;
            return yearsAgo;
        }
    }
}
