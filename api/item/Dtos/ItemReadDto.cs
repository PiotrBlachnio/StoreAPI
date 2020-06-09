using System;

namespace Store.Dtos
{
    public class ItemReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}