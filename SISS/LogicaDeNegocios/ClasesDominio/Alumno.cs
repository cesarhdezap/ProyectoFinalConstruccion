using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Alumno : CuentaHabiente
	{
		private const int MAXIMO_DE_ASIGNACIONES = 2;
		private string carrera { get; set; }
		private string matricula { get; set; }
		private EestadoAlumno estadoAlumno { get; set; }
		private List<Solicitud> solicitud { get; set; }
		private List<Asignacion> asignaciones { get; set; }

		public void Asignar(Proyecto proyecto)
		{
			if (asignaciones.Count < MAXIMO_DE_ASIGNACIONES)
			{
				Asignacion asignacion = new Asignacion(this, proyecto);
				asignaciones.Add(asignacion);
			} 
		}
	}

	public enum EestadoAlumno
	{
		EnEspera, 
		Liberado,
		Aceptado,
		Rechazado,
		Desactivado,
		EsperandoAsignacion
	}
}
