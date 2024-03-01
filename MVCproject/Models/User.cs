namespace MVCproject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
        public int Age { get; set; }

        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
    }
}
