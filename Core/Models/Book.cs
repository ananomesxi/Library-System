using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Book
    {
        private string _isbn;
        private string _title;
        private string _author;
        private int _quantity;
        public string ISBN 
        { 
            get
            {
                return _isbn;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new NullOrWhiteSpace();
                }
                _isbn = value;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new NullOrWhiteSpace();
                }
                _title = value;
            }
        }
        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new NullOrWhiteSpace();
                }
                _author = value;
            }
        }
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (value < 0)
                {
                    throw new NegativeValue();
                }
                _quantity = value;
            }
        }

    }
}
