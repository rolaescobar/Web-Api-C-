namespace WebApiAutores.Servicios
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "Archivo 1.txt";
        private Timer timer;

        public EscribirEnArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            timer = new Timer(DoWork, null,TimeSpan.Zero,TimeSpan.FromSeconds(5));
            Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }

        private void DoWork(Object state)
        {

            Escribir("Proceso en Ejecucion" + DateTime.Now.ToString("dd/MM/yyy hh:mm:ss"));
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            Escribir("Proceso Finalizado");
            return Task.CompletedTask;

        }

        private void Escribir(string mensaje)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";

            using (StreamWriter writer = new StreamWriter(ruta, append: true))
            { 
               writer.WriteLine(mensaje);
            }
        }
    }
}
