using System;
using System.Collections.Generic;

namespace Subastas.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public int RolID { get; set; }
        public string NombreUsuario { get; set; }
        public string RFC { get; set; }
        public string Descripcion { get; set; }
        public int Calificacion { get; set; }
        public ICollection<Subasta> Subasta { get; set; }
        public ICollection<Propuesta> Propuesta { get; set; }

    }
}