﻿using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using System;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios
{
	public class Encargado : Persona
	{
		public int IDEncargado { get; set; }
		public string Puesto { get; set; }
		public List<Proyecto> Proyectos { get; set; }
        public Organizacion Organizacion { get; set; }

		public void AñadirProyecto(Proyecto proyecto)
		{
            proyecto.Encargado = this;
            this.Proyectos.Add(proyecto);
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            proyectoDAO.GuardarProyecto(proyecto);
        }

        public bool Guardar()
        {
            bool resultadoDeCreacion = false;
            AdministradorDeEncargados administradorDeEncargados = new AdministradorDeEncargados();
            if (ValidarEncargado() && administradorDeEncargados.ValidarExistencia(this))
            {
                EncargadoDAO encargadoDAO = new EncargadoDAO();
                encargadoDAO.GuardarEncargado(this);
                resultadoDeCreacion = true;
            }
            return resultadoDeCreacion;
        }

        private bool ValidarEncargado()
        {
            bool resultadoDeValidacion = false;
            if (ValidarNombre(Nombre)
                && ValidarCorreoElectronico(CorreoElectronico)
                && ValidarTelefono(Telefono)
                && ValidarCadena(Puesto))
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }
    }
}
