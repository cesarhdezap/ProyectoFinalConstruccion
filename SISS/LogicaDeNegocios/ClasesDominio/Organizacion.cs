using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
	public class Organizacion
	{
		public int IDOrganizacion { get; set; }
		public string CorreoElectronico { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Nombre { get; set; }
		public List<Encargado> Encargados { get; set; }

        public void AñadirEncargado(Encargado encargado)
		{
			//TODO
			throw new NotImplementedException();
		}
	}
}

	
