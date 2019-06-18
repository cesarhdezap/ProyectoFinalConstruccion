using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using LogicaDeNegocios.Servicios;
using LogicaDeNegocios.Interfaces;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ClasesDominio;
using AccesoABaseDeDatos;
using System.Data.SqlClient;
using System.Data;
using LogicaDeNegocios.Querys;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
	/// <summary>
	/// Clase de abstraccion para acceso a objetos Imagen en la base de datos.
	/// Contiene metodos para cargar, insertar y actualizar objetos Imagen.
	/// </summary>
	public class ImagenDAO : IImagenDAO
    {
		/// <summary>
		/// Actualiza una Imagen dada la ID del Documento relacionado a ella.
		/// </summary>
		/// <param name="imagen">La Imagen a actualizar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepcion si el cliente de SQL tiro una excepción.</exception>
		public void ActualizarImagenPorIDDocumentno(Imagen imagen)
        {
            SqlParameter[] parametrosDeImagen = InicializarParametrosDeSql(imagen);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeImagen.ACTUALIZAR_IMAGEN_POR_IDDOCUMENTO, parametrosDeImagen);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, imagen);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("La imagen con IDDocumento: " + imagen.IDDocumento + " no existe.", TipoDeErrorDeAccesoADatos.ObjetoNoExiste);
            }
        }

		/// <summary>
		/// Carga una Imagen dada la ID del Documento relacionado a ella y el tipo de documento contenido en la imagen.
		/// </summary>
		/// <param name="IDDocumento">La ID del Documento relcionado a la Imagen a cargar.</param>
		/// <param name="tipoDeDocumentoEnImagen">El tipo de documento contenido en la Imagen</param>
		/// <returns>La Imagen relacionada a la ID del Documento relacionado a ella.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		public BitmapImage CargarImagenPorIDDocumentoYTipoDeDocumentoEnImagen(int IDDocumento, TipoDeDocumentoEnImagen tipoDeDocumentoEnImagen)
        {
            if (IDDocumento <= 0)
            {
                throw new AccesoADatosException("Error al cargar Imagen Por IDDocumento: " + IDDocumento + ". IDDocumento no es valido.", TipoDeErrorDeAccesoADatos.IDInvalida);
            }
            DataTable tablaDeImagen = new DataTable();
            SqlParameter[] parametrosDeDocumento = new SqlParameter[2];
            parametrosDeDocumento[0] = new SqlParameter
            {
                ParameterName = "@IDDocumento",
                Value = IDDocumento
            };
            parametrosDeDocumento[1] = new SqlParameter
            {
                ParameterName = "@TipoDeDocumentoEnImagen",
                Value = (int)tipoDeDocumentoEnImagen
            };
            try
            {
                tablaDeImagen = AccesoADatos.EjecutarSelect(QuerysDeImagen.CARGAR_IMAGEN_POR_IDDOCUMENTO_Y_TIPO_DE_DOCUMENTO_EN_IMAGEN, parametrosDeDocumento);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, IDDocumento);
			}
			BitmapImage imagen = new BitmapImage();
            try
            {
                imagen = ConvertirDataTableAImagen(tablaDeImagen);
            }
            catch (FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Imagen en cargar Imagen por IDDOcumento: " + IDDocumento, e, TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto);
            }

            return imagen;
        }

		/// <summary>
		/// Guarda una Imagen en la base de datos.
		/// </summary>
		/// <param name="imagen">La Imagen a guardar.</param>
		/// <exception cref="AccesoADatosException">Tira esta excepción si el cliente de SQL tiro una excepción.</exception>
		public void GuardarImagen(Imagen imagen)
        {
            SqlParameter[] parametroIDDocumento = InicializarParametrosDeSql(imagen);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto(QuerysDeImagen.GUARDAR_IMAGEN, parametroIDDocumento);
            }
			catch (SqlException e)
			{
				EncadenadorDeExcepciones.EncadenarExcepcionDeSql(e, imagen);
			}
			if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Imagen con IDDocumento: " + imagen.IDDocumento + " no fue guardada.", TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto);
            }
        }

		/// <summary>
		/// Inicializa un arreglo de SqlParameter basado en una Imagen.
		/// </summary>
		/// <param name="imagen">La Imagen para inicializar los parametros.</param>
		/// <returns>Un arreglo de SqlParameter donde cada posición es uno de los atributos del Alumno.</returns>
		private static SqlParameter[] InicializarParametrosDeSql(Imagen imagen)
        {
            SqlParameter[] parametrosDeImagen = new SqlParameter[3];

            parametrosDeImagen[0] = new SqlParameter
            {
                ParameterName = "@IDDocumento",
                Value = imagen.IDDocumento
            };
            parametrosDeImagen[1] = new SqlParameter
            {
                ParameterName = "@DatosDeImagen",
                Value = ServiciosDeManejoDeImagenes.ConvertirImagenAArregloDeBytes(ServiciosDeManejoDeImagenes.CargarImagenPorDireccion(imagen.DireccionDeImagen))
            };
            parametrosDeImagen[2] = new SqlParameter
            {
                ParameterName = "@TipoDeDocumentoEnImagen",
                Value = (int)imagen.TipoDeDocumentoEnImagen
            };

            return parametrosDeImagen;
        }

		/// <summary>
		/// Convierte una DataTable a una Imagen.
		/// </summary>
		/// <param name="tablaDeImagen">La DataTable que contiene datos de la Imagen.</param>
		/// <returns>La Imagen contenida en la DataTable.</returns>
		/// <exception cref="FormatException">Tira esta excepción si hay algún error de casteo en la conversión.</exception>
		private BitmapImage ConvertirDataTableAImagen(DataTable tablaDeImagen)
        {
            BitmapImage imagen = new BitmapImage();
            foreach (DataRow fila in tablaDeImagen.Rows)
            {
                imagen = ServiciosDeManejoDeImagenes.ConvertirArregloDeBytesAImagen((byte[])fila["DatosDeImagen"]);
            }
            return imagen;
        }
    }
}
