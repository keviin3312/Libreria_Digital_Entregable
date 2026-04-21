using KtorresLibreriaDigital.Data;
using KtorresLibreriaDigital.Dtos;
using KtorresLibreriaDigital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KtorresLibreriaDigital.Controllers;

[ApiController]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("api/users/{userId:int}/books")]
    public async Task<ActionResult<Book>> CreateBook(int userId, CreateBookDto dto)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
            return NotFound(new { message = "Usuario no encontrado." });

        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            PublicationYear = dto.PublicationYear,
            CoverImageUrl = dto.CoverImageUrl,
            UserId = userId
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpGet("api/users/{userId:int}/books")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooksByUser(int userId)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
            return NotFound(new { message = "Usuario no encontrado." });

        var books = await _context.Books
            .Where(b => b.UserId == userId)
            .Include(b => b.Reviews)
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("api/books/{id:int}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books
            .Include(b => b.Reviews)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null)
            return NotFound(new { message = "Libro no encontrado." });

        return Ok(book);
    }

    [HttpPut("api/books/{id:int}")]
    public async Task<ActionResult<Book>> UpdateBook(int id, UpdateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null)
            return NotFound(new { message = "Libro no encontrado." });

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.PublicationYear = dto.PublicationYear;
        book.CoverImageUrl = dto.CoverImageUrl;

        await _context.SaveChangesAsync();
        return Ok(book);
    }

    [HttpDelete("api/books/{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null)
            return NotFound(new { message = "Libro no encontrado." });

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
