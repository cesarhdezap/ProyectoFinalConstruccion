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
            Solicitud = new Solicitud();
            Liberacion = new Liberacion();
            FechaDeFinal = DateTime.MinValue;
            FechaDeInicio = DateTime.MinValue;
		}

        public override string ToString()
        {
            string asignacion = System.Environment.NewLine +
                                "IDAsignacion: " + IDAsignacion + System.Environment.NewLine +
                                "Estado: " + EstadoAsignacion.ToString() + System.Environment.NewLine +
                                "FechaDeInicio: " + FechaDeInicio.ToString() + System.Environment.NewLine +
                                "FechaDeFinal: " + FechaDeFinal.ToString() + System.Environment.NewLine +
                                "HorasCubiertas: " + HorasCubiertas + System.Environment.NewLine;
            return asignacion;
        }

		public Proyecto CargarProyecto()
		{
			ProyectoDAO proyectoDAO = new ProyectoDAO();
			Proyecto proyecto = new Proyecto();
			proyecto = proyectoDAO.CargarIDProyectoPorIDAsignacion(IDAsignacion);
			proyecto = proyectoDAO.CargarProyectoPorID(proyecto.IDProyecto);
			return proyecto;
		}

		public void CargarDocumentos()
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            ReportesMensuales = reporteMensualDAO.CargarIDsPorIDAsignacion(IDAsignacion);
            DocumentosDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarIDsPorIDAsignacion(IDAsignacion);
            for (int i = 0; i< DocumentosDeEntregaUnica.Count; i++)
            {
                DocumentosDeEntregaUnica[i] = documentoDeEntregaUnicaDAO.CargarDocumentoDeEntregaUnicaPorID(DocumentosDeEntregaUnica[i].IDDocumento);
            }

            for (int i = 0; i < ReportesMensuales.Count; i++)
            {
                ReportesMensuales[i] = reporteMensualDAO.CargarReporteMensualPorID(ReportesMensuales[i].IDDocumento);
            }
        }

        public int ObtenerHorasCubiertas()
        {
			
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            for (int i = 0; i < ReportesMensuales.Count; i++)
            {
                ReportesMensuales[i] = reporteMensualDAO.CargarReporteMensualPorID(ReportesMensuales[i].IDDocumento);
            }
            ActualizarHorasCubiertas();
            return HorasCubiertas;
        }

        public void RegistrarReporteMensual(ReporteMensual reporteMensual)
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            reporteMensualDAO.GuardarReporteMensual(reporteMensual, IDAsignacion);
            reporteMensual.IDDocumento = reporteMensualDAO.ObtenerUltimoIDInsertado();
            ReportesMensuales.Add(reporteMensual);
        }

        public void RegistrarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica)
        {
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            documentoDeEntregaUnicaDAO.GuardarDocumentoDeEntregaUnica(documentoDeEntregaUnica, IDAsignacion);
            documentoDeEntregaUnica.IDDocumento = documentoDeEntregaUnicaDAO.ObtenerUltimoIDInsertado();
            DocumentosDeEntregaUnica.Add(documentoDeEntregaUnica);
        }


		public void Guardar()
		{
			AsignacionDAO asignacionDAO = new AsignacionDAO();
			asignacionDAO.GuardarAsignacion(this);
			Alumno.Asignar();
		}

		private void ActualizarHorasCubiertas()
        {
            int horasCubiertas = 0;

            foreach (ReporteMensual reporteMensual in ReportesMensuales)
            {
                horasCubiertas = horasCubiertas + reporteMensual.HorasReportadas;
            }

            HorasCubiertas = horasCubiertas;
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
                Liberacion = liberacion;
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
