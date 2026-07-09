using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Repository
{
    internal class FileRepository : IUserRepository, IBookRepository
    {
        // აქ იუზერის და წიგნის მეთოდები ერთად დავაიმპლემენტირე თუ გამოდგება ასე... შეიძლება შევცვალო 

        #region IUserRepository

        private readonly string _usersPath = "D:\\Library System\\Repository\\Data\\Users.txt"; // ამას პროექტში როგორც კეთდება ისე გადავაკეთებ მერე.

        public void AddUser(User user)
        {
            string line = JsonSerializer.Serialize(user); // User user-ს ვაქცევთ string line-ად
            File.AppendAllLines(_usersPath, new[] {line}); // ახალი ხაზი მივაწეროთ ფაილში
        }

        public void DeleteUser(int id)
        {
            // TODO
        }

        public List<User> GetAllUsers()
        {
            string[] lines = File.ReadAllLines(_usersPath);
            if (!File.Exists(_usersPath))
            {
                return new List<User>(); // თუ ფაილი ვერ იპოვა ახალს შექმნის და დააბრუნებს
            }

            List<User> users = new List<User>();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) // ხაზი შეიძლება გამოტოვებული/ცარიელი იყოს
                {
                    continue;
                }
                User user = JsonSerializer.Deserialize<User>(line); // string line-ს ვაქცევთ User user-ად
                users.Add(user);
            }
            return users;
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

        #endregion

        // წიგნისთვის ჯერ არ დამიწერია, მაგრამ მალე დავამატებ აუცილებლად
    }
}
