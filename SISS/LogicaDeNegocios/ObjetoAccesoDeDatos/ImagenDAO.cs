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

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
    class ImagenDAO : IImagenDAO
    {
        public void ActualizarImagenPorIDDocumentno(Imagen imagen)
        {
            SqlParameter[] parametroIDDocumento = InicializarParametrosDeSql(imagen);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("UPDATE Imagenes SET DatosDeImagen = @DatosDeImagen WHERE IDDocumento = @IDDocuemnto AND TipoDeDocumentoEnImagen = @TipoDeDocumentoEnImagen");
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al acutalizar imagen por IDDocumento: " + imagen.IDDocumento, e);
            }

            if(filasAfectadas <= 0)
            {
                throw new AccesoADatosException("La imagen con IDDocumento: " + imagen.IDDocumento + " no existe.");
            }
        }

        public BitmapImage CargarImagenPorIDDocumento(int IDDocumento)
        {
            DataTable tablaDeImagen = new DataTable();
            SqlParameter[] parametroIDDocumento = new SqlParameter[1];
           
            parametroIDDocumento[0] = new SqlParameter
            {
                ParameterName = "@IDDocumento",
                Value = IDDocumento
            };

            try
            {
                tablaDeImagen = AccesoADatos.EjecutarSelect("SELECT DatosDeImagen FROM Imagenes WHERE IDDocumento = @IDDocumento", parametroIDDocumento);
            }
            catch(SqlException e)
            {
                throw new AccesoADatosException("Error al cargar Imagen por IDDocumento: " + IDDocumento, e);
            }
            BitmapImage imagen = new BitmapImage();
            try
            {
                imagen = ConvertirDataTableAImagen(tablaDeImagen);
            }
            catch(FormatException e)
            {
                throw new AccesoADatosException("Error al convertir datatable a Imagen en cargar Imagen por IDDOcumento: " + IDDocumento, e);
            }

            return imagen;
        }

        public void GuardarImagen(Imagen imagen)
        {
            SqlParameter[] parametroIDDocumento = InicializarParametrosDeSql(imagen);
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = AccesoADatos.EjecutarInsertInto("INSERT INTO Imagenes(IDDocumento, DatosDeImagen, TipoDeDocumentoEnImagen) VALUES(@IDDocumento, @DatosDeImagen, @TipoDeDocumentoEnImagen)");
            }
            catch (SqlException e)
            {
                throw new AccesoADatosException("Error al guardar imagen con IDDocumento: " + imagen.IDDocumento , e);
            }
            if (filasAfectadas <= 0)
            {
                throw new AccesoADatosException("Imagen con IDDocumento: " + imagen.IDDocumento + " no fue guardada.");
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
                ParameterName = "@TipoDeDocumento",
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
