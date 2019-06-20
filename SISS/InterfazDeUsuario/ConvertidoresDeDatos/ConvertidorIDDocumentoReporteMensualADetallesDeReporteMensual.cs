using System;
using System.Globalization;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario
{
	class ConvertidorIDDocumentoReporteMensualADetallesDeReporteMensual : IValueConverter
    {
        public object Convert(object IDDocumento, Type targetType, object parameter, CultureInfo culture)
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            ReporteMensual reporteMensual = new ReporteMensual();
			string cadenaResultado = string.Empty;

			try
			{
				reporteMensual = reporteMensualDAO.CargarReporteMensualPorID((int)IDDocumento);
				cadenaResultado = "- Mes: " + reporteMensual.Mes.ToString() + System.Environment.NewLine +
								  "- Horas reportadas: " + reporteMensual.HorasReportadas + System.Environment.NewLine +
							   	  "- Reporte Mensual: " + reporteMensual.NumeroDeReporte + System.Environment.NewLine +
								  "- Entregado: " + reporteMensual.FechaDeEntrega.ToString();
			}
			catch (AccesoADatosException e)
			{
				MostrarMessageBoxDeExcepcion(e);
			}

			return cadenaResultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
