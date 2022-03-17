using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
