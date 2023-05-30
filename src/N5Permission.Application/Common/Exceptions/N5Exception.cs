using System.Globalization;

namespace N5Permission.Application.Common.Exceptions
{
    public class N5Exception : Exception
    {

        public N5Exception() : base()
        {}

        public N5Exception(string message)
          : base(message)
        {
        }

        public N5Exception(string message, Exception innerException) : base(message, innerException)
        {  }

        public N5Exception(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {  }
    }
}
