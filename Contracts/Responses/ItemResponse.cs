using System;

namespace Store.Contracts.Responses
{
    public class ItemResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }
    }
}