using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    internal class NegativeValue : Exception
    {
        public NegativeValue() : base("Value can not be negative.")
        {
        }
    }
}
