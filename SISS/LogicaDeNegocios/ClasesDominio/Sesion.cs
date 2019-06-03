using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogicaDeNegocios.ServiciosDeSesion;

namespace LogicaDeNegocios.ClasesDominio
{
    public class Sesion
    {
        public string IDUsuario { get; set; }
        public TipoDeSesion TipoDeUsuario { get; set; }            
        public string Nombre { get; set; }

    }
}
