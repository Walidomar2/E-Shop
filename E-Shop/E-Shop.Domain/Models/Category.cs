using System.ComponentModel.DataAnnotations;

namespace E_Shop.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
