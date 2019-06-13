using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
