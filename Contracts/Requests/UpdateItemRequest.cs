namespace Store.Contracts.Requests
{
    public class UpdateItemRequest
    {
        public string Name { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }
    }
}