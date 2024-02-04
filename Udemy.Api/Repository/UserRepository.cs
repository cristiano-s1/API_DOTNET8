using System.Security.Cryptography;
using System.Text;
using Udemy.Api.Context;
using Udemy.Api.Data.VO;
using Udemy.Api.Models;

namespace Udemy.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UdemyContext _context;

        public UserRepository(UdemyContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var password = ComputeHash(user.Password, SHA256.Create());

            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == password));
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => (u.UserName == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == userName));
            
            if (user is null) 
                return false;

            user.RefreshToken = null;
            _context.SaveChanges();

            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) 
                return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return result;
        }

        private static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            var builder = new StringBuilder();

            foreach (Byte b in hashedBytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
