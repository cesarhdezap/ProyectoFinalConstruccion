using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Liberacion
	{
		private DateTime Fecha { get; set; }
		private Asignacion Asignacion { get; set; }
		private DocumentoDeEntregaUnica CartaDeLiberacion { get; set; }

	}
}
