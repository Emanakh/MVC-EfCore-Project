using Microsoft.EntityFrameworkCore;
using MVCproject.Models;
using MVCproject.ViewModels;

namespace MVCproject.Repository

{
    public interface IAccount
    {
        public User FindUser(LoginViewModel user);
        public void AddNewUser(User user);
        public Role FindRole(string RoleName);

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
            return dbcontext.Users.Include(a => a.Roles).FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        }
        public void AddNewUser(User user)
        {
            dbcontext.Users.Add(user);
            int Rows = dbcontext.SaveChanges();
            Console.WriteLine($"{Rows} added");
        }
        public Role FindRole(string RoleName)
        {
            return dbcontext.Roles.FirstOrDefault(r => r.Name == RoleName);
        }
    }
}
