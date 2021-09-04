using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombre{ get; set; }
        public string Nombre2 { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public string Movil { get; set; }
        public string LugarTrabajo { get; set; }
        public string TelLaboral { get; set; }
        public int Estado { get; set; }
    }
}
