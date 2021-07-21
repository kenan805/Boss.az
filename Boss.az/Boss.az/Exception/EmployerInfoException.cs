using System;

namespace Boss.az.ExceptionNS
{
    class EmployerInfoException:ApplicationException
    {
        public EmployerInfoException(string message):base(message)
        {

        }
    }
}
