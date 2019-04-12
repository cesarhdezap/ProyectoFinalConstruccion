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
		private string rutaDeArchivo { get; set; }
		private DateTime fechaDeEntrega { get; set; }
		private EtipoDeDocumento tipoDeDocumento { get; set; }
		private string nombre { get; set; }
		private DocenteAcademico docenteAdminsitrativo { get; set; }
		
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
