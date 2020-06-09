using System.ComponentModel.DataAnnotations;

namespace Store.Database.Models
{
    public class Item 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public double Price { get; set; }
    }
}