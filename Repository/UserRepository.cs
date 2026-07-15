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
        

        private readonly string _usersPath = "D:\\Library System\\Repository\\Data\\Users.txt"; // ამას პროექტში როგორც კეთდება ისე გადავაკეთებ მერე.

        public void AddUser(User user)
        {
            List<User> users = GetAllUsers();
            user.Id = users.Count() + 1;
            string line = JsonSerializer.Serialize(user); // User user-ს ვაქცევთ string line-ად
            File.AppendAllLines(_usersPath, new[] {line}); // ახალი ხაზი მივაწეროთ ფაილში
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
            if (!File.Exists(_usersPath))
            {
                return new List<User>(); // თუ ფაილი ვერ იპოვა ახალს შექმნის და დააბრუნებს
            }
            string[] lines = File.ReadAllLines(_usersPath);
            

            List<User> users = new List<User>();
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) // ხაზი შეიძლება გამოტოვებული/ცარიელი იყოს
                {
                    continue;
                }
                User user = JsonSerializer.Deserialize<ClientUser>(line); // string line-ს ვაქცევთ User user-ად
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
            File.Delete(_usersPath);
            File.AppendAllLines(_usersPath, users.Select(u => JsonSerializer.Serialize(u)));
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
