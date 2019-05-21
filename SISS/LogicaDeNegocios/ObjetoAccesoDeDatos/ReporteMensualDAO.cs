using System;
using System.Collections.Generic;
using System.Data;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	class ReporteMensualDAO : Interfaces.IReporteMensualDAO
	{
        public void ActualizarReporteMensualPorID(int IDReporteMensual, ReporteMensual reporteMensual)
		{
			//TODO
			throw new NotImplementedException();
		}

        public List<ReporteMensual> CargarIDsPorIDAsignacion(int IDasignacion)
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

        private ReporteMensual ConvertirDataTableAReporteMensual (DataTable TablaDeReportesMensuales)
		{
			//TODO
			throw new NotImplementedException();
		}

        private DataTable ConvertirReporteMensualADataTable (ReporteMensual reporteMensual)
		{
			//TODO
			throw new NotImplementedException();
		}

        public int GuardarReporteMensual(ReporteMensual reporteMensual)
		{
			//TODO
			throw new NotImplementedException();
		}

	}
}
