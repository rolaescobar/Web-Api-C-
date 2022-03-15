using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiAutores.Filtros
{
    public class MiFiltroDeAccion : IActionFilter
    {
        private readonly ILogger<MiFiltroDeAccion> logger;

        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("Antes de Ejecutar la Accion");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("Despues de Ejecutar la Accion");
        }
    }
}
