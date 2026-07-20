using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Repository
{
    public class UserRepository : IUserRepository
    {


        private readonly string _usersPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..", "..", "..", "..","Repository","Data","Users.txt"));


        public void AddUser(User user)
        {
            string json = JsonSerializer.Serialize(user, user.GetType());
            File.AppendAllText(_usersPath, json + Environment.NewLine);
        }

        public void DeleteUser(int id)
        {
            List<User> users = GetAllUsers();
            User user = GetUserById(id);
            if (user == null)
            {
                throw new UserIdDoesNotExist();
            }
            users.Remove(user);
            SaveChanges(users);
            
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new();
            if (!File.Exists(_usersPath))
                return users;
            foreach (string line in File.ReadAllLines(_usersPath))
            {
                using JsonDocument doc = JsonDocument.Parse(line);
                string type = doc.RootElement.GetProperty("UserType").GetString();
                User user;
                if (type == "ClientUser")
                {
                    user = JsonSerializer.Deserialize<ClientUser>(line);
                }
                else if (type == "AdminUser")
                {
                    user = JsonSerializer.Deserialize<AdminUser>(line);
                }
                else
                {
                    continue;
                }
                users.Add(user);
            }
            return users;
        }

        public User GetLastLoggedUser()
        {
            List<User> users = GetAllUsers();
            User user = users.OrderBy(u => u.LastLogin).LastOrDefault();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            List<User> users = GetAllUsers();
            User user = users.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public User GetUserById(int id)
        {
            List<User> users = GetAllUsers();
            User user = users.FirstOrDefault(u =>u.Id == id);
            return user;
        }

        public void SaveChanges(List<User> users)
        {
            List<string> lines = new();

            foreach (User user in users)
            {
                lines.Add(JsonSerializer.Serialize(user, user.GetType()));
            }

            File.WriteAllLines(_usersPath, lines);
        }
        public void UpdateUser(User user)
        {
            List<User> users = GetAllUsers();
            int index = users.FindIndex(u => u.Id == user.Id);
            if (index != -1) 
            {
                users[index] = user; 
            }
            SaveChanges(users);
        }

        

    }
}
