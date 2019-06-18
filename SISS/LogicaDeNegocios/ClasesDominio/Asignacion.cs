using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
    /// <summary>
    /// Clase Asignacion.
    /// Contiene todos los métodos para realizar operaciones de la clase y
    /// los objetos que contiene con la base de datos.
    /// </summary>
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

        /// <summary>
        /// Inicializa las listas y objetos atributos de la clase y las fechas en <see cref="DateTime.MinValue"/>.
        /// </summary>
		public Asignacion()
		{
			DocumentosDeEntregaUnica = new List<DocumentoDeEntregaUnica>();
			ReportesMensuales = new List<ReporteMensual>();
            Solicitud = new Solicitud();
            Liberacion = new Liberacion();
            FechaDeFinal = DateTime.MinValue;
            FechaDeInicio = DateTime.MinValue;
		}

        /// <summary>
        /// Convierte los atributos del <see cref="Asignacion"/>
        /// a una cadena con espacios para debugging.
        /// </summary>
        /// <returns>Cadena con los datos de los atributos.</returns>
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

        /// <summary>
        /// Carga el <see cref="Proyecto"/> vinculado a la <see cref="Asignacion"/>
        /// por el atributo IDAsignacion.
        /// </summary>
        /// <returns>El proyecto de la Asignacion</returns>
		public Proyecto CargarProyecto()
		{
			ProyectoDAO proyectoDAO = new ProyectoDAO();
			Proyecto proyecto = new Proyecto();
			proyecto = proyectoDAO.CargarIDPorIDAsignacion(IDAsignacion);
			proyecto = proyectoDAO.CargarProyectoPorID(proyecto.IDProyecto);
			return proyecto;
		}

        /// <summary>
        /// Carga los documentos de la <see cref="Asignacion"/> por
        /// el atributo IDAsignacion y los guarda en las listas atributo
        /// <see cref="Asignacion.ReportesMensuales"/> y <see cref="Asignacion.DocumentosDeEntregaUnica"/>.
        /// </summary>
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

        /// <summary>
        /// Carga todos los <see cref="ReporteMensual"/> de la Asignacion, calcula
        /// las horas cubiertas y regresa <see cref="HorasCubiertas"/>.
        /// </summary>
        /// <returns>Horas totales de todos los <see cref="ReportesMensuales"/> de la Asignacion.</returns>
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

        /// <summary>
        /// Registra un <see cref="ReporteMensual"/> en la base de datos
        /// según la IDAsignacion.
        /// </summary>
        /// <param name="reporteMensual">Reporte mensual a registrar en la <see cref="Asignacion"/>.</param>
        public void RegistrarReporteMensual(ReporteMensual reporteMensual)
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            reporteMensualDAO.GuardarReporteMensual(reporteMensual, IDAsignacion);
            reporteMensual.IDDocumento = reporteMensualDAO.ObtenerUltimoIDInsertado();
            ReportesMensuales.Add(reporteMensual);
        }

        /// <summary>
        /// Registra un <see cref="DocumentosDeEntregaUnica"/> en la base de datos
        /// según la IDAsignacion.
        /// </summary>
        /// <param name="documentoDeEntregaUnica">Documento de entrega única a registrar en la <see cref="Asignacion"/></param>
        public void RegistrarDocumentoDeEntregaUnica(DocumentoDeEntregaUnica documentoDeEntregaUnica)
        {
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            documentoDeEntregaUnicaDAO.GuardarDocumentoDeEntregaUnica(documentoDeEntregaUnica, IDAsignacion);
            documentoDeEntregaUnica.IDDocumento = documentoDeEntregaUnicaDAO.ObtenerUltimoIDInsertado();
            DocumentosDeEntregaUnica.Add(documentoDeEntregaUnica);
        }

        /// <summary>
        /// Guarda la <see cref="Asignacion"/> en la base de datos.
        /// </summary>
        /// <exception cref="Excepciones.AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
		public void Guardar()
		{
			AsignacionDAO asignacionDAO = new AsignacionDAO();
			asignacionDAO.GuardarAsignacion(this);
			Alumno.Asignar();
		}

        /// <summary>
        /// Suma las horas cubiertas según <see cref="ReporteMensual.HorasReportadas"/>
        /// en <see cref="Asignacion.ReportesMensuales"/> y las guarda en <see cref="HorasCubiertas"/>.
        /// </summary>
		private void ActualizarHorasCubiertas()
        {
            int horasCubiertas = 0;

            foreach (ReporteMensual reporteMensual in ReportesMensuales)
            {
                horasCubiertas = horasCubiertas + reporteMensual.HorasReportadas;
            }

            HorasCubiertas = horasCubiertas;
		}

        /// <summary>
        /// Guarda la <see cref="Liberacion"/> en la base de datos y actualiza
        /// el atributo <see cref="EsLiberable"/>.
        /// </summary>
        /// <param name="cartaDeLiberacion">Carta de liberacion de la Asignacion.</param>
        /// <returns>Si se guardo la Liberacion.</returns>
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

        /// <summary>
        /// Valida si la <see cref="Asignacion"/> tiene las horas necesarias
        /// para ser liberada según <see cref="HORAS_MAXIMAS_CUBIERTAS"/>.
        /// </summary>
        /// <returns>Si se puede registrar una Liberacion en la Asignacion.</returns>
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

    /// <summary>
    /// Enumerador con los estados en los que puede estar una <see cref="Asignacion"/>.
    /// </summary>
	public enum EstadoAsignacion
	{
		Activo,
		Liberado,
		Inactivo
	}
}
