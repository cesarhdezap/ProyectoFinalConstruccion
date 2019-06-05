using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;

namespace LogicaDeNegocios
{
    public class ServiciosDeSesion
    {
        public static int CargarIDDeUsuarioPorCorreo(string correo)
        {
            throw new NotImplementedException();
        }

        public static string CargarMatriculaDeAlumnoPorCorreo(string correo)
        {
            throw new NotImplementedException();
        }

        public static TipoDeSesion CargarTipoDeSesionPorCorreo(string correo)
        {
            TipoDeSesion tipoDeSesion = TipoDeSesion.NoValido;
            ServiciosDeSesion serviciosDeSesion = new ServiciosDeSesion();

            AlumnoDAO alumnoDAO = new AlumnoDAO();
            string matricula = alumnoDAO.CargarMatriculaPorCorreo(correo);

            return tipoDeSesion;
        }

        public enum TipoDeSesion
        {
            NoValido = -1,
            Director = 0,
            Coordinador = 1,
            Tecnico = 2,
            Alumno = 3,
        }
    }
}
