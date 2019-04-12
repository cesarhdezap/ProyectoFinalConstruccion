using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
    public abstract class Persona
    {
		private string Nombre { get; set; }
		private string CorreoElectronico { get; set; }
		private string Telefono { get; set; }
	}
}
