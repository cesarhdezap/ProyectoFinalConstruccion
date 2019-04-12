using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
	class Solicitud
	{
		public DateTime Fecha { get; set; }
		public List<Proyecto> Proyecto { get; set; }
	}
}
