using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class UserEmailDoesNotExist : Exception
    {
        public UserEmailDoesNotExist() : base("User with such email does not exist.")
        {
        }
    }
}
