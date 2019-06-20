using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeAsignacion
	{
		public const string ACTUALIZAR_ASIGNACION = "UPDATE Asignaciones SET Estado = @EstadoAsignacion, FechaDeFinal = @FechaDeFinalAsignacion, IDLiberacion = @IDLiberacionAsignacion WHERE IDAsignacion = @IDAsignacion ";
		public const string CARGAR_ASIGNACION_POR_ID = "SELECT * FROM Asignaciones WHERE IDAsignacion = @IDAsignacion";
		public const string CARGAR_ID_POR_MATRICULA_DE_ALUMNO = "SELECT IDAsignacion FROM Asignaciones WHERE Matricula = @Matricula";
		public const string CARGAR_IDS_POR_IDPROYECTO = "SELECT IDAsignacion FROM Asignaciones WHERE IDProyecto = @IDProyecto";
		public const string GUARDAR_ASIGNACION = "INSERT INTO Asignaciones(Estado, FechaDeInicio, Matricula, IDProyecto, IDSolicitud) VALUES(@EstadoAsignacion, @FechaDeInicioAsignacion, @MatriculaDeAlumnoAsignacion, @IDProyectoAsignacion, @IDSolicitudAsignacion)";

	}
}
