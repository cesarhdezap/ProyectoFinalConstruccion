using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
    public class ServiciosDeSesion
    {
        public TipoDeSesion ObtenerTipoDeSesionPorCorreo(string correo)
        {
            throw new NotImplementedException("ServiciosDeSesion.ObtenerTipoDeSesionPorCorreo");
        }
        public enum TipoDeSesion
        {
            NoValido = -1,
            Director = 0,
            Coordinador = 1,
            Tecnico = 2,
            Alumno = 3,
        }
    }
}
