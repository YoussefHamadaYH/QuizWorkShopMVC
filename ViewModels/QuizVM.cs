namespace QuizWorkShopMVC.DTO
{
    public class QuizVM
    {
            public int ? QuizId { get; set; }
            public string QuizName { get; set; }
            public string QuizDescription { get; set; }
            public string ?ImageUrl { get; set; }
            public DateTime Date { get; set; }
    }
}
