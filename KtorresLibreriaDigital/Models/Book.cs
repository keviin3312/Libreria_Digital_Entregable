namespace KtorresLibreriaDigital.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public string? CoverImageUrl { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
