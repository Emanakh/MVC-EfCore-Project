using Microsoft.EntityFrameworkCore;
using MVCproject.Models;

namespace MVCproject.Repository
{

	public interface IDepartmentRepo
	{
		IEnumerable<Department> GetAll();
		Department GetById(int id);
		int Add(Department department);
		int Update(Department dept, int id);
		int SoftDelete(int id);
		int HardDelete(int id);


	}
	public class DepartmentRepo : IDepartmentRepo
	{
		AppDbContext dbcontext; //= new AppDbContext();
		public DepartmentRepo(AppDbContext _dbcontext)
		{
			dbcontext = _dbcontext;
		}

		public IEnumerable<Department> GetAll()
		{
			return dbcontext.Departments.Where(a => a.Status == true).Include(d => d.Students).Include(d => d.Courses).ToList();
		}
		public Department GetById(int id)
		{
			return dbcontext.Departments.Include(d => d.Students).SingleOrDefault(s => s.DeptId == id);

		}
		public int Add(Department department)
		{
			dbcontext.Departments.Add(department);
			int Rows = dbcontext.SaveChanges();
			Console.WriteLine($"{Rows} added");
			return Rows;
		}
		public int Update(Department dept, int id)
		{
			var old = dbcontext.Departments.FirstOrDefault(d => d.DeptId == id);
			old.DeptName = dept.DeptName;
			int Rows = dbcontext.SaveChanges();
			Console.WriteLine($"{Rows} updated");
			return Rows;
		}
		public int SoftDelete(int id)
		{
			Department dept = GetById(id);
			dept.Status = false;
			dbcontext.Departments.Update(dept);
			int Rows = dbcontext.SaveChanges();
			Console.WriteLine($"{Rows} soft deleted");
			return Rows;

		}


		public int HardDelete(int id)
		{
			Department dept = GetById(id);
			dbcontext.Departments.Remove(dept);
			int Rows = dbcontext.SaveChanges();
			Console.WriteLine($"{Rows} hard deleted");
			return Rows;

		}


	}
}
