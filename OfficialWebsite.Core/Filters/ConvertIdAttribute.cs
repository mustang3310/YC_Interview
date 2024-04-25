namespace OfficialWebsite.Core.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using System;

    public class ConvertIdAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            IDictionary<string, object?> actionArduments = context.ActionArguments;
            RouteData routeData = context.RouteData;

            if (routeData.Values.TryGetValue("id", out object? id))
            {
                string idString = id?.ToString() ?? string.Empty;
                int result = int.Parse(Crypto.Aes.Decrypt(ConvertType.Aes.ConvertToByte(idString)));
                actionArduments["id"] = result;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
