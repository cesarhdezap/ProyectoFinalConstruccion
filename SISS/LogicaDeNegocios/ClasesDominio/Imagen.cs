using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios.ClasesDominio
{
    public class Imagen
    {
        public string DireccionDeImagen { get; set; }
        public int IDDocumento { get; set; }
        public TipoDeDocumentoEnImagen TipoDeDocumentoEnImagen { get; set; }

		public Imagen(TipoDeDocumentoEnImagen tipoDeDocumentoEnImagen)
		{
			this.DireccionDeImagen = string.Empty;
			this.TipoDeDocumentoEnImagen = tipoDeDocumentoEnImagen;
		}

		public void Guardar()
		{
			ImagenDAO imagenDAO = new ImagenDAO();
			imagenDAO.GuardarImagen(this);
		}

		public void Actualizar()
		{
			ImagenDAO imagenDAO = new ImagenDAO();
			imagenDAO.ActualizarImagenPorIDDocumentno(this);
		}
	}

    public enum TipoDeDocumentoEnImagen
    {
        DocumentoDeEntregaUnica,
        ReporteMensual
    }
}
