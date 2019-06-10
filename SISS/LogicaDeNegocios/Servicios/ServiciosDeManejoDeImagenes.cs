using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace LogicaDeNegocios.Servicios
{
    public static class ServiciosDeManejoDeImagenes
    {

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
