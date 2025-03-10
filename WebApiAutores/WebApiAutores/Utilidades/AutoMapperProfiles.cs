﻿using AutoMapper;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<Autor, AutorDTOConLibros>()
                .ForMember(autorDTO => autorDTO.libros, opciones => opciones.MapFrom(MapAutoresDTOLibros));

           
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.AutoresLibros,opciones =>opciones.MapFrom(MapAutoresLibros));

            CreateMap<Libro, LibroDTO>();
            CreateMap<Libro,LibroDTOConAutores>()
                .ForMember(LibroDTO => LibroDTO.Autores,opciones =>opciones.MapFrom(MapLibroDTOAutores));
           
            CreateMap<LibroPatchDTO,Libro>().ReverseMap();
            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario,ComentarioDTO>();
        }

        private List<LibroDTO> MapAutoresDTOLibros(Autor autor,AutorDTO autorDTO)
        { 
            var resultado =  new List<LibroDTO>();
            if (autor.AutoresLibros == null) { return resultado; }

            foreach (var autorLibro in autor.AutoresLibros)
            {
                resultado.Add(new LibroDTO()
                {
                    Id = autorLibro.LibroId,
                    Titulo = autorLibro.libro.Titulo

                });
            }

            return resultado;
        }

        private List<AutorDTO> MapLibroDTOAutores(Libro libro, LibroDTO libroDTO)
        { 
            var resultado = new List<AutorDTO>();
            if (libro.AutoresLibros == null) { return resultado; }

            foreach (var autorLibro in libro.AutoresLibros)
            {
                resultado.Add(new AutorDTO()
                {
                    Id = autorLibro.AutorId,
                    Nombre = autorLibro.autor.Nombre
                });
               
            }

            return resultado;
        }

        private List<AutorLibro> MapAutoresLibros(LibroCreacionDTO libroCreacionDTO, Libro libro)
        { 
           var resultado =  new List<AutorLibro>();

            if (libroCreacionDTO == null){ return resultado;}

            foreach (var autorId in libroCreacionDTO.AutoresIds)
            {
                resultado.Add(new AutorLibro() { AutorId = autorId });

            }

            return resultado;
        
        }
    }
}
