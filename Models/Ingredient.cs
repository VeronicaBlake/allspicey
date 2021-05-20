using System.ComponentModel.DataAnnotations;

namespace allspicey.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int RecipeId { get; private set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MeasuredIn { get; set; }

    }
}