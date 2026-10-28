using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters
{
    public class ApiLogginFilter : IActionFilter
    {

        private readonly ILogger<ApiLogginFilter> _logger;


        public ApiLogginFilter(ILogger<ApiLogginFilter> logger)
        {
            _logger = logger;
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Executado antes da ação
            _logger.LogInformation("### Excutando -> OnActionExecuting");
            _logger.LogInformation("######################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
            _logger.LogInformation("######################################################");

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Executado depois da ação
            _logger.LogInformation("### Excutando -> OnActionExecuting");
            _logger.LogInformation("######################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("######################################################");
        }

      
    }
}
