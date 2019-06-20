using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios.Servicios
{
	/// <summary>
	/// Clase para identificar el tipo de sesión.
	/// Contiene métodos para cargar la sesión
	/// </summary>
	public class ServiciosDeSesion
    {
        private const int ID_NO_ASIGNADA = 0;

        /// <summary>
        /// Carga el tipo Sesion por correo electrónico.
        /// </summary>
        /// <param name="correo">Correo del usuario en cadena de carácteres.</param>
        /// <returns>Objeto Sesion del correo.</returns>
        public static Sesion CargarSesion(string correo)
        {
            Sesion sesion = new Sesion();
			AlumnoDAO alumnoDAO = new AlumnoDAO();
            sesion.IDUsuario = alumnoDAO.CargarMatriculaPorCorreoElectronico(correo);
            sesion.TipoDeUsuario = TipoDeSesion.Alumno;

			if (sesion.IDUsuario == string.Empty)
			{
				sesion.TipoDeUsuario = TipoDeSesion.NoValido;
				DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
				sesion.IDUsuario = docenteAcademicoDAO.CargarIDPorCorreoYRol(correo, Rol.Coordinador);
				sesion.TipoDeUsuario = TipoDeSesion.Coordinador;
			}

			if (sesion.IDUsuario == ID_NO_ASIGNADA.ToString())
			{
				sesion.TipoDeUsuario = TipoDeSesion.NoValido;
				DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
				sesion.IDUsuario = docenteAcademicoDAO.CargarIDPorCorreoYRol(correo, Rol.TecnicoAcademico);
				sesion.TipoDeUsuario = TipoDeSesion.Tecnico;
			}

			if (sesion.IDUsuario == ID_NO_ASIGNADA.ToString())
			{
				sesion.TipoDeUsuario = TipoDeSesion.NoValido;
				DirectorDAO directorDAO = new DirectorDAO();
				sesion.IDUsuario = directorDAO.CargarIDPorCorreo(correo);
				sesion.TipoDeUsuario = TipoDeSesion.Director;
			}

            return sesion;
        }

        public enum TipoDeSesion
        {
            Director,
            Coordinador,
            Tecnico,
            Alumno,
            NoValido
        }
    }
}
