using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Windows.Media.Imaging;

namespace LogicaDeNegocios
{
	public class DocumentoDeEntregaUnica
	{
		public int IDDocumento { get; set; }
		public DateTime FechaDeEntrega { get; set; }
		public TipoDeDocumento TipoDeDocumento { get; set; }
		public DocenteAcademico DocenteAcademico { get; set; }
		public BitmapImage Imagen { get; set; }
	}

	

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
