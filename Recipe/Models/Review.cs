namespace Recipe.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
    }
}
