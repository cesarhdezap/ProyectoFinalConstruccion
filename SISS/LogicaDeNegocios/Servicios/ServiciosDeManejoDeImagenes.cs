using System;
using System.Windows.Media.Imaging;
using System.IO;

namespace LogicaDeNegocios.Servicios
{
    /// <summary>
    /// Clase de utilidad para procesar imagenes.
    /// Contiene la conversión de una dirección y un arreglo de Bytes a BitmapImage
    /// y la conversión de BitMapImage a arreglo de Bytes
    /// </summary>
    public static class ServiciosDeManejoDeImagenes
    {
        /// <summary>
        /// Convierte <paramref name="datosDeImagen"/> con una imagen a un objeto BitMapImage.
        /// </summary>
        /// <param name="datosDeImagen">Datos de la imagen en arreglo de Byte.</param>
        /// <returns>BitmapImage con los datos de <paramref name="datosDeImagen"/></returns>
        public static BitmapImage ConvertirArregloDeBytesAImagen(Byte[] datosDeImagen)
        {
            using (MemoryStream memoryStream = new MemoryStream(datosDeImagen))
            {
                BitmapImage imagen = new BitmapImage();
                imagen.BeginInit();
                imagen.CacheOption = BitmapCacheOption.OnLoad;
                imagen.StreamSource = memoryStream;
                imagen.EndInit();
                return imagen;
            }
        }

        /// <summary>
        /// Convierte <paramref name="imagen"/> a un arreglo de Byte.
        /// </summary>
        /// <param name="imagen">BitmapImage con datos de una imagen.</param>
        /// <returns>Arreglo de Byte con los datos de <paramref name="imagen"/></returns>
        public static byte[] ConvertirImagenAArregloDeBytes(BitmapImage imagen)
        {
            byte[] datosDeimagen;
            JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
            jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(imagen));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                jpegBitmapEncoder.Save(memoryStream);
                datosDeimagen = memoryStream.ToArray();
            }
            return datosDeimagen;
        }

        /// <summary>
        /// Convierte <paramref name="direccion"/> a un objeto BitmapImage.
        /// </summary>
        /// <param name="direccion">Dirreción de los datos de una imagen local.</param>
        /// <returns>BitmapImage con los datos de <paramref name="direccion"/></returns>
        public static BitmapImage CargarImagenPorDireccion(string direccion)
        {
            BitmapImage imagen = new BitmapImage();
            try
            {
				imagen.BeginInit();
                imagen.UriSource = new Uri(direccion);
				imagen.EndInit();
            }
            catch (UriFormatException e)
            {
                throw new FormatException("La direccion: " + direccion + " no es una direccion de imagen valida.", e);
            }

            return imagen;
        }   
    }
}
