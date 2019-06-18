using System;
using System.Windows.Media.Imaging;

namespace LogicaDeNegocios
{
    /// <summary>
    /// Clase <see cref="DocumentoDeEntregaUnica"/>.
    /// </summary>
	public class DocumentoDeEntregaUnica
	{
		public int IDDocumento { get; set; }
		public DateTime FechaDeEntrega { get; set; }
		public TipoDeDocumento TipoDeDocumento { get; set; }
		public DocenteAcademico DocenteAcademico { get; set; }
		public BitmapImage Imagen { get; set; }
	}
    
    /// <summary>
    /// Enumerador con los tipos de documento de <see cref="DocumentoDeEntregaUnica"/>.
    /// </summary>
	public enum TipoDeDocumento
	{
		CartaDeSolicitud,
		CartaDeAutorizacion,
		CartaDeAceptacion,
		FormatoDeRegistroYPlanDeActividades,
		Memoria,
		CartaDeLiberacion,
	}
}
