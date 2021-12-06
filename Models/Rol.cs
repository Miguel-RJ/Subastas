using System;
using System.Collections.Generic;

namespace Subastas.Models
{
    public class Rol
    {
        public int RolID { get; set; }
        public string NombreRol { get; set; }
        public ICollection<Usuario> Usuario { get; set; }

    }
}