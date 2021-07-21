using System;

namespace Boss.az.ExceptionNS
{
    class EmailFormatException:ApplicationException
    {
        public EmailFormatException(string message):base(message)
        {

        }
    }
}
