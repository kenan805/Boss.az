using System;
namespace Boss.az.ExceptionNS
{
    class UserInfoException:ApplicationException
    {
        public UserInfoException(string message):base(message)
        {

        }
    }
}
