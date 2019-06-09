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

        public bool CrearEncargado(Encargado encargado)
        {
            bool resultadoDeCreacion = false;
            CargarEncargadosTodos();
            resultadoDeCreacion = !Encargados.Exists(e => e.CorreoElectronico == encargado.CorreoElectronico) && ValidarEncargado(encargado);
            if (resultadoDeCreacion)
            {
                EncargadoDAO encargadoDAO = new EncargadoDAO();
                encargadoDAO.GuardarEncargado(encargado);
            }
            return resultadoDeCreacion;
		}

        private bool ValidarEncargado(Encargado encargado)
        {
            bool resultadoDeValidacion = false;
            if (ValidarNombre(encargado.Nombre)
                && ValidarCorreoElectronico(encargado.CorreoElectronico)
                && ValidarTelefono(encargado.Telefono)
                && ValidarPuestoEncargado(encargado.Puesto))
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }
    }
}
