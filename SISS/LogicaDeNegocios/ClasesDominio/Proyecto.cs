using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Proyecto
	{
		private string nombre { get; set; }
		private int cupo { get; set; }
		private int IDproyecto { get; set; }
		private EestadoProyecto estadoProyeto { get; set; }
		private string descripcionGeneral { get; set; }
		private string objetivoGeneral { get; set; }
		private List<Solicitud> solicitudes { get; set; }
		private List<Asignacion> asignaciones { get; set; }


		public int getDisponibilidad()
		{
			//TODO
			return 0;
		}

	}

	public enum EestadoProyecto
	{
		//TODO
	}
}
