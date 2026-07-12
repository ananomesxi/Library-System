using Application.Interfaces;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(string username, string email, string password)
        {
            var existingUser = _userRepository.GetUserByEmail(email);

            if (existingUser != null)
            {
                throw new UserEmailExists();
            }

            var newClientUser = new ClientUser
            {
                Username = username,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                IsVerified = false
            };

            _userRepository.AddUser(newClientUser);
        }



    }
}
