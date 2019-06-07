using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios.Servicios
{
    public class ServiciosDeSesion
    {
        public static Sesion CargarSesion(string correo)
        {
            Sesion sesion = new Sesion();
            string[] listaDeIDs = new string[4];

            AlumnoDAO alumnoDAO = new AlumnoDAO();
            listaDeIDs[(int)TipoDeSesion.Alumno] = alumnoDAO.CargarMatriculaPorCorreoElectronico(correo);
            if (listaDeIDs[(int)TipoDeSesion.Alumno] != string.Empty)
            {
                sesion.IDUsuario = listaDeIDs[(int)TipoDeSesion.Alumno];
                sesion.TipoDeUsuario = TipoDeSesion.Alumno;
            }
            else
            {
                DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
                listaDeIDs[(int)TipoDeSesion.Coordinador] = docenteAcademicoDAO.CargarIDPorCorreoYRol(correo, Rol.Coordinador);
                if (listaDeIDs[(int)TipoDeSesion.Coordinador] != string.Empty)
                {
                    sesion.IDUsuario = listaDeIDs[(int)TipoDeSesion.Coordinador];
                    sesion.TipoDeUsuario = TipoDeSesion.Coordinador;
                }
                else
                {
                    listaDeIDs[(int)TipoDeSesion.Tecnico] = docenteAcademicoDAO.CargarIDPorCorreoYRol(correo, Rol.TecnicoAcademico);
                    if (listaDeIDs[(int)TipoDeSesion.Tecnico] != string.Empty)
                    {
                        sesion.IDUsuario = listaDeIDs[(int)TipoDeSesion.Tecnico];
                        sesion.TipoDeUsuario = TipoDeSesion.Tecnico;
                    }
                    else
                    {
                        DirectorDAO directorDAO = new DirectorDAO();
                        listaDeIDs[(int)TipoDeSesion.Director] = directorDAO.CargarIDPorCorreo(correo);
                        if (listaDeIDs[(int)TipoDeSesion.Director] != string.Empty)
                        {
                            sesion.IDUsuario = listaDeIDs[(int)TipoDeSesion.Director];
                            sesion.TipoDeUsuario = TipoDeSesion.Director;
                        }
                        else
                        {
                            throw new AccesoADatosException("Error: ServiciosDeSesion.CargarTipoDeSesion No se encontro la id del correo: " + correo);
                        }
                    }
                }
            }
            return sesion;
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
