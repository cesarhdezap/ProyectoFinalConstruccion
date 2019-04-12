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
		private int IDAsignacion { get; set; }
		private EEstadoAsignacion EstadoAsignacion { get; set; }
		private DateTime FechaDeInicio { get; set; }
		private DateTime FechaDeFinal { get; set; }
		private int HorasCubiertas { get; set; }
		private Alumno Alumno { get; set; }
		public Proyecto Proyecto { get; set; }
		private List<DocumentoDeEntregaUnica> DocumentosDeEntregaUnica { get; set; }
		private List<ReporteMensual> ReportesMensuales { get; set; }
		private Liberacion Liberacion { get; set; }
		private Solicitud Solicitud { get; set; }
		
		
		public void RegistrarReporteMensual(ReporteMensual reporteMensual, DocenteAcademico docenteAcademico)
		{
			//TODO
		}

		public void RegistrarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica, DocenteAcademico docenteAcademico)
		{
			//TODO
		}

		private void ActualizarHorasCubiertas()
		{
			//TODO
		}

		public void Liberar()
		{
			//TODO
		}

		public bool isLiberable()
		{
			//TODO
			return false;
		}


	}


	public enum EEstadoAsignacion
	{
		Activo,
		Liberado,
		Inactivo
	}
}
