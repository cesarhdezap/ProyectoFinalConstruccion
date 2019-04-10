using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Asignacion
	{
		private const int HORAS_MAXIMAS_CUBIERTAS = 480;

		private EestadoAsignacion estadoAsignacion { get; set; }
		private DateTime fechaDeInicio { get; set; }
		private DateTime fechaDeFinal { get; set; }
		private int IDAsignacion { get; set; }
		private int horasCubiertas { get; set; }
		private List<DocumentoDeEntregaUnica> documentosDeEntregaUnica { get; set; }
		private List<ReporteMensual> reportesMensuales { get; set; }
		private Itinerario itinerario { get; set; }
		private Liberacion liberacion { get; set; }
		private Solicitud solicitud { get; set; }
		private Alumno alumno { get; set; }
		public Proyecto proyecto { get; set; }

		public Asignacion(Alumno alumno, Proyecto proyecto)
		{
			estadoAsignacion = EestadoAsignacion.Activo;
			fechaDeInicio = DateTime.Now;
			horasCubiertas = ActualizarHorasCubiertas();
			documentosDeEntregaUnica = new List<DocumentoDeEntregaUnica>();
			reportesMensuales = new List<ReporteMensual>();
			this.alumno = alumno;
			this.proyecto = proyecto;
		}

		private int ActualizarHorasCubiertas()
		{
			//TODO
			return 0;
		}

		private void Liberar()
		{
			//TODO
		}
	}


	public enum EestadoAsignacion
	{
		Activo,
		Liberado,
		Inactivo
	}
}
