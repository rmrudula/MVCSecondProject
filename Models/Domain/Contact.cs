namespace MVCSecondProject.Models.Domain
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string primaryPhone { get; set; }
        public string secondaryPhone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
