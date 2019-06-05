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

        public override string ToString()
        {
            string asignacion = System.Environment.NewLine +
                                "IDAsignacion: " + this.IDAsignacion + System.Environment.NewLine +
                                "Estado: " + this.EstadoAsignacion.ToString() + System.Environment.NewLine +
                                "FechaDeInicio: " + this.FechaDeInicio.ToString() + System.Environment.NewLine +
                                "FechaDeFinal: " + this.FechaDeFinal.ToString() + System.Environment.NewLine +
                                "HorasCubiertas: " + this.HorasCubiertas + System.Environment.NewLine;
            return asignacion;
        }

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
            int horasCubiertas = 0;

            foreach (ReporteMensual reporteMensual in this.ReportesMensuales)
            {
                horasCubiertas = horasCubiertas + reporteMensual.HorasReportadas;
            }

            this.HorasCubiertas = horasCubiertas;
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
