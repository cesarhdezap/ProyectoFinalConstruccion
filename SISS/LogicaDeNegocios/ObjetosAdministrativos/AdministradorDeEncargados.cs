using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
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
            CargarEncargadosTodos();
            bool resultadoDeCreacion = false;
            if (!Encargados.Exists(e => e.CorreoElectronico == encargado.CorreoElectronico))
            {
                EncargadoDAO encargadoDAO = new EncargadoDAO();
                encargadoDAO.GuardarEncargado(encargado);
                resultadoDeCreacion = true;
            }
            return resultadoDeCreacion;
		}
    }
}
