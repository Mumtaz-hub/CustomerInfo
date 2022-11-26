using System.Text.Json;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result)
        {
            if (!result.IsFailure)
                return new OkResult();

            return new BadRequestObjectResult(result.HtmlFormattedFailures);
        }

        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new JsonResult(result.Value, new JsonSerializerOptions()  
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    //ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
               
            }
            return new BadRequestObjectResult(result.HtmlFormattedFailures);
        }
    }
}
