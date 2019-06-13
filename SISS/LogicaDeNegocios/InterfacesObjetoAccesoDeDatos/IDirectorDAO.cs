using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaDeNegocios.ClasesDominio;

namespace LogicaDeNegocios.Interfaces
{
    interface IDirectorDAO
    {
        string CargarIDPorCorreo(string correoElectronico);
        Director CargarDirectorPorIDPersonal(int IDPersonal);
    }
}
