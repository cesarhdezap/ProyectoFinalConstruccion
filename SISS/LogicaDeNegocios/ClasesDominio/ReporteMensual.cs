using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class ReporteMensual
	{
		private DateTime fechaDeEntrega { get; set; }
		private int horasReportadas { get; set; }
		private int IDReporte { get; set; }
		private int numeroDeReporte { get; set; }
		private DocenteAcademico docenteAdministrativo { get; set; }
	}
}
