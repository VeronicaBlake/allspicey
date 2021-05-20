using System.ComponentModel.DataAnnotations;

namespace allspicey.Models
{
    public class Recipe
    {
        public string CreatorId { get; set; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = "No Description";

    }
}