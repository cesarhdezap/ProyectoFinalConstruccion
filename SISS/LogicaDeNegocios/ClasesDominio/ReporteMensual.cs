using System;

namespace LogicaDeNegocios
{
	public class ReporteMensual
	{
		public DateTime FechaDeEntrega { get; set; }
		public int HorasReportadas { get; set; }
		public int IDReporte { get; set; }
		public int NumeroDeReporte { get; set; }
		public DocenteAcademico DocenteAcademico { get; set; }
		public EMes Mes { get; set; }
	}

	public enum EMes
	{
		Enero,
		Febrero,
		Marzo,
		Abril, 
		Mayo, 
		Junio, 
		Julio, 
		Agosto, 
		Septiembre, 
		Octubre,
		Noviembre, 
		Diciembre
	}
}
