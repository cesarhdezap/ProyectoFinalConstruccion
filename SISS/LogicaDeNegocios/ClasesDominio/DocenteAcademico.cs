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
		private string Carrera { get; set; }
		private string Contraseña { get; set; }
		private int Cubiculo { get; set; }
		private bool esActivo { get; set; }
		private DocenteAcademico Coordinador { get; set; }
		private ERol Rol { get; set; }
        

	}
	
	
	public enum ERol
	{
		TecnicoAcademico,
		Coordinador
	}
}
