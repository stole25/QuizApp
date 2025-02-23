using System.Collections.Generic;

namespace QuizApp.Shared.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; } = new();
    }
}