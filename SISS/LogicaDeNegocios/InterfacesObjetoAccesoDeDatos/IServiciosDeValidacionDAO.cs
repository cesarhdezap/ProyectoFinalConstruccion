using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Interfaces
{
    interface IServiciosDeValidacionDAO
    {
		public int ContarOcurrenciasDeCorreo(string correo);

		public int ContarOcurrenciasDeMatricula(string matricula);
	}
}
