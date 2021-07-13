using BusinessLayer.Utilities;
using DataLayer;
using System;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class Users : IUsers
    {
        private readonly IUserRepository userRepository;

        public Users(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public User Get(int id)
        {
            try
            {
                return userRepository.Select(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User Validate(string userName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    return null;
                }

                var user = userRepository.Select(nameof(User.UserName), userName).First();

                var isValid = AuthUtilities.Validate(password, user);

                return isValid ? user : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User Insert(string userName, string password)
        {
            try
            {
                if (userRepository.Select(nameof(User.UserName), userName).FirstOrDefault() != null)
                {
                    return null;
                }

                var (salt, hash) = AuthUtilities.Create(password);

                var user = new User
                {
                    UserName = userName,
                    PasswordSalt = Encoding.Default.GetString(salt),
                    PasswordHash = Encoding.Default.GetString(hash)
                };

                userRepository.Add(user);

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
