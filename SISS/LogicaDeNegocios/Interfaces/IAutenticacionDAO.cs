using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IAutenticacionDAO
    {
        List<string> CargarCorreoDeUsuarios();
        string CargarContraseñaPorCorreo(string correoElectronico);

    }
}
