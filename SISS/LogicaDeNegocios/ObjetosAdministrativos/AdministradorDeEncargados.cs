﻿using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios.ObjetosAdministrador
{
    public class AdministradorDeEncargados
    {
        public List<Encargado> Encargados;

        public void CargarEncargadosTodos()
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            Encargados = encargadoDAO.CargarEncargadosConIDNombreYOrganizacion();
        }

        public bool ValidarExistencia(Encargado encargado)
        {
            bool resultadoDeCreacion = false;
            CargarEncargadosTodos();
            resultadoDeCreacion = !Encargados.Exists(e => e.CorreoElectronico == encargado.CorreoElectronico);
            return resultadoDeCreacion;
		}


        public List<Encargado> SeleccionarEncargadosPorIDOrganizacion(int IDOrganizacion)
        {
            List<Encargado> encargados = new List<Encargado>();
			this.CargarEncargadosTodos();
            if (Encargados.Count > 0)
            {
                encargados = Encargados.FindAll(encargadoActual => encargadoActual.Organizacion.IDOrganizacion == IDOrganizacion);
            }
            return encargados;
        }
    }
}
