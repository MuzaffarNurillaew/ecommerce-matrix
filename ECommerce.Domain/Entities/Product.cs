namespace ECommerce.Domain.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Can_Deleiver { get; set; }
        public decimal Price { get; set; }
        public long Category_Id { get; set;}
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get;set; }
    }
}
