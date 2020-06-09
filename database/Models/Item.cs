using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Database.Models
{
    public class Item 
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }
    }
}