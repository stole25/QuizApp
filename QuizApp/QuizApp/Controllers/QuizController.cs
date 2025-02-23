using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Server.Data;
using QuizApp.Shared.Models;

namespace QuizApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizDbContext _context;

        public QuizController(QuizDbContext context)
        {
            _context = context;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            var quizzes = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .ToListAsync();
            
            return Ok(quizzes);
        }

        // GET: api/Quiz/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetQuiz), new { id = quiz.QuizId }, quiz);
        }

        // Additional endpoints (PUT, DELETE) can be added as needed.
    }
}