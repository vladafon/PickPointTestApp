using System.Linq;
using PickPointTestApp.Logic.Models;
using PickPointTestApp.DB;
using PickPointTestApp.DB.Models;
using System.Text.RegularExpressions;

namespace PickPointTestApp.Logic
{
    public class OrderManager
    {
        public OrderModel GetOrder(int orderId)
        {
            using(var dc = new PickPointContext())
            {
                var order = dc.Orders.Where(c => c.Id == orderId)
                    .Select(c => new OrderModel
                    {
                        Id = c.Id,
                        CustomerName = c.CustomerName,
                        CustomerPhone = c.CustomerPhone,
                        Status = (OrderStatus)c.Status,
                        TotalCost = c.TotalCost,
                        Postomat = new PostomatModel
                        {
                            Id = c.Postomat.Id,
                            Address = c.Postomat.Address,
                            IsActive = c.Postomat.IsActive,
                            Number = c.Postomat.Number
                        },
                        Products = c.Products.Select(x=>new ProductModel
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).ToList()
                    }).FirstOrDefault();
                return order;
            }
        }

        public AnswerStatus CreateOrder(OrderModel model)
        {
            using (var dc = new PickPointContext())
            {
                //_________validation model__________

                if (model.Products == null || !model.Products.Any())
                    return new AnswerStatus(AnswerStatuses.TooFewItems, "Products");

                if (model.Products.Count() > 10)
                    return new AnswerStatus(AnswerStatuses.TooManyItems, "Products");

                //phone check
                Regex rx = new Regex(@"^\+7[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}$",
                             RegexOptions.Compiled | RegexOptions.IgnoreCase);

                // Find matches.
                MatchCollection matches = rx.Matches(model.CustomerPhone);

                if (matches.Count() != 1)
                {
                    return new AnswerStatus(AnswerStatuses.WrongFormat, "CustomerPhone");
                }


                //postomat checking

                if (model.Postomat == null || string.IsNullOrEmpty(model.Postomat.Number))
                {
                    return new AnswerStatus(AnswerStatuses.WrongFormat, "Postomat");
                }

                var pm = new PostomatManager();
                if (pm.CheckPostomatNumber(model.Postomat.Number))
                {
                    return new AnswerStatus(AnswerStatuses.WrongFormat, model.Postomat.Number);
                }

                var postomat = pm.GetPostomat(model.Postomat.Number);
                
                if (postomat == null)
                {
                    return new AnswerStatus(AnswerStatuses.NotFound, model.Postomat.Number);
                }

                if (postomat.IsActive == false)
                {
                    return new AnswerStatus(AnswerStatuses.Forbidden, model.Postomat.Number);
                }



                var dbOrder = new Order
                {
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    PostomatId = model.Postomat.Id,
                    Status = (int)model.Status,
                    TotalCost = model.TotalCost,
                    Products = model.Products.Select(c => new Product
                    {
                        Name = c.Name
                    }).ToList()
                };

                dc.Add(dbOrder);
                dc.SaveChanges();

                return new AnswerStatus(AnswerStatuses.Created, dbOrder.Id.ToString());
            }
        }
        public AnswerStatus EditOrder(OrderModel model)
        {
            using (var dc = new PickPointContext())
            {
                var dbOrder = dc.Orders.Where(c => c.Id == model.Id).FirstOrDefault();

                if (dbOrder == null)
                    return new AnswerStatus(AnswerStatuses.NotFound,model.Id.ToString());

                //__________validation model________

                if (model.Products == null || !model.Products.Any())
                    return new AnswerStatus(AnswerStatuses.TooFewItems, "Products");

                if (model.Products.Count() > 10)
                    return new AnswerStatus(AnswerStatuses.TooManyItems, "Products");

                //phone check
                Regex rx = new Regex(@"\+7[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

                // Find matches.
                MatchCollection matches = rx.Matches(model.CustomerPhone);

                if (matches.Count() != 1)
                {
                    return new AnswerStatus(AnswerStatuses.WrongFormat, "CustomerPhone");
                }



                dbOrder.CustomerPhone = model.CustomerPhone;
                dbOrder.TotalCost = model.TotalCost;
                dbOrder.CustomerName = model.CustomerName;
                dbOrder.Products = model.Products.Select(c => new Product
                {
                    Name = c.Name
                }).ToList();
                return new AnswerStatus(AnswerStatuses.Edited, dbOrder.Id.ToString());
            }
        }

        public AnswerStatus CancelOrder(int orderId)
        {
            using (var dc = new PickPointContext())
            {
                var dbOrder = dc.Orders.Where(c => c.Id == orderId).FirstOrDefault();

                if (dbOrder == null)
                    return new AnswerStatus(AnswerStatuses.NotFound, orderId.ToString());

                dbOrder.Status = (int)OrderStatus.Cancelled;
                dc.SaveChanges();
                return new AnswerStatus(AnswerStatuses.Cancelled, dbOrder.Id.ToString());

            }
        }
    }
}
