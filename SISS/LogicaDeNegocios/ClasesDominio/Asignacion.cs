using LogicaDeNegocios.ObjetoAccesoDeDatos;
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


		public Asignacion()
		{
			DocumentosDeEntregaUnica = new List<DocumentoDeEntregaUnica>();
			ReportesMensuales = new List<ReporteMensual>();
			this.Solicitud = new Solicitud();
			this.Liberacion = new Liberacion();
			this.FechaDeFinal = DateTime.MinValue;
			this.FechaDeInicio = DateTime.MinValue;
		}

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

        public void CargarDocumentos()
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
			this.ReportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion(this.IDAsignacion);
			this.DocumentosDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarIDsPorIDAsignacion(this.IDAsignacion);
            for (int i = 0; i<this.DocumentosDeEntregaUnica.Count; i++)
            {
                this.DocumentosDeEntregaUnica[i] = documentoDeEntregaUnicaDAO.CargarDocumentoDeEntregaUnicaPorID(this.DocumentosDeEntregaUnica[i].IDDocumento);
            }
            for (int i = 0; i < this.ReportesMensuales.Count; i++)
            {
                this.ReportesMensuales[i] = reporteMensualDAO.CargarReporteMensualPorID(this.ReportesMensuales[i].IDDocumento);
            }
        }

        public int ObtenerHorasCubiertas()
        {
			
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            for (int i = 0; i < ReportesMensuales.Count; i++)
            {
                ReportesMensuales[i] = reporteMensualDAO.CargarReporteMensualPorID(ReportesMensuales[i].IDDocumento);
            }

            this.ActualizarHorasCubiertas();
            return this.HorasCubiertas;
        }

        public void RegistrarReporteMensual(ReporteMensual reporteMensual)
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            reporteMensualDAO.GuardarReporteMensual(reporteMensual, this.IDAsignacion);
            reporteMensual.IDDocumento = reporteMensualDAO.ObtenerUltimoIDInsertado();
            this.ReportesMensuales.Add(reporteMensual);
        }

        public void RegistrarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica)
        {
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            documentoDeEntregaUnicaDAO.GuardarDocumentoDeEntregaUnica(documentoDeEntregaUnica, this.IDAsignacion);
            documentoDeEntregaUnica.IDDocumento = documentoDeEntregaUnicaDAO.ObtenerUltimoIDInsertado();
            this.DocumentosDeEntregaUnica.Add(documentoDeEntregaUnica);
        }


		public void Guardar()
		{
			AsignacionDAO asignacionDAO = new AsignacionDAO();
			asignacionDAO.GuardarAsignacion(this);
			this.Alumno.Asignar();
		}

		public void ActualizarHorasCubiertas()
        {
            int horasCubiertas = 0;

            foreach (ReporteMensual reporteMensual in this.ReportesMensuales)
            {
                horasCubiertas = horasCubiertas + reporteMensual.HorasReportadas;
            }

            this.HorasCubiertas = horasCubiertas;
		}

        public bool Liberar(DocumentoDeEntregaUnica cartaDeLiberacion)
        {
            bool fueLiberado = false;
            Liberacion liberacion = new Liberacion();
            LiberacionDAO liberacionDAO = new LiberacionDAO();
            if (EsLiberable())
            {
                liberacion.Fecha = DateTime.Now;
                liberacion.Asignacion = this;
                liberacion.CartaDeLiberacion = cartaDeLiberacion;
                liberacionDAO.GuardarLiberacion(liberacion);
                liberacion.IDLiberacion = liberacionDAO.ObtenerUltimoIDInsertado();
                this.Liberacion = liberacion;
                fueLiberado = true;
            }
            return fueLiberado;
        }

        public bool EsLiberable()
		{
            bool esLiberable = false;
            ActualizarHorasCubiertas();

            if (HorasCubiertas >= HORAS_MAXIMAS_CUBIERTAS)
            {
                esLiberable = true;
            }

            return esLiberable;
        }
    }



	public enum EstadoAsignacion
	{
		Activo,
		Liberado,
		Inactivo
	}
}
