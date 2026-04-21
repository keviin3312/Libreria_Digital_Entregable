namespace KtorresLibreriaDigital.Models;

public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;

    public int BookId { get; set; }
    public Book? Book { get; set; }
}
