namespace Recipe.Models
{
    public class RecipeDetails
    {
        public RecipeM RecipeM { get; set; }
        public RecipeDTO RecipeDTO { get; set; }
        public Review Review { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
