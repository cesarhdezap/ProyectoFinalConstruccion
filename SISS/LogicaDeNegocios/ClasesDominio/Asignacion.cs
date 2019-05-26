using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
	public class Asignacion
	{
		private const int HORAS_MAXIMAS_CUBIERTAS = 480;
		public int IDAsignacion { get; set; }
        public EstadoAsignacion EstadoAsignacion { get; set; }
        public DateTime FechaDeInicio { get; set; }
        public DateTime FechaDeFinal { get; set; }
        public int HorasCubiertas { get; set; }
        public Alumno Alumno { get; set; }
        public Proyecto Proyecto { get; set; }
		public List<DocumentoDeEntregaUnica> DocumentosDeEntregaUnica { get; set; }
		public List<ReporteMensual> ReportesMensuales { get; set; }
		public Liberacion Liberacion { get; set; }
		public Solicitud Solicitud { get; set; }
		
		
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
