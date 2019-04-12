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
		private string CorreoElectronico { get; set; }
		private string Direccion { get; set; }
		private string Telefono { get; set; }
		private string Nombre { get; set; }
		private List<Encargado> Encargados { get; set; }

        public void AñadirEncargado(Encargado encargado)
		{
			//TODO
		}
	}
}

	
