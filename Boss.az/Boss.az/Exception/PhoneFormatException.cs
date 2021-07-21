using System;

namespace Boss.az.ExceptionNS
{
    class PhoneFormatException : ApplicationException
    {
        public PhoneFormatException(string message) : base(message)
        {

        }
    }
}
