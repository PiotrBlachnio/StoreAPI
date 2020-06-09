using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public int joinedDate { get; set; }
    }
}