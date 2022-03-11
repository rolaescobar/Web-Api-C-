namespace WebApiAutores.Middlewares
{

    //metodo de extension

    public static class  LoguearRespuestaHttpMiddlewareExtensions
     {
        public static IApplicationBuilder UseLoguearRespuestaHttp(this IApplicationBuilder app)
        { 
             return  app.UseMiddleware<LoguearRespuestaHttpMiddleware>();
        
        }
     }


    public class LoguearRespuestaHttpMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<LoguearRespuestaHttpMiddleware> logger;

        public LoguearRespuestaHttpMiddleware(RequestDelegate siguiente,ILogger<LoguearRespuestaHttpMiddleware> logger)
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }

        //invoke o invokeasync
        public async Task InvokeAsync(HttpContext contexto)
        {
            using (var ms = new MemoryStream())
            {
                var cuerpoOriginalRespuesta = contexto.Response.Body;
                contexto.Response.Body = ms;
                await siguiente(contexto);
                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpoOriginalRespuesta);
                contexto.Response.Body = cuerpoOriginalRespuesta;

                logger.LogInformation(respuesta);
            }

        }
    }
}
