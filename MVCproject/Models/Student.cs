using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    public class Student
    {

        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [Range(20, 30, ErrorMessage = "The Age must be between 20 and 30")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "you muse choose a department")]
        [ForeignKey("Department")]
        public int? DeptNo { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-Z]+.[a-zA-Z]{2,4}")]
        public string Email { get; set; }
        [NotMapped]
        [Compare("Email")]
        public string ConfirmEmail { get; set; }
        public bool Status { get; set; } = true;
        public byte[] Image { get; set; }

        public Department Department { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();

    }
}
