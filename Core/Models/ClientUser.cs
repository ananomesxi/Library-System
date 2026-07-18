using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class ClientUser : User
    {
        private decimal _fine;
        public decimal Fine
        {
            get
            {
                return _fine;
            }
            set
            {
                if (value < 0)
                {
                    throw new NegativeValue();
                }
                _fine = value;

            }
        }

    }
}
