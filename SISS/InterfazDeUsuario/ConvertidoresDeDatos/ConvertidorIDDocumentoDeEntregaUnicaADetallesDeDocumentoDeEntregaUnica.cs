using System;
using System.Globalization;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

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
