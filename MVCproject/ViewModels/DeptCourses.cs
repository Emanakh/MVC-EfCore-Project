using Microsoft.EntityFrameworkCore;
using MVCproject.Models;

namespace MVCproject.ViewModels
{
    public class DeptCourses
    {
        public Department department { get; set; }
        public List<Course> CoursesNotInDept { get; set; }
        public Course Course { get; set; }
        public DeptCourses(int deptId, AppDbContext context, int CrsId)
        {
            department = context.Departments.Include(d => d.Students).FirstOrDefault(c => c.DeptId == deptId);
            Course = context.Courses.FirstOrDefault(c => c.Id == CrsId);
        }
        public DeptCourses(int Deptid, AppDbContext context)
        {
            department = context.Departments.Include(d => d.Courses).FirstOrDefault(c => c.DeptId == Deptid);
            var allCourses = context.Courses.ToList();
            CoursesNotInDept = allCourses.Except(department.Courses).ToList();
        }

    }
}
