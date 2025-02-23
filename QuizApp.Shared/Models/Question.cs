using System.Collections.Generic;

namespace QuizApp.Shared.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string MediaUrl { get; set; } // Optional
        public List<Answer> Answers { get; set; } = new();
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}