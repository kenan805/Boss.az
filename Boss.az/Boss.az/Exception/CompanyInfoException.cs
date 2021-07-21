using System;

namespace Boss.az.ExceptionNS
{
    class CompanyInfoException : ApplicationException
    {
        public CompanyInfoException(string message) : base(message)
        {

        }
    }
}
