using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.az.ExceptionNS
{
    class EmailFormatException:ApplicationException
    {
        public EmailFormatException(string message):base(message)
        {

        }
    }
}
