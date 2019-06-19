using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace InterfazDeUsuario
{
    public class ConvertidorIDDocumentoDeEntregaUnicaADetallesDeDocumentoDeEntregaUnica : IValueConverter
    {
        public object Convert(object IDDocumento, Type targetType, object parameter, CultureInfo culture)
        {
            DocumentoDeEntregaUnicaDAO documentoDeEntregaUnicaDAO = new DocumentoDeEntregaUnicaDAO();
            DocumentoDeEntregaUnica documentoDeEntregaUnica = new DocumentoDeEntregaUnica();
			string cadenaResultado = string.Empty;

			try
			{
				documentoDeEntregaUnica = documentoDeEntregaUnicaDAO.CargarDocumentoDeEntregaUnicaPorID((int)IDDocumento);
				cadenaResultado = documentoDeEntregaUnica.TipoDeDocumento.ToString() + System.Environment.NewLine +
                                     "- Entregado: " + documentoDeEntregaUnica.FechaDeEntrega.ToString();
			}
			catch (AccesoADatosException e)
			{
				MensajeDeErrorParaMessageBox mensaje;
				mensaje = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(e);
				cadenaResultado = mensaje.Mensaje;
			}

            return cadenaResultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
