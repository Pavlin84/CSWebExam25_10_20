using Git.ViewModels.Usres;

namespace Git.Services
{
    public interface IUserService
    {
        public void CreateUser(UserInputModel user);

        public string GetUserId(string userName, string password);

        public bool IsUsernameTaken(string userName);

        public bool IsEmaiTaken(string email);
    }
}
