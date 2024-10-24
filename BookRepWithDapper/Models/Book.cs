namespace BookRepWithDapper.Models
{
    public class Book
    {
        public long ID { get; set; }
        public string? BookName { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public long YearPublished { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime? Deleted_At { get; set; }
        public bool IsDeleted { get; set; }

    }
}
