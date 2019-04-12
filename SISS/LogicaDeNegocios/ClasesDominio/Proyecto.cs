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
		private string Nombre { get; set; }
		private string DescripcionGeneral { get; set; }
		private string ObjetivoGeneral { get; set; }
		private int Cupo { get; set; }
		private List<Asignacion> Asignaciones { get; set; }


		public int ObtenerDisponibilidad()
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
