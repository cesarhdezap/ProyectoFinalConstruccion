using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;

namespace LogicaDeNegocios.Servicios
{
    public class ServiciosDeSesion
    {
        private const int ID_NO_ASIGNADA = 0;

        public static Sesion CargarSesion(string correo)
        {
            Sesion sesion = new Sesion();

            AlumnoDAO alumnoDAO = new AlumnoDAO();
            try
            {
                sesion.IDUsuario = alumnoDAO.CargarMatriculaPorCorreoElectronico(correo);
                sesion.TipoDeUsuario = TipoDeSesion.Alumno;
            }
            catch (AccesoADatosException e)
            {
                Console.WriteLine("No se encontro la ID del correo {0} en CargarMatriculaPorCorreo. Stacktrace: {1}", e.Message, e);
            }

            if (sesion.IDUsuario == string.Empty)
            {
                sesion.TipoDeUsuario = TipoDeSesion.NoValido;
                DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
                try
                {
                    sesion.IDUsuario = docenteAcademicoDAO.CargarIDPorCorreoYRol(correo, Rol.Coordinador);
                    sesion.TipoDeUsuario = TipoDeSesion.Coordinador;
                }
                catch (AccesoADatosException e)
                {
                    Console.WriteLine("No se encontro la ID del correo {0} en CargarIDPorCorreoYRol. Stacktrace: {1}", e.Message, e);
                }
            }

            if (sesion.IDUsuario == ID_NO_ASIGNADA.ToString())
            {
                sesion.TipoDeUsuario = TipoDeSesion.NoValido;
                DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
                try
                {
                    sesion.IDUsuario = docenteAcademicoDAO.CargarIDPorCorreoYRol(correo, Rol.TecnicoAcademico);
                    sesion.TipoDeUsuario = TipoDeSesion.Tecnico;
                }
                catch (AccesoADatosException e)
                {
                    Console.WriteLine("No se encontro la ID del correo {0} en CargarIDPorCorreoYRol. Stacktrace: {1}", e.Message, e);
                    sesion.IDUsuario = string.Empty;
                    sesion.TipoDeUsuario = TipoDeSesion.NoValido;
                }
            }

            if (sesion.IDUsuario == ID_NO_ASIGNADA.ToString())
            {
                sesion.TipoDeUsuario = TipoDeSesion.NoValido;
                DirectorDAO directorDAO = new DirectorDAO();
                try
                {
                    sesion.IDUsuario = directorDAO.CargarIDPorCorreo(correo);
                    sesion.TipoDeUsuario = TipoDeSesion.Director;
                }
                catch (AccesoADatosException e)
                {
                    Console.WriteLine("No se encontro la ID del correo {0} en CargarIDPorCorreoYRol. Stacktrace: {1}", e.Message, e);
                    sesion.IDUsuario = string.Empty;
                    sesion.TipoDeUsuario = TipoDeSesion.NoValido;
                    throw new AccesoADatosException("Error: ServiciosDeSesion.CargarTipoDeSesion No se encontro la id del correo: " + correo);
                }
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
