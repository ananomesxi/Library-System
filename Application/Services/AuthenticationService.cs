using Application.Interfaces;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using System.Net;
using System.Net.Sockets;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region DI
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        
        public AuthenticationService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }
        #endregion

        public User LoginUser(string email, string password)
        {
            User user = _userRepository.GetUserByEmail(email);
            if (user != null && user.IsVerified)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    user.LastLogin = DateTime.Now;
                    _userRepository.UpdateUser(user);
                    return user;
                }
            }
            throw new Exception("Invalid email or not verified.");
        }
        
        public bool VerifyUser(string email, string verificationCode)
        {
            User user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new UserEmailDoesNotExist();
            }
            if (user.VerificationCode == verificationCode)
            {
                user.IsVerified = true;
                _userRepository.UpdateUser(user);
                return true;
            }
            return false;
        }

        public void SendVerificationCode(string email)
        {
            User user = _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new UserEmailDoesNotExist();
            }

            string code = new Random().Next(1000, 9999).ToString();

            user.VerificationCode = code;

            _userRepository.UpdateUser(user);
            string body = $@"
            <html>
            <body>
            <h2>Library Management System 📚</h2>
            <p>Hello <b>{user.Username}</b>,</p>
            <p>Your verification code is:</p>
            <h1>{code}</h1>
            <p>Enter this code to verify your account.</p>
            <p>Best regards,<br>Library Management System</p>
            </body>
            </html>";
            _emailService.SendEmail(email,"Library Verification Code",body);
        }

        public void LogoutUser (string email)
        {
            User user = _userRepository.GetUserByEmail(email);
            user.LastLogin = null;
            _userRepository.UpdateUser(user);
        }

        

        
    }
}
