using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        // User-ის ფაილის მეთოდები
        List<User> GetAllUsers();
        User GetUserByEmail(string email);
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser (User user);
        void DeleteUser (int id);
        void SaveChanges(List<User> users);
    }
}
