using System.Collections.Generic;

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

	
