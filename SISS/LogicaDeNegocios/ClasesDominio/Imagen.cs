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

		public void Guardar()
		{
			ImagenDAO imagenDAO = new ImagenDAO();
			imagenDAO.GuardarImagen(this);
		}
    }

    public enum TipoDeDocumentoEnImagen
    {
        DocumentoDeEntregaUnica,
        ReporteMensual
    }
}
