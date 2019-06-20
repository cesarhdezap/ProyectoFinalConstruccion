namespace LogicaDeNegocios.Querys
{

	public static class QuerysDeAlumno
	{
		public const string ACTUALIZAR_ALUMNO = "UPDATE Alumnos SET Nombre = @NombreAlumno, Estado = @EstadoAlumno, Telefono = @TelefonoAlumno, CorreoElectronico = @CorreoElectronicoAlumno WHERE Matricula = @MatriculaAlumno";
		public const string CARGAR_MATRICULA_ALUMNO = "SELECT Matricula FROM Alumnos WHERE CorreoElectronico = @CorreoElectronico";
		public const string CARGAR_ALUMNOS_POR_ESTADO = "SELECT * FROM Alumnos WHERE Estado = @EstadoAlumno";
		public const string CARGAR_ALUMNOS_POR_CARRERA = "SELECT * FROM Alumnos WHERE Carrera = @Carrera";
		public const string CARGAR_ALUMNO_POR_MATRICULA = "SELECT * FROM Alumnos WHERE Matricula = @Matricula";
		public const string CARGAR_ALUMNOS_TODOS = "SELECT * FROM Alumnos";
		public const string CARGAR_MATRICULA_POR_IDASIGNACION = "SELECT Matricula FROM Asignaciones WHERE IDAsignacion = @IDAsignacion";
		public const string GUARDAR_ALUMNO = "INSERT INTO Alumnos(Matricula, Nombre, Carrera, Estado, Telefono, CorreoElectronico, Contraseña) VALUES (@MatriculaAlumno, @NombreAlumno, @CarreraAlumno, @EstadoAlumno, @TelefonoAlumno, @CorreoElectronicoAlumno, @ContraseñaAlumno)";
	}
}
