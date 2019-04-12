using System;

namespace LogicaDeNegocios
{
	public class DocumentoDeEntregaUnica
	{
		private int IDDocumento { get; set; }
		private string RutaDeArchivo { get; set; }
		private DateTime FechaDeEntrega { get; set; }
		private ETipoDeDocumento TipoDeDocumento { get; set; }
		private string Nombre { get; set; }
		private DocenteAcademico DocenteAdminsitrativo { get; set; }
		
	}

	public enum ETipoDeDocumento
	{
		CartaDeSolicitud,
		CartaDeAutorizacion,
		CartaDeAceptacion,
		FormatoDeRegistroYPlanDeActividades,
		Memoria,
		CartaDeLiberacion,
	}
}
