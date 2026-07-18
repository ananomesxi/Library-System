using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(string username, string email, string password);
        void ShowAllUsers();
        void RemoveUser();


    }
}
