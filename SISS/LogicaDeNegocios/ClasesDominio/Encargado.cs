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
		private string puesto { get; set; }
		List<Proyecto> proyectos { get; set; }

		public void AñadirProyecto(Proyecto proyecto)
		{
			//TODO	
		}
	}
}
