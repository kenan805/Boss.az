using System;

namespace Boss.az.ExceptionNS
{
    class DatabaseInfoException : ApplicationException
    {
        public DatabaseInfoException(string message) : base(message)
        {

        }
    }
}
