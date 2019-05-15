using System.Collections.Generic;

namespace LogicaDeNegocios
{
	public class Alumno : Persona
	{
		private const int MAXIMO_DE_ASIGNACIONES = 2;
		public string Matricula { get; set; }
		public string Carrera { get; set; }
		public string Contraseña { get; set; }
		public EstadoAlumno EstadoAlumno { get; set; }
		public List<Asignacion> Asignaciones { get; set; }	

		public void DarDeBaja()
		{
			EstadoAlumno = EstadoAlumno.Desactivado;
		}

		public void AceptarAlumno()
		{
			EstadoAlumno = EstadoAlumno.Aceptado;
		}

		public void RechazarAlumno()
		{
			EstadoAlumno = EstadoAlumno.Rechazado;
		}
    }

	public enum EstadoAlumno
	{
		EnEspera, 
		Liberado,
		Aceptado,
		Rechazado,
		Desactivado,
		EsperandoAsignacion
	}
}
