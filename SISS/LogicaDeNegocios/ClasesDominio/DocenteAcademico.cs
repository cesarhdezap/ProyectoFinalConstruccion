using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
	public class DocenteAcademico : Persona
	{
		private int IDPersonal { get; set; }
		private string carrera { get; set; }
		private string contraseña { get; set; }
		private int cubiculo { get; set; }
		private bool esActivo { get; set; }
		private DocenteAcademico coordinador { get; set; }
		private Erol rol { get; set; }
        

	}
	
	
	public enum Erol
	{
		TecnicoAcademico,
		Coordinador
	}
}
