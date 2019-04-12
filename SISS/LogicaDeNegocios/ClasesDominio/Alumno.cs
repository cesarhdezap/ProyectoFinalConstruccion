using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Alumno : Persona
	{
		private const int MAXIMO_DE_ASIGNACIONES = 2;

		public string matricula { get; set; }
		public string carrera { get; set; }
		public string contraseña { get; set; }
		public EestadoAlumno estadoAlumno { get; set; }
		private List<Asignacion> asignaciones { get; set; }	

		public void DarDeBaja()
		{
			estadoAlumno = EestadoAlumno.Desactivado;

		}

		public void Aceptar()
		{
			estadoAlumno = EestadoAlumno.Aceptado;
		}

		public void Rechazar()
		{
			estadoAlumno = EestadoAlumno.Rechazado;
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
