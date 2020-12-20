using System;
using System.Collections.Generic;
using System.Text;

namespace Git
{
    public static class GlobalConstants
    {

        // User Error
        public const string NameError = "The username should be between 5 and 20 characters long !";

        public const string PasswordError = "The password should be between 6 and 20 characters long !";

        public const string EmailError = "Please enter valid email !";

        public const string LoginError = "User name or password is not valid !";

        public const string ConfirmPasswordError = "The passwords you entered did not match !";

        public const string UsernameAvaivableError = "This username already is taken !";

        public const string EmailAvaivableError = "This email already is taken !";

        //Repository Error

        public const string RepositoryNameAvaivable = "This name already exist, Pleace choise another name !";

        public const string RepositoryNameLenghtError = "Repository name should be betwen 3 and 10 character long !";

        // Commit Error

        public const string DescriptionLenghtError = "Descriotion lenght should be more five characters !";

        public const string DeleteError = "Not Allowed to Deleted !";

    }
}
