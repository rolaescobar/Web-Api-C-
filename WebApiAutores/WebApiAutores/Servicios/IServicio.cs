namespace WebApiAutores.Servicios
{
    public interface IServicio
    {
        Guid ObtenerScoped();
        Guid ObtenerSinglenton();
        Guid ObtenerTrasient();
        void Realizartarea();
    }
    public class ServicioA : IServicio
    {
        private readonly ILogger<ServicioA> logger;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSinglenton servicioSinglenton;

        public  ServicioA(ILogger<ServicioA> logger,ServicioTransient servicioTransient,ServicioScoped servicioScoped,ServicioSinglenton servicioSinglenton)    
        {
            this.logger = logger;
            this.servicioTransient = servicioTransient;
            this.servicioScoped = servicioScoped;
            this.servicioSinglenton = servicioSinglenton;
        }

        public Guid ObtenerTrasient() { return servicioTransient.Guid; }
        public Guid ObtenerScoped() { return servicioScoped.Guid; }
        public Guid ObtenerSinglenton() { return servicioSinglenton.Guid; }

        public void Realizartarea()
        {

        }
    }

    public class ServicioB : IServicio
    {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSinglenton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTrasient()
        {
            throw new NotImplementedException();
        }

        public void Realizartarea()
        {
    
        }
    }

    public class ServicioTransient
    { 
         public Guid Guid = Guid.NewGuid();
    
    }

    public class ServicioScoped
    {
        public Guid Guid = Guid.NewGuid();

    }

    public class ServicioSinglenton
    {
        public Guid Guid = Guid.NewGuid();

    }

}
