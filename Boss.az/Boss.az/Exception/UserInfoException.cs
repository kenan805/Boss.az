﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.az.ExceptionNS
{
    class UserInfoException:ApplicationException
    {
        public UserInfoException(string message):base(message)
        {

        }
    }
}