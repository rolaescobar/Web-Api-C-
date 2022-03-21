namespace WebApiAutores.Entidades
{
    public class AutorLibro
    {
        public int LibroId { get; set; }
        public int AutorId { get; set; }

        public int Orden { get; set; }

        public Libro libro { get; set; }
        public Autor autor { get; set; }    


    }
}
