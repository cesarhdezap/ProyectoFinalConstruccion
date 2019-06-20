using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IReporteMensualDAO
	{
        List<ReporteMensual> CargarIDsPorIDAsignacion(int IDAsignacion);
        ReporteMensual CargarReporteMensualPorID(int IDReporteMensual);
        void GuardarReporteMensual(ReporteMensual reporteMensual, int IDAsignacion);
		void ActualizarReporteMensualPorID(int IDReporteMensual, ReporteMensual reporteMensual);
        int ObtenerUltimoIDInsertado();
	}
}
