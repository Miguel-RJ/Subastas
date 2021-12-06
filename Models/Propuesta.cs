using System;
using System.Collections.Generic;

namespace Subastas.Models
{
    public class Propuesta
    {
        public int ID { get; set; }
        public int SubastaID { get; set; }
        public int UsuarioID { get; set; }

        public string TituloPropuesta { get; set; }
        public string Descripcion { get; set; }
        public char Estatus { get; set; }
        public int Calificacion { get; set; }
    }
}