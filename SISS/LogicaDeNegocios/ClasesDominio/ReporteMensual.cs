using System;
using System.Windows.Media.Imaging;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios
{
	public class ReporteMensual
	{
		public DateTime FechaDeEntrega { get; set; }
		public int HorasReportadas { get; set; }
		public int IDDocumento { get; set; }
		public int NumeroDeReporte { get; set; }
		public DocenteAcademico DocenteAcademico { get; set; }
        public BitmapImage Imagen { get; set; }
		public Mes Mes { get; set; }

		public void Actualizar()
		{
			ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
			reporteMensualDAO.ActualizarReporteMensualPorID(IDDocumento, this);
		}
	}

	public enum Mes
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
