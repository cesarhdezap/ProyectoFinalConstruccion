namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeAutenticacion
	{
		public const string CARGAR_CONTRASEÑA_POR_CORREO = "SELECT Contraseña FROM (SELECT CorreoElectronico,Contraseña FROM Alumnos UNION SELECT CorreoElectronico, Contraseña FROM DocentesAcademicos UNION SELECT CorreoElectronico, Contraseña From Directores) AS U WHERE CorreoElectronico = @CorreoElectronico";
		public const string CARGAR_CORREOS_DE_USUARIOS = "SELECT CorreoElectronico FROM Alumnos UNION SELECT CorreoElectronico FROM DocentesAcademicos UNION SELECT CorreoElectronico FROM Directores";
	}
}
