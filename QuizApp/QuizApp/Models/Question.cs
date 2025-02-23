namespace QuizApp.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string MediaUrl { get; set; } // Optional for images or videos
        public List<Answer> Answers { get; set; } = new();
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}