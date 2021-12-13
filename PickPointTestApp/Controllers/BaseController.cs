using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using PickPointTestApp.Logic.Models;
using PickPointTestApp.Logic;

namespace PickPointTestApp.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        public ActionResult GetResultAction(AnswerStatus status)
        {
            if (status == AnswerStatuses.NotFound)
            {
                return NotFound(GetResultJson(status));
            }
            else if (status == AnswerStatuses.Forbidden)
            {
                return Forbid();
            }
            else if (status.ResultCode == false)
            {
                return BadRequest(GetResultJson(status));
            }
            else
            {
                return Ok(GetResultJson(status));
            }
        }

        [NonAction]
        public string GetResultJson(object result)
        {

            return JsonConvert.SerializeObject(result, new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                NullValueHandling = NullValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter(), new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'" } },
               
            });
        }

    }
}
