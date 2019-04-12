using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class Liberacion
	{
		private DateTime fecha { get; set; }
		private Asignacion asignacion { get; set; }
		private DocumentoDeEntregaUnica cartaDeLiberacion { get; set; }

	}
}
