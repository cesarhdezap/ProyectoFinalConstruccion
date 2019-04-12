using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class ReporteMensual
	{
		private DateTime FechaDeEntrega { get; set; }
		private int HorasReportadas { get; set; }
		private int IDReporte { get; set; }
		private int NumeroDeReporte { get; set; }
		private DocenteAcademico DocenteAdministrativo { get; set; }
	}
}
