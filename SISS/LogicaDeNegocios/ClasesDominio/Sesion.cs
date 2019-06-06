using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogicaDeNegocios.Servicios.ServiciosDeSesion;

namespace LogicaDeNegocios.ClasesDominio
{
    public class Sesion
    {
        public string IDUsuario { get; set; }
        public TipoDeSesion TipoDeUsuario { get; set; }            

    }
}
