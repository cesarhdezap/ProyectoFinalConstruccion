using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class ReporteMensualDAO : Interfaces.IReporteMensualDAO
	{
        public void ActualizarReporteMensualPorID(int IDReporteMensual, ReporteMensual reporteMensual)
		{
			//TODO
			throw new NotImplementedException();
		}

        public List<ReporteMensual> CargarIDsPorIDAsignacion(int IDAsignacion)
		{
			//TODO
			throw new NotImplementedException();
		}

        public ReporteMensual CargarReporteMensualPorID(int IDReporteMensual)
		{
			//TODO
			throw new NotImplementedException();
		}

        private List<ReporteMensual> ConvertirDataTableAListaDeReportesMensuales (DataTable tablaDeReportesMensuales)
		{
			//TODO
			throw new NotImplementedException();
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

        public void GuardarReporteMensual(ReporteMensual reporteMensual)
		{
			//TODO
			throw new NotImplementedException();
		}

        private SqlParameter[] InicializarParametrosDeSql(ReporteMensual reporteMensual)
        {
            SqlParameter[] parametrosDeReporteMensual = new SqlParameter[6];

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

            return parametrosDeReporteMensual;
        }
	}
}
