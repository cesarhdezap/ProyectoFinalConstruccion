using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
	public class Solicitud
	{
		public DateTime Fecha { get; set; }
		public List<Proyecto> Proyecto { get; set; }
	}
}
