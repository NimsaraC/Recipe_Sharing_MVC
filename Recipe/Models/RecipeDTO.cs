﻿namespace Recipe.Models
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Steps { get; set; }
        public string CookingTime { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }
        public IFormFile ImageFileName { get; set; }
    }
}