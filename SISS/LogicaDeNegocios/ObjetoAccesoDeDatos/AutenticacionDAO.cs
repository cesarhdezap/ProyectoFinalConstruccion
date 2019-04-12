using LogicaDeNegocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.ObjetoAccesoDeDatos
{
    public class AutenticacionDAO : IAutenticacionDAO
    {
        public List<string> CargarContraseñasPorCorreo(string correo)
        {
            throw new NotImplementedException();
        }

        public List<string> CargarCorreoDeUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}
