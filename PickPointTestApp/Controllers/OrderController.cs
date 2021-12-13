using Microsoft.AspNetCore.Mvc;
using PickPointTestApp.Logic.Models;
using PickPointTestApp.Logic;

namespace PickPointTestApp.Controllers
{
    [Route("ru/Data/v1/orders")]
    public class OrderController : BaseController
    {
        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            var om = new OrderManager();
            var order = om.GetOrder(id);

            if (order == null)
            {
                return GetResultAction(new AnswerStatus(AnswerStatuses.NotFound,id.ToString()));
            }
            else
            {
                return Ok(GetResultJson(order));
            }
        }

        [HttpPut]
        [Route("create")]
        public ActionResult Create([FromBody] OrderModel model)
        {
            var om = new OrderManager();
            var status = om.CreateOrder(model);

            return GetResultAction(status);
        }

        
        [HttpPost]
        [Route("edit")]
        public ActionResult Edit([FromBody] OrderModel model)
        {
            var om = new OrderManager();
            var status = om.EditOrder(model);

            return GetResultAction(status);
        }

        [HttpPost]
        [Route("cancel/{id}")]
        public ActionResult Cancel(int id)
        {
            var om = new OrderManager();
            var status = om.CancelOrder(id);

            return GetResultAction(status);
        }

    }
}
