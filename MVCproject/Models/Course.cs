namespace MVCproject.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CrsName { get; set; }
        public int Duration { get; set; }
        public ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
    }
}
