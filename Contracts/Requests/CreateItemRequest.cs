namespace Store.Contracts.Requests
{
    public class CreateItemRequest
    {
        public string Name { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }
    }
}