using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
	public class Proyecto
	{
		public int IDProyecto { get; set; }
		public string Nombre { get; set; }
		public string DescripcionGeneral { get; set; }
		public string ObjetivoGeneral { get; set; }
		public int Cupo { get; set; }
		public List<Asignacion> Asignaciones { get; set; }


		public int ObtenerDisponibilidad()
		{
			//TODO
			throw new NotImplementedException();
		}

		public void AsignarAlumno(Alumno alumno)
		{
			//TODO
			throw new NotImplementedException();
		}

		public enum EstadoProyecto
        {

        }
	}
}
