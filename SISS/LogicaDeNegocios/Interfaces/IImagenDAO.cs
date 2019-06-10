using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using LogicaDeNegocios.ClasesDominio;

namespace LogicaDeNegocios.Interfaces
{
    interface IImagenDAO
    {
        void GuardarImagen(Imagen imagen);
        BitmapImage CargarImagenPorIDDocumentoYTipoDeDocumentoEnImagen(int IDDocumento, TipoDeDocumentoEnImagen tipoDeDocumentoEnImagen);
        void ActualizarImagenPorIDDocumentno(Imagen imagen);
    }
}
