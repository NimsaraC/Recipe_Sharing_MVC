namespace Recipe.Models
{
    public class UserDash
    {
        public User User { get; set; }
        public IEnumerable<RecipeM> recipeMs { get; set; }
    }
}
