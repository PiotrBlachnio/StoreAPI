using System.ComponentModel.DataAnnotations;

namespace Store.Dtos
{
    public class ItemUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public double Price { get; set; }
    }
}