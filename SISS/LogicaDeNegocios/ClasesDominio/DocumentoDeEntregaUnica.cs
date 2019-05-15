using System;

namespace LogicaDeNegocios
{
	public class DocumentoDeEntregaUnica
	{
		public int IDDocumento { get; set; }
		public string RutaDeArchivo { get; set; }
		public DateTime FechaDeEntrega { get; set; }
		public TipoDeDocumento TipoDeDocumento { get; set; }
		public string Nombre { get; set; }
		public DocenteAcademico DocenteAdminsitrativo { get; set; }
		
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
