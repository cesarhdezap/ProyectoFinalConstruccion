using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class DocumentoDeEntregaUnica
	{
		private int IDDocumento { get; set; }
		private string RutaDeArchivo { get; set; }
		private DateTime FechaDeEntrega { get; set; }
		private EtipoDeDocumento TipoDeDocumento { get; set; }
		private string Nombre { get; set; }
		private DocenteAcademico DocenteAdminsitrativo { get; set; }
		
	}

	public enum EtipoDeDocumento
	{
		CartaDeSolicitud,
		CartaDeAutorizacion,
		CartaDeAceptacion,
		FormatoDeRegistroYPlanDeActividades,
		Memoria,
		CartaDeLiberacion,
	}
}
