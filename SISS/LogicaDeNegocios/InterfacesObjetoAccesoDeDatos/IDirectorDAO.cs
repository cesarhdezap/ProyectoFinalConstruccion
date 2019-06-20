using LogicaDeNegocios.ClasesDominio;

namespace LogicaDeNegocios.Interfaces
{
	interface IDirectorDAO
    {
        string CargarIDPorCorreo(string correoElectronico);
        Director CargarDirectorPorIDPersonal(int IDPersonal);
    }
}
