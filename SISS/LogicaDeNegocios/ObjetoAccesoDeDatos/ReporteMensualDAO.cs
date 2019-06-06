using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using AccesoABaseDeDatos;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos

{
	class ReporteMensualDAO : IReporteMensualDAO
	{

        public List<ReporteMensual> CargarIDsPorIDAsignacion(int IDAsignacion)
		{
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al cargar IDsReporteMensual Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.");
            }
            DataTable tablaDeReportesMensuales = new DataTable();
            SqlParameter[] parametroIDAsignacion = new SqlParameter[1];
            parametroIDAsignacion[0] = new SqlParameter
            {
                ParameterName = "@IDAsignacion",
                Value = IDAsignacion
            };

            try
            {
                tablaDeReportesMensuales = AccesoADatos.EjecutarSelect("SELECT IDDocumento FROM ReportesMensuales WHERE IDAsignacion = @IDasignacion", parametroIDAsignacion);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar IDsReporteMensual por IDAsignacion: " + IDAsignacion, e);
            }
            List<ReporteMensual> listaDeReportesMensuales = new List<ReporteMensual>();
            try
            {
                listaDeReportesMensuales = ConvertirDataTableAListaDeReportesMensualesConSoloID(tablaDeReportesMensuales);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a ListaDeReportesMensuales en cargar IDs con IDAsignacion: " + IDAsignacion, e);
            }
            return listaDeReportesMensuales;
        }

        public ReporteMensual CargarReporteMensualPorID(int IDDocumento)
		{
            if (IDDocumento <= 0)
            {
                throw new AccesoADatosException("Error al cargar ReporteMensual Por IDDocumento: " + IDDocumento + ". IDDocumento no es valido.");
            }
            DataTable tablaDeReporteMensual = new DataTable();
            SqlParameter[] parametroIDDocumento = new SqlParameter[1];
            parametroIDDocumento[0] = new SqlParameter
            {
                ParameterName = "@IDDocumento",
                Value = IDDocumento
            };

            try
            {
                tablaDeReporteMensual = AccesoADatos.EjecutarSelect("SELECT * FROM ReportesMensuales WHERE IDDocumento = @IDDocumento", parametroIDDocumento);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al cargar ReporteMensual con IDDocumento: " + IDDocumento, e);
            }
            ReporteMensual reporteMensual = new ReporteMensual();
            try
            {
                reporteMensual = ConvertirDataTableAReporteMensual(tablaDeReporteMensual);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir DocumentoDeEntregaUnica al en cargar DocumentoDeEntregaUnica con IDDocumento: " + IDDocumento, e);
            }
            return reporteMensual;
        }

        private List<ReporteMensual> ConvertirDataTableAListaDeReportesMensuales (DataTable tablaDeReportesMensuales)
		{
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            List<ReporteMensual> listaDeReportesMensuales = new List<ReporteMensual>();
            foreach (DataRow fila in tablaDeReportesMensuales.Rows)
            {
                ReporteMensual reporteMensual = new ReporteMensual
                {
                    IDDocumento = (int)fila["IDDocumento"],
                    HorasReportadas = (int)fila["HorasReportadas"],
                    FechaDeEntrega = DateTime.Parse(fila["FechaDeEntrega"].ToString()),
                    NumeroDeReporte = (int)fila["NumeroDeReporte"],
                    DocenteAcademico = docenteAcademicoDAO.CargarIDPorIDDocumento((int)fila["IDDocumento"]),
                    Mes = (Mes)fila["Mes"]
                };
            }
            return listaDeReportesMensuales;
        }

        private List<ReporteMensual> ConvertirDataTableAListaDeReportesMensualesConSoloID(DataTable tablaDeReportesMensuales)
        {
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            List<ReporteMensual> listaDeReportesMensuales = new List<ReporteMensual>();
            foreach (DataRow fila in tablaDeReportesMensuales.Rows)
            {
                ReporteMensual reporteMensual = new ReporteMensual
                {
                    IDDocumento = (int)fila["IDDocumento"],
                };
            }
            return listaDeReportesMensuales;
        }

        private ReporteMensual ConvertirDataTableAReporteMensual (DataTable tablaDeReportesMensuales)
		{
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            ReporteMensual reporteMensual = new ReporteMensual();
            foreach (DataRow fila in tablaDeReportesMensuales.Rows)
            {
                reporteMensual.IDDocumento = (int)fila["IDDocumento"];
                reporteMensual.HorasReportadas = (int)fila["HorasReportadas"];
                reporteMensual.FechaDeEntrega = DateTime.Parse(fila["FechaDeEntrega"].ToString());
                reporteMensual.NumeroDeReporte = (int)fila["NumeroDeReporte"];
                reporteMensual.DocenteAcademico = docenteAcademicoDAO.CargarIDPorIDDocumento((int)fila["IDDocumento"]);
                reporteMensual.Mes = (Mes)fila["Mes"];
            }
            return reporteMensual;
		}

        public void GuardarReporteMensual(ReporteMensual reporteMensual, int IDAsignacion)
        {
            if (IDAsignacion <= 0)
            {
                throw new AccesoADatosException("Error al guardar ReporteMensual Por IDAsignacion: " + IDAsignacion + ". IDAsignacion no es valido.");
            }
            SqlParameter[] parametrosDocumentoDeEntregaUnica = InicializarParametrosDeSql(reporteMensual, IDAsignacion);

            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO ReportesMensuales(FechaDeEntrega, HorasReportadas, FechaDeEntrega, NumeroDeReporte, DocenteAcademico, Mes, IDAsignacion) VALUES(@IDDocumento, @HorasReportadas, @FechaDeEntrega, @NumeroDeReporte, @DocenteAcademico, @Mes, @IDAsignacion)", parametrosDocumentoDeEntregaUnica);
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar ReporteMensual: " + reporteMensual.ToString(), e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("El ReporteMensual: " + reporteMensual.ToString() + " no fue guardado.");
            }
        }

        private SqlParameter[] InicializarParametrosDeSql(ReporteMensual reporteMensual, int IDAsignacion)
        {
            SqlParameter[] parametrosDeReporteMensual = new SqlParameter[7];

            for (int i = 0; i < parametrosDeReporteMensual.Length; i++)
            {
                parametrosDeReporteMensual[i] = new SqlParameter();
            }

            parametrosDeReporteMensual[0].ParameterName = "@IDReporte";
            parametrosDeReporteMensual[0].Value = reporteMensual.IDDocumento;
            parametrosDeReporteMensual[1].ParameterName = "@HorasReportadas";
            parametrosDeReporteMensual[1].Value = reporteMensual.HorasReportadas;
            parametrosDeReporteMensual[2].ParameterName = "@FechaDeEntrega";
            parametrosDeReporteMensual[2].Value = reporteMensual.FechaDeEntrega.ToString();
            parametrosDeReporteMensual[3].ParameterName = "@NumeroDeReporte";
            parametrosDeReporteMensual[3].Value = reporteMensual.NumeroDeReporte;
            parametrosDeReporteMensual[4].ParameterName = "@DocenteAcademico";
            parametrosDeReporteMensual[4].Value = reporteMensual.DocenteAcademico.IDPersonal;
            parametrosDeReporteMensual[5].ParameterName = "@Mes";
            parametrosDeReporteMensual[5].Value = (int)reporteMensual.Mes;
            parametrosDeReporteMensual[6].ParameterName = "@IDAsignacion";
            parametrosDeReporteMensual[6].Value = IDAsignacion;

            return parametrosDeReporteMensual;
        }

        public void ActualizarReporteMensualPorID(int IDReporteMensual, ReporteMensual reporteMensual)
        {
            throw new NotImplementedException();
        }

        public int ObtenerUltimoIDInsertado()
        {
            throw new NotImplementedException();
        }
    }
}
