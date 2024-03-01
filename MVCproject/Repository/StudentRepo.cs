using Microsoft.EntityFrameworkCore;
using MVCproject.Models;
using System.Security.Cryptography;

namespace MVCproject.Repository
{
	public interface IStudentRepo
	{
		IEnumerable<Student> GetAll();
		Student GetById(int id);
		int Add(Student student);
		int Update(Student old, Student st);
		int SoftDelete(int id);
		int HardDelete(int id);
	}
	public class StudentRepo : IStudentRepo
	{
		AppDbContext dbcontext;  // = new AppDbContext();
		public StudentRepo(AppDbContext _dbcontext)
		{
			dbcontext = _dbcontext;
		}
		public int Add(Student student)
		{
			dbcontext.Students.Add(student);
			int Rows = dbcontext.SaveChanges();
			Console.WriteLine($"{Rows} added");
			return Rows;

		}

		public IEnumerable<Student> GetAll()
		{
			//?? include student  courses ???
			return dbcontext.Students.Include(s => s.Department).Where(s => s.Status == true).ToList();
		}

		public Student GetById(int id)
		{
			return dbcontext.Students.Include(f => f.Department).SingleOrDefault(d => d.Id == id);
		}

		public int HardDelete(int id)
		{
			Student st = GetById(id);
			dbcontext.Students.Remove(st);
			int Rows = dbcontext.SaveChanges();
			Console.WriteLine($"{Rows} hard deleted");
			return Rows;
		}

		public int SoftDelete(int id)
		{
			Student st = GetById(id);
			st.Status = false;
			dbcontext.Students.Update(st);
			int Rows = dbcontext.SaveChanges();
			Console.WriteLine($"{Rows} soft deleted");
			return Rows;
		}

		public int Update(Student old, Student st)
		{
			old.Age = st.Age;
			old.Email = st.Email;
			old.DeptNo = st.DeptNo;
			old.Name = st.Name;
			int Rows = dbcontext.SaveChanges();
			Console.WriteLine($"{Rows} Updated");
			return Rows;
		}
	}
}
