using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        User LoginUser(string email, string password);
        void SendVerificationCode(string email);
        bool VerifyUser(string email, string verificationCode);
        void LogoutUser(string email);

    }
}
