using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeAlumno
	{
		public const string ACTUALIZAR_ALUMNO = "UPDATE Alumnos SET Nombre = @NombreAlumno, Estado = @EstadoAlumno, Telefono = @TelefonoAlumno, CorreoElectronico = @CorreoElectronicoAlumno WHERE Matricula = @MatriculaAlumno";
		public const string CARGAR_MATRICULA_ALUMNO = "SELECT Matricula FROM Alumnos WHERE CorreoElectronico = @CorreoElectronico";
		public const string CARGAR_ALUMNOS_POR_ESTADO = "SELECT * FROM Alumnos WHERE Estado = @EstadoAlumno";
	}
}
