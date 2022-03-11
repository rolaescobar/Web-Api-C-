using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo nombre es requerido")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public List<Libro> Libros { get; set; }
    }
}
