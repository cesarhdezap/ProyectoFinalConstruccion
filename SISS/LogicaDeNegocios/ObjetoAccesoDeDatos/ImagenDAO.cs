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
    public class ImagenDAO : IImagenDAO
    {
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
