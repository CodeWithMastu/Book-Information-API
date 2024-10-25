namespace Book_Information_API.Models
{
    public class BookInfoDTO
    {
        public required string? BookName { get; set; }
        public required string? Author { get; set; }
        public required string? ISBN { get; set; }
        public required long YearPublished { get; set; }
    }
}
