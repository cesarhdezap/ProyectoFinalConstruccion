using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace LogicaDeNegocios.ClasesDominio
{
    class Imagen
    {
        public string DireccionDeImagen { get; set; }
        public int IDDocumento { get; set; }
        public TipoDeDocumentoEnImagen TipoDeDocumentoEnImagen { get; set; }
    }

    public enum TipoDeDocumentoEnImagen
    {
        DocumentoDeEntregaUnica,
        ReporteMensual
    }
}
