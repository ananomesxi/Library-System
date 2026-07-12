using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class UserIdDoesNotExist : Exception
    {
        public UserIdDoesNotExist() : base("User with such ID does not exist.")
        {
        }
    }
}
