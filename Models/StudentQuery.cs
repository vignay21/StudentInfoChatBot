namespace StudentInfoChatBot.Models
{
    public class StudentQuery
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsValid { get; set; }
        public bool IsInvalid { get; set; }
    }
}
