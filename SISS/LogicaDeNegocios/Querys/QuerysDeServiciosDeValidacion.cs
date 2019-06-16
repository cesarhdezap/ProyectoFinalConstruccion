using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeServiciosDeValidacion
	{
		public const string CARGAR_CORREOS_DE_USUARIOS = "SELECT CorreoElectronico FROM Alumnos UNION SELECT CorreoElectronico FROM DocentesAcademicos UNION SELECT CorreoElectronico FROM Directores";
		public const string CONTAR_OCURRENCIAS_DE_CORREO = "SELECT (SELECT COUNT(*) FROM Alumnos WHERE CorreoElectronico = @CorreoElectronico) + (SELECT COUNT(*) FROM Directores WHERE CorreoElectronico = @CorreoElectronico) + (SELECT COUNT(*) FROM DocentesAcademicos WHERE CorreoElectronico = @CorreoElectronico) + (SELECT COUNT(*) FROM Organizaciones WHERE CorreoElectronico = @CorreoElectronico)";
		public const string CONTAR_OCURRENCIAS_DE_MATRICULA = "SELECT COUNT(*) FROM Alumnos WHERE Matricula = @Matricula";
	}
}
