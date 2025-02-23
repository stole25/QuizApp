namespace QuizApp.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string Response { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}