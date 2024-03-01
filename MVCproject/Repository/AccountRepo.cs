using MVCproject.Models;
using MVCproject.ViewModels;

namespace MVCproject.Repository

{
    public interface IAccount
    {
        public User FindUser(LoginViewModel user);

    }

    public class AccountRepo : IAccount
    {
        AppDbContext dbcontext; //= new AppDbContext();
        public AccountRepo(AppDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public User FindUser(LoginViewModel user)
        {
            return dbcontext.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        }
    }
}
