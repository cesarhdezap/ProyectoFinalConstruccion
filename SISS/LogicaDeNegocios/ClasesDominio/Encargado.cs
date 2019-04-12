using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Encargado : Persona
	{
		private int IDEncargado { get; set; }
		private string Puesto { get; set; }
		List<Proyecto> Proyectos { get; set; }

		public void AñadirProyecto(Proyecto proyecto)
		{
			//TODO	
		}
	}
}
