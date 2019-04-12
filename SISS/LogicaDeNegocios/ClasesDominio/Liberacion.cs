using System;

namespace LogicaDeNegocios
{
	public class Liberacion
	{
		public DateTime Fecha { get; set; }
		public Asignacion Asignacion { get; set; }
		public DocumentoDeEntregaUnica CartaDeLiberacion { get; set; }

	}
}
