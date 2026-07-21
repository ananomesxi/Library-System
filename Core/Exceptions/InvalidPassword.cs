using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
        public class InvalidPassword : Exception
        {
            public InvalidPassword() : base("The current password is incorrect.")
            {
            }
        }
}
