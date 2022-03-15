using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
using WebApiAutores.Filtros;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{

    [ApiController]
    [Route("api/autores")]
    // [Authorize]
    public class AutoresController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSinglenton servicioSinglenton;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, IServicio servicio, ServicioTransient servicioTransient, ServicioScoped servicioScoped, ServicioSinglenton servicioSinglenton, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.servicio = servicio;
            this.servicioTransient = servicioTransient;
            this.servicioScoped = servicioScoped;
            this.servicioSinglenton = servicioSinglenton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        //[ResponseCache(Duration =10)]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
      public ActionResult ObtenerGuids()
        {
            return Ok(new
                {
             
             
               
                ServicioA_Transient = servicio.ObtenerTrasient(),
                AutoresController_Transient = servicioTransient.Guid,

                ServicioA_Scoped = servicio.ObtenerScoped(),
                AutoresController_Scoped = servicioScoped.Guid,

                ServicioA_Singlenton = servicio.ObtenerSinglenton(),
                AutoresController_Singlenton = servicioSinglenton.Guid


            });
        
        
        }

        [HttpGet("listado")]
        [HttpGet("/listado")]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public  async Task<ActionResult<List<Autor>>> Get()
        {
            logger.LogInformation("Estamos Obteniendo los Autores");
            servicio.Realizartarea();
            return await context.Autores.Include(x => x.Libros).ToListAsync();

        
        }

        [HttpGet("primero")] //api/autories => primero
        public async Task<ActionResult<Autor>> PrimerAutor()
        { 
        
            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        { 
          var autor =  await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {

                return NotFound();
            }

            return autor;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get([FromRoute]string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (autor == null)
            {

                return NotFound();
            }

            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Autor autor)
        {

            var  existeAutorConElMismoNombre = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

            if (existeAutorConElMismoNombre)
            {
                return BadRequest($"Ya existe un auto con el nombre {autor.Nombre}");
            }

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        
        }

        [HttpPut ("{id:int}")] // api/autores/1
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la Url");
            }
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();    
        }

        [HttpDelete("{id:int}")] // api/autores/2

        public async Task<ActionResult> Delete(int id)
        { 
        
            var existe = await context.Autores.AnyAsync(x => x.Id == id );

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
