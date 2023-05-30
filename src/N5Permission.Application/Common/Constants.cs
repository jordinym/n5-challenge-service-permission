namespace N5Permission.Application.Common
{
    public class Constants
    {
        public struct StatusCode
        {
            public const int Ok = 200;
            public const int TechnicalError = -1;
            public const int FunctionalError = 1;
        }
        public struct DateFormat
        {
            public const string Date = "dd/MM/yyyy";
            public const string DateAndTime = "dd/MM/yyyy HH:mm";
            public const string SQLMinDate = "01/01/1900";
        }

        public static DateTime GetCustomDateTimeNow()
        {
            return DateTime.Now;
        }

    }
}
