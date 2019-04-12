using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Proyecto
	{
		private int IDproyecto { get; set; }
		private string nombre { get; set; }
		private string descripcionGeneral { get; set; }
		private string objetivoGeneral { get; set; }
		private int cupo { get; set; }
		private List<Asignacion> asignaciones { get; set; }


		public int getDisponibilidad()
		{
			//TODO
			return 0;
		}
		public void AsignarAlumno(Alumno alumno)
		{
			//TODO
		}

		
	}
}
