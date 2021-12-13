using System.Collections.Generic;

namespace PickPointTestApp.DB.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public decimal TotalCost { get; set; }
        public int PostomatId { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }


        public ICollection<Product> Products { get; set; }
        public Postomat Postomat { get; set; }
    }
}
