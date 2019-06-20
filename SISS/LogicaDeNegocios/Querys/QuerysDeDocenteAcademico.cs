using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeDocenteAcademico
	{
public const string CARGAR_ID_COORDINADOR_POR_CARRERA = "SELECT IDPersonal FROM DocentesAcademicos WHERE Carrera = @Carrera AND Rol = 0 AND EsActivo = 1";
		public const string ACTUALIZAR_DOCENTE_ACADEMICO_POR_IDPERSONAL = "UPDATE DocentesAcademicos SET Nombre = @NombreDocenteAcademico, CorreoElectronico = @CorreoElectronicoDocenteAcademico, Telefono = @TelefonoDocenteAcademico, EsActivo = @EsActivoDocenteAcademico";
		public const string CARGAR_ID_POR_CORREO_Y_ROL = "SELECT IDPersonal FROM DocentesAcademicos WHERE CorreoElectronico = @CorreoElectronico AND Rol = @Rol";
		public const string CARGAR_DOCENTE_ACADEMICO_POR_IDPERSONAL = "SELECT * FROM DocentesAcademicos WHERE IDPersonal = @IDPersonal";
		public const string CARGAR_ID_POR_IDDOCUMENTO = "SELECT IDDocenteAcademico FROM ReportesMensuales WHERE IDDocumento = @IDDocumento";
		public const string CARGAR_DOCENTES_ACADEMICOS_POR_ESTADO = "SELECT * FROM DocentesAcademicos WHERE EsActivo = @EsActivo";
		public const string CARGAR_DOCENTES_ACADADEMICOS_POR_ROL = "SELECT * FROM DocentesAcademicos WHERE Rol = @Rol";
		public const string GUARDAR_DOCENTE_ACADEMICO = "INSERT INTO DocentesAcademicos(Nombre, Rol, IDCoordinador, CorreoElectronico, Telefono, Carrera, EsActivo, Cubiculo, Contraseña) VALUES (@NombreDocenteAcademico, @RolDocenteAcademico, @IDCoordinadorDocenteAcademico, @CorreoElectronicoDocenteAcademico, @TelefonoDocenteAcademico, @CarreraDocenteAcademico, @EsActivoDocenteAcademico, @CubiculoDocenteAcademico, @ContraseñaDocenteAcademico)";
	}
}
