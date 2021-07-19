using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.az.ExceptionNS
{
    class EmployerInfoException:ApplicationException
    {
        public EmployerInfoException(string message):base(message)
        {

        }
    }
}
