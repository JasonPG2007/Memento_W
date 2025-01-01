namespace Memento_W.Models
{
    public class ProfileDTO
    {
        public int ProfileId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public string School { get; set; }
        public string City { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
