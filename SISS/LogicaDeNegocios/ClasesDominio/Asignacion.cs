using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
	public class Asignacion
	{
		private const int HORAS_MAXIMAS_CUBIERTAS = 480;
		private int IDAsignacion { get; set; }
		private EstadoAsignacion EstadoAsignacion { get; set; }
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
			throw new NotImplementedException();
		}

		public void RegistrarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica, DocenteAcademico docenteAcademico)
		{
			//TODO
			throw new NotImplementedException();
		}

		private void ActualizarHorasCubiertas()
		{
			//TODO
			throw new NotImplementedException();
		}

		public void Liberar()
		{
			//TODO
			throw new NotImplementedException();
		}

		public bool EsLiberable()
		{
			//TODO
			throw new NotImplementedException();
		}


	}


	public enum EstadoAsignacion
	{
		Activo,
		Liberado,
		Inactivo
	}
}
