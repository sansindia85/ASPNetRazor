using System.ComponentModel.DataAnnotations;

namespace WiredBrainCoffeeAdmin.Data
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MinLength(20, ErrorMessage = "The description should be at least 20 characters with meaningful content")]
        [Required]
        public string Description { get; set; }

        [MaxLength(30)]
        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }

        public IFormFile Upload { get; set; }
    }
}
