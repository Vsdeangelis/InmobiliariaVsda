using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Inmueble
    {
        public int  IdInmueble { get; set; }
        public int IdPropietario { get; set; }
        public Persona Propietario { get; set; }
        public string  Calle { get; set; }
        public string Numero { get; set; }
        public string Barrio { get; set; }
        public string Manzana { get; set; }
        public string Dpto { get; set; }
        public string Piso { get; set; }
        public int IdTipo { get; set; }
        public TipoInmueble tipo { get; set; }
        public string Uso { get; set; }
        public string Descripcion { get; set; }
        public string Ambientes { get; set; }
        public decimal Precio { get; set; }
        public string Disponibilidad { get; set; }
        public int Estado { get; set; }


    }
}
