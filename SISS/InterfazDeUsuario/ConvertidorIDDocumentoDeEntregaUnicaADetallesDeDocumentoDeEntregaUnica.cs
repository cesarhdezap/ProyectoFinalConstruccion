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
    public class ConvertidorIDDocumentoDeEntregaUnicaADetallesDeDocumentoDeEntregaUnica : IValueConverter
    {
        public object Convert(object IDDocumento, Type targetType, object parameter, CultureInfo culture)
        {
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            DocumentoDeEntregaUnica documentoDeEntregaUnica = new DocumentoDeEntregaUnica();
            documentoDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarDocumentoDeEntregaUnicaPorID((int)IDDocumento);

            string cadenaResultado = documentoDeEntregaUnica.Nombre + System.Environment.NewLine +
                                     "Entregado: " + documentoDeEntregaUnica.FechaDeEntrega.ToString();

            return cadenaResultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
