using System;
using System.Collections.Generic;

namespace u4_2_RolesUsuario.Models
{
    public partial class Director
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
