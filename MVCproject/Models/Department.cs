using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "department ID")]
        public int DeptId { get; set; }

        [Display(Name = "department Name")]
        public string DeptName { get; set; }
        public bool Status { get; set; } = true;
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();

    }
}
