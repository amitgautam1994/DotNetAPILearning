using System.Linq;
using DotnetAPI.Models;

namespace DotnetAPI.Data 
{
    public class UserRepository : IUserRepository
    {
        DataContextEF _entityFramework;

        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null){
                _entityFramework.Add(entityToAdd);
            }
        }

        public void RemoveEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null){
                _entityFramework.Remove(entityToAdd);
            }
        }


        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList<User>();
            return users;
        }


        public User GetSingleUser(int userId)
        {
            User? userData = _entityFramework.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault<User>();
            if (userData != null) {
                return userData;
            }
            throw new Exception ("User does not exist");
        }


        public UserSalary GetUserSalary(int userId)
        {
            UserSalary? userData = _entityFramework.UserSalary
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserSalary>();
            if (userData != null) {
                return userData;
            }
            throw new Exception ("User does not exist");
        }


        public UserJobInfo GetUserJobInfo(int userId)
        {
            UserJobInfo? userData = _entityFramework.UserJobInfo
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserJobInfo>();
            if (userData != null) {
                return userData;
            }
            throw new Exception ("User does not exist");
        }

    }
}