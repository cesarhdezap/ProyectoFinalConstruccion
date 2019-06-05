using static LogicaDeNegocios.ServiciosDeSesion;

namespace LogicaDeNegocios.Interfaces
{
    interface ISesionDAO
    {
        TipoDeSesion CargarTipoDeSesionPorCorreo(string correo);
        int CargarIDDeUsuarioPorCorreo(string correo);
        string CargarMatriculaDeAlumnoPorCorreo(string correo);
    }
}
