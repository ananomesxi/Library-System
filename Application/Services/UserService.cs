using Application.Interfaces;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using System.Net;
using System.Net.Sockets;

namespace Application.Services
{
    public class UserService : IUserService
    {
        #region DI
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        private readonly string _historyPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Core", "Logging", "History.txt"));
        #endregion

        public void RegisterUser(string username, string email, string password)
        {
            var existingUser = _userRepository.GetUserByEmail(email);

            if (existingUser != null)
            {
                throw new UserEmailExists();
            }

            List<User> users = _userRepository.GetAllUsers();

            var newClientUser = new ClientUser
            {
                Id = users.Count == 0 ? 1 : users.Max(u => u.Id) + 1,
                Username = username,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                IsVerified = false,
                UserType = "ClientUser"
            };

            _userRepository.AddUser(newClientUser);
        }

        public void ShowAllUsers ()
        {
            List <User> users = _userRepository.GetAllUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No users found."); return;
            }
            Console.WriteLine("User list: ");
            foreach (var user in users)
            {
                Console.Write($"ID: {user.Id} | Username: {user.Username} | Email: {user.Email} | Role: {user.Role}");
                if (user is ClientUser client)
                {
                    Console.Write($" | Fine: {client.Fine}");
                }
                Console.WriteLine();
            }
        }

        public void RemoveUser()
        {
            Console.Write("Enter user ID to remove: ");
            bool isValid = int.TryParse(Console.ReadLine(), out int userId);
            if (!isValid)
            {
                throw new FormatException();
            }
            User user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new UserIdDoesNotExist();
            }
            if (user.Role == UserRole.Admin)
            {
                Console.WriteLine("You cannot remove an admin.");
                return;
            }
            _userRepository.DeleteUser(userId);
            Console.WriteLine("User has been removed.");
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

        public void AddHistory(User user, string message)
        {
            File.AppendAllLines(_historyPath,new[] { $"{GetUserIp()} | {user.Username} {message} at {DateTime.Now:d/M/yyyy}" });
        }

    }
}
