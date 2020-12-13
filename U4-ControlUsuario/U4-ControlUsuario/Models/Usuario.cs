using System;
using System.Collections.Generic;

namespace U4_ControlUsuario.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public int? Activo { get; set; }
        public string Codigo { get; set; }
    }
}
