using System;
using System.Collections.Generic;

namespace Subastas.Models
{
    public class Subasta
    {
        public int ID { get; set; }
        public int UsuarioID { get; set; }
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public float? Grade { get; set; }
        public string Status { get; set; }
    }
}