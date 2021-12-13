using System.Collections.Generic;

namespace PickPointTestApp.Logic.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalCost { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public List<ProductModel> Products { get; set; }
        public PostomatModel Postomat { get; set; }
    }


    //Зарегистрирован = 1
    //Принят на складе = 2
    //Выдан курьеру = 3
    //Доставлен в постамат = 4
    //Доставлен получателю = 5
    //Отменен = 6
    public enum OrderStatus
    {
        Unknown = 0,
        Registered = 1,
        AcceptedInWarehouse = 2,
        HandedToDelivery = 3,
        DeliveredToPostomat = 4,
        DeliveredToCustomer = 5,
        Cancelled = 6

    }
}
