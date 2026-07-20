using Application.Interfaces;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        private readonly string _loginHistoryPath =Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..", "..", "..", "..","Core","Logging","LoginHistory.txt"));
        public AuthenticationService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        

        public User LoginUser(string email, string password)
        {
            User user = _userRepository.GetUserByEmail(email);
            if (user != null && user.IsVerified)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    user.LastLogin = DateTime.Now;
                    _userRepository.UpdateUser(user);
                    
                    File.AppendAllLines(_loginHistoryPath, new[] { $"{user.Username} logged in at {user.LastLogin} from {GetUserIp()}" });
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

            _emailService.SendEmail(
                email,
                "Library Verification Code",
                $"Your verification code is: {code}"
            );
        }

        public void LogoutUser (string email)
        {
            User user = _userRepository.GetUserByEmail(email);
            user.LastLogin = null;
            _userRepository.UpdateUser(user);
        }

        public string GetUserIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }
    }
}
