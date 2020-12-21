using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

using Git.Services;
using Git.ViewModels.Usres;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {

            if (!this.IsUserSignedIn())
            {
                var userId = this.userService.GetUserId(username, password);
                if (userId == null)
                {
                    return this.Error(GlobalConstants.LoginError);
                }
                this.SignIn(userId);
            }
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserInputModel inputModel)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return this.Error(GlobalConstants.ConfirmPasswordError);
            }
            if (string.IsNullOrEmpty(inputModel.Username) || inputModel.Username.Length < 5 || inputModel.Username.Length > 20)
            {
                return this.Error(GlobalConstants.NameError);
            }
            if (string.IsNullOrEmpty(inputModel.Password) || inputModel.Password.Length < 6 || inputModel.Password.Length > 20)
            {
                return this.Error(GlobalConstants.PasswordError);
            }
            if (inputModel.Email == null || !new EmailAddressAttribute().IsValid(inputModel.Email))
            {
                return this.Error(GlobalConstants.EmailError);
            }
            if (userService.IsUsernameTaken(inputModel.Username))
            {
                return this.Error(GlobalConstants.UsernameAvaivableError);
            }
            if (userService.IsEmaiTakne(inputModel.Email))
            {
                return this.Error(GlobalConstants.EmailAvaivableError);
            }

            this.userService.CreateUser(inputModel);

            return this.Redirect("/Users/Login");
        }


        public HttpResponse Logout()
        {
            if (this.IsUserSignedIn())
            {
                this.SignOut();
            }
            return this.Redirect("/");
        }


    }
}
