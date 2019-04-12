using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Alumno : Persona
	{
		private const int MAXIMO_DE_ASIGNACIONES = 2;

		public string Matricula { get; set; }
		public string Carrera { get; set; }
		public string Contraseña { get; set; }
		public EEstadoAlumno EstadoAlumno { get; set; }
		public List<Asignacion> Asignaciones { get; set; }	

		public void DarDeBaja()
		{
			EstadoAlumno = EEstadoAlumno.Desactivado;

		}

		public void AceptarAlumno()
		{
			EstadoAlumno = EEstadoAlumno.Aceptado;
		}

		public void RechazarAlumno()
		{
			EstadoAlumno = EEstadoAlumno.Rechazado;
		}

        public static explicit operator Alumno(DataRowCollection v)
        {
            throw new NotImplementedException();
        }
    }

	public enum EEstadoAlumno
	{
		EnEspera, 
		Liberado,
		Aceptado,
		Rechazado,
		Desactivado,
		EsperandoAsignacion
	}
}
