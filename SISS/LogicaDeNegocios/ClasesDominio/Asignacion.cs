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
		private EestadoAsignacion estadoAsignacion { get; set; }
		private DateTime fechaDeInicio { get; set; }
		private DateTime fechaDeFinal { get; set; }
		private int horasCubiertas { get; set; }
		private Alumno alumno { get; set; }
		public Proyecto proyecto { get; set; }
		private List<DocumentoDeEntregaUnica> documentosDeEntregaUnica { get; set; }
		private List<ReporteMensual> reportesMensuales { get; set; }
		private Liberacion liberacion { get; set; }
		private Solicitud solicitud { get; set; }
		
		
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


	public enum EestadoAsignacion
	{
		Activo,
		Liberado,
		Inactivo
	}
}
