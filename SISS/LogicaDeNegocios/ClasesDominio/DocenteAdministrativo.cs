using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	class DocenteAdministrativo : CuentaHabiente
	{
		private string carrera { get; set; }
		private int cubiculo { get; set; }
		private int IDPersonal { get; set; }
		private DocenteAdministrativo coordinador { get; set; }
		private Erol rol { get; set; }
        private bool esActivo { get; set; }
	}
	
	
	public enum Erol
	{
		TecnicoAcademico,
		Coordinador
	}
}
