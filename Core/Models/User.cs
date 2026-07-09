using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public abstract class User
    {
        // TODO validations 
        private string _username;

        public int Id { get; set; }
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
                    throw new ArgumentException("Username can not be empty.");
                }
                _username = value;
            } 
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; } = UserRole.Client;
        public string VerificationCode { get; set; }
        public bool IsVerified { get; set; }



    }
}
