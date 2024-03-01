using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Models;
using MVCproject.ViewModels;

namespace MVCproject.Controllers
{
	public class DepartmentCourseController : Controller
	{
		AppDbContext db = new AppDbContext();
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult ShowCourses(int id)
		{
			Department dept = db.Departments.Include(c => c.Courses).FirstOrDefault(a => a.DeptId == id);
			return View(dept);
		}
		public IActionResult ManageCourses(int id)
		{
			DeptCourses model = new DeptCourses(id, db);
			return View(model);
		}
		[HttpPost]
		public IActionResult ManageCourses(int id, List<int> CoursesToRemove, List<int> CoursesToAdd)
		{
			Department dept = db.Departments.Include(d => d.Courses).FirstOrDefault(a => a.DeptId == id);
			foreach (int c in CoursesToRemove)
			{
				Course cs = db.Courses.FirstOrDefault(a => a.Id == c);
				dept.Courses.Remove(cs);
			}
			foreach (int c in CoursesToAdd)
			{
				Course cs = db.Courses.FirstOrDefault(a => a.Id == c);
				dept.Courses.Add(cs);
			}
			db.SaveChanges();
			return RedirectToAction("Index", "Department");
		}

		public IActionResult SetGrade(int deptId, int crsId)
		{
			DeptCourses model = new DeptCourses(deptId, db, crsId);
			return View(model);

		}

		//manage the grades .. if thre was past grades .. display them in the input box
		[HttpPost]
		public IActionResult SetGrade(int crsId, Dictionary<int, int> degree)
		{
			foreach (var item in degree)
			{
				var searchItem = db.StudentCourses.FirstOrDefault(s => s.StudentId == item.Key && s.CourseId == crsId);
				if (searchItem == null)
				{
					Console.WriteLine("not found grade");
					StudentCourse sc = new StudentCourse() { CourseId = crsId, Grade = item.Value, StudentId = item.Key };
					db.StudentCourses.Add(sc);
					db.SaveChanges();

				}
				else
				{
					Console.WriteLine(" found grade");
					searchItem.Grade = item.Value;
					db.StudentCourses.Update(searchItem);
					db.SaveChanges();
				}
			}

			return RedirectToAction("Index", "Department");
		}
	}
}
