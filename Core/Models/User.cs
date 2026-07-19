using Core.Enums;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.Models
{
    public abstract class User
    {
        private int _id;
        private string _username;
        private string _email;
        private string _password;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value < 0)
                {
                    throw new NegativeValue();
                }
                _id = value;
            }
        }
        public string Username 
        { 
            get
            {
                return _username;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullOrWhiteSpace();
                }
                _username = value;
            } 
        }
        public string Email 
        { 
            get
            {
                return _email;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new NullOrWhiteSpace();
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new FormatException("Invalid email format.");
                }
                _email = value;
            }
        }
        public string Password 
        {
            get
            {
                return _password;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new NullOrWhiteSpace();
                }
                _password = value;
            }
        }
        public UserRole Role { get; set; } = UserRole.Client;
        public string VerificationCode { get; set; } // არ სჭირდება ვალიდაცია, რენდომით ვქმნით ვერიფიკაციის კოდს
        public bool IsVerified { get; set; } = false;
        public DateTime? LastLogin { get; set; } // არც ამას სჭირდება ვალიდაცია
        public string UserType { get; set; }


    }
}
