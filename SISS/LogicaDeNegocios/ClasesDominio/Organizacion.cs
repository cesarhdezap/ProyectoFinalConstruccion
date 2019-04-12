using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Organizacion
	{
		private int IDOrganizacion { get; set; }
		private string correoElectronico { get; set; }
		private string direccion { get; set; }
		private string telefono { get; set; }
		private string nombre { get; set; }
		private List<Encargado> encargados { get; set; }
		public void AñadirEncargado(Encargado encargado)
		{
			//TODO
		}
	}
}

	
