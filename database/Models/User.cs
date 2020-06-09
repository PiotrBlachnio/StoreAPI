using System.ComponentModel.DataAnnotations;

namespace Store.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string username { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public int joinedDate { get; set; }
    }
}