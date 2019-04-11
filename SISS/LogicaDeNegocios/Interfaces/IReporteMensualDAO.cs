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
        int GuardarReporteMensual(ReporteMensual reporteMensual);
        ReporteMensual CargarReporteMensualPorID(int IDReporteMensual);
        DataTable ReporteMensualADataTable(ReporteMensual reporteMensual);

	}
}
