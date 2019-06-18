using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios.ClasesDominio
{
    /// <summary>
    /// Clase <see cref="Imagen"/>. 
    /// <para>Contiene los métodos para guardar y actualizar una <see cref="Imagen"/>
    /// en la base de datos.</para>
    /// </summary>
    public class Imagen
    {
        public string DireccionDeImagen { get; set; }
        public int IDDocumento { get; set; }
        public TipoDeDocumentoEnImagen TipoDeDocumentoEnImagen { get; set; }

        /// <summary>
        /// Inicializa la imagen con el atributo <see cref="TipoDeDocumentoEnImagen"/>
        /// de <paramref name="tipoDeDocumentoEnImagen"/>.
        /// </summary>
        /// <param name="tipoDeDocumentoEnImagen">Enumerador de
        /// <see cref="TipoDeDocumento"/>.</param>
		public Imagen(TipoDeDocumentoEnImagen tipoDeDocumentoEnImagen)
		{
            DireccionDeImagen = string.Empty;
            TipoDeDocumentoEnImagen = tipoDeDocumentoEnImagen;
		}

        /// <summary>
        /// Guarda la <see cref="Imagen"/> en la base de datos.
        /// </summary>
		public void Guardar()
		{
			ImagenDAO imagenDAO = new ImagenDAO();
			imagenDAO.GuardarImagen(this);
		}

        /// <summary>
        /// 
        /// </summary>
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
