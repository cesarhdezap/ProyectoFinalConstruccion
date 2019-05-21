using System.Collections.Generic;

namespace LogicaDeNegocios.Interfaces
{
	interface IAutenticacionDAO
    {
        string CargarContraseñaPorCorreo(string correoElectronico);
        List<string> CargarCorreoDeUsuarios();
        
    }
}
