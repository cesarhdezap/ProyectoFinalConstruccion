using System.Windows.Media.Imaging;
using LogicaDeNegocios.ClasesDominio;

namespace LogicaDeNegocios.Interfaces
{
	interface IImagenDAO
	{
		void ActualizarImagenPorIDDocumentno(Imagen imagen);
		BitmapImage CargarImagenPorIDDocumentoYTipoDeDocumentoEnImagen(int IDDocumento, TipoDeDocumentoEnImagen tipoDeDocumentoEnImagen);
		void GuardarImagen(Imagen imagen);
	}
}
