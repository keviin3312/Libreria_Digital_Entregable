using KtorresLibreriaDigital.Data;
using KtorresLibreriaDigital.Dtos;
using KtorresLibreriaDigital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KtorresLibreriaDigital.Controllers;

[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReviewsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("api/books/{bookId:int}/reviews")]
    public async Task<ActionResult<Review>> CreateReview(int bookId, CreateReviewDto dto)
    {
        if (dto.Rating < 1 || dto.Rating > 5)
            return BadRequest(new { message = "La calificación debe estar entre 1 y 5." });

        var book = await _context.Books.FindAsync(bookId);
        if (book is null)
            return NotFound(new { message = "Libro no encontrado." });

        var review = new Review
        {
            Rating = dto.Rating,
            Comment = dto.Comment,
            BookId = bookId
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReviewsByBook), new { bookId }, review);
    }

    [HttpGet("api/books/{bookId:int}/reviews")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByBook(int bookId)
    {
        var bookExists = await _context.Books.AnyAsync(b => b.Id == bookId);
        if (!bookExists)
            return NotFound(new { message = "Libro no encontrado." });

        var reviews = await _context.Reviews
            .Where(r => r.BookId == bookId)
            .ToListAsync();

        return Ok(reviews);
    }

    [HttpPut("api/reviews/{id:int}")]
    public async Task<ActionResult<Review>> UpdateReview(int id, UpdateReviewDto dto)
    {
        if (dto.Rating < 1 || dto.Rating > 5)
            return BadRequest(new { message = "La calificación debe estar entre 1 y 5." });

        var review = await _context.Reviews.FindAsync(id);
        if (review is null)
            return NotFound(new { message = "Reseña no encontrada." });

        review.Rating = dto.Rating;
        review.Comment = dto.Comment;

        await _context.SaveChangesAsync();
        return Ok(review);
    }

    [HttpDelete("api/reviews/{id:int}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review is null)
            return NotFound(new { message = "Reseña no encontrada." });

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
