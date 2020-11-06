using System;
namespace UnusualSpending
{
    public class DateTimeProvider : IDateTimeProvider
    { 

        public DateTime getDateTime()
        {
            return DateTime.Now;
        }
    }
}
