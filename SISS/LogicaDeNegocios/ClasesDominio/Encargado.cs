using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
	public class Encargado : Persona
	{
		public int IDEncargado { get; set; }
		public string Puesto { get; set; }
		public List<Proyecto> Proyectos { get; set; }

		public void AñadirProyecto(Proyecto proyecto)
		{
			//TODO	
			throw new NotImplementedException();
		}
	}
}
