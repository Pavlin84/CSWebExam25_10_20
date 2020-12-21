using System.Text;
using System.Linq;
using System.Security.Cryptography;

using Git.Data;
using Git.ViewModels.Usres;

namespace Git.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateUser(UserInputModel inputUser)
        {
            var user = new User
            {
                Username = inputUser.Username,
                Password = ComputeHash(inputUser.Password),
                Email = inputUser.Email
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public string GetUserId(string userName, string password)
        {
            var userId = db.Users
                .Where(x => x.Username == userName && x.Password == ComputeHash(password))
                .Select(x => x.Id)
                .FirstOrDefault();

            return userId;
        }

        public bool IsEmaiTakne(string email)
        {
            if (db.Users.Any(x => x.Email == email))
            {
                return true;
            }

            return false;
        }

        public bool IsUsernameTaken(string userName)
        {
            if (db.Users.Any(x => x.Username == userName))
            {
                return true;
            }

            return false;
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
