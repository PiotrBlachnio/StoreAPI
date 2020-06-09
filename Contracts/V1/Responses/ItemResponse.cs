namespace Store.Contracts.V1.Responses
{
    public class ItemResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }
    }
}