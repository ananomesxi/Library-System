using Core.Enums;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Models
{
    public class BorrowRecord
    {
        private int _borrowID;
        private int _userID;
        private string _isbn;

        public int BorrowID
        {
            get
            {
                return _borrowID;
            }
            set
            {
                if (value < 0)
                {
                    throw new NegativeValue();
                }
                _borrowID = value;
            }
        }
        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                if (value < 0)
                {
                    throw new NegativeValue();
                }
                _userID = value;
            }
        }
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
        public DateTime ReturnDate { get; set; }
        public BorrowStatus Status { get; set; }

    }
}
