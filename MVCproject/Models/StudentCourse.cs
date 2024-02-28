using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    public class StudentCourse
    {
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public int Grade { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
