using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class UserEmailExists : Exception
    {
        public UserEmailExists() : base("User with such email already exists.")
        {
        }
    }
}
