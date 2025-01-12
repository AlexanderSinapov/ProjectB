namespace ProjectB.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswer { get; set; }
        public string WorkType { get; set; }
    }
}
