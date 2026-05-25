



namespace StudentManager.Shared.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string?Email { get; set; }
        // removed Topic, bcz why would we need Topic in a student data container?
        public DateTime CreatedAt { get; set; }

    }
}
