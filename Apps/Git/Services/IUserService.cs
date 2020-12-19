using Git.ViewModels.Usres;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface IUserService
    {
        public void CreateUser(UserInputModel user);

        public string GetUserId(string userName, string password);

        public bool IsUsernameTaken(string userName);

        public bool IsEmaiTakne(string email);
    }
}
