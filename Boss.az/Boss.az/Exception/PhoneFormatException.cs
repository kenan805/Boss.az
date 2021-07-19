using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.az.ExceptionNS
{
    class PhoneFormatException : ApplicationException
    {
        public PhoneFormatException(string message) : base(message)
        {

        }
    }
}
