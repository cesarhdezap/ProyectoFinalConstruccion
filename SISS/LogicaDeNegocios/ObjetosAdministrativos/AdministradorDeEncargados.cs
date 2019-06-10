using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios.ObjetosAdministrador
{
    public class AdministradorDeEncargados
    {
        private List<Encargado> Encargados;

        public void CargarEncargadosTodos()
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            Encargados = encargadoDAO.CargarEncargadosTodos();
        }

        public bool ValidarExistencia(Encargado encargado)
        {
            bool resultadoDeCreacion = false;
            CargarEncargadosTodos();
            resultadoDeCreacion = !Encargados.Exists(e => e.CorreoElectronico == encargado.CorreoElectronico);
            return resultadoDeCreacion;
		}

    }
}
