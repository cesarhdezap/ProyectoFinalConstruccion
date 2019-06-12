using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace InterfazDeUsuario
{
    class ConvertidorIDDocumentoReporteMensualADetallesDeReporteMensual : IValueConverter
    {
        public object Convert(object IDDocumento, Type targetType, object parameter, CultureInfo culture)
        {
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            ReporteMensual reporteMensual = new ReporteMensual();
            reporteMensual = reporteMensualDAO.CargarReporteMensualPorID((int)IDDocumento);

			string cadenaResultado = "Mes: " + reporteMensual.Mes.ToString() + System.Environment.NewLine +
									 "Horas reportadas: " + reporteMensual.HorasReportadas + System.Environment.NewLine +
									 "Reporte Mensual: " + reporteMensual.NumeroDeReporte + System.Environment.NewLine +
									 "Entregado: " + reporteMensual.FechaDeEntrega.ToString();
            return cadenaResultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
