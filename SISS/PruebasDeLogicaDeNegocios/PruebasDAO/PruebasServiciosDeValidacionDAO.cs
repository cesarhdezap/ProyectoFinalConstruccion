using System;
using System.Collections.Generic;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class PruebasServiciosDeValidacionDAO
    {
        [TestMethod]
        public void ProbarCargarCorreosDeUsuario_Correcto_RegresaListaDeCadenas()
        {
            ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();
            List<string> listaDeCorreos = serviciosDeValidacionDAO.CargarCorreosDeUsuarios();

            Assert.AreEqual(12, listaDeCorreos.Count);
        }

		[TestMethod]
		public void ProbarContarOcurrenciasDeCorreo_Regresa2()
		{
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();
			int ocurrencias = serviciosDeValidacionDAO.ContarOcurrenciasDeCorreo("a@mia.mx");

			Assert.AreEqual(3, ocurrencias);
		}

		[TestMethod]
		public void ProbarContarOcurrenciasDeCorreo_Regresa0()
		{
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();
			int ocurrencias = serviciosDeValidacionDAO.ContarOcurrenciasDeCorreo("a@sdfsduv.mx");

			Assert.AreEqual(0, ocurrencias);
		}

		[TestMethod]
		public void ProbarContarOcurrenciasDeMatricula_Regresa1()
		{
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();
			int ocurrencias = serviciosDeValidacionDAO.ContarOcurrenciasDeMatricula("s17012931");

			Assert.AreEqual(1, ocurrencias);
		}

		[TestMethod]
		public void ProbarContarOcurrenciasDeMatricula_Regresa1_2()
		{
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();
			int ocurrencias = serviciosDeValidacionDAO.ContarOcurrenciasDeMatricula("s99999999");

			Assert.AreEqual(1, ocurrencias);
		}

		[TestMethod]
		public void ProbarContarOcurrenciasDeMatricula_Regresa0()
		{
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();
			int ocurrencias = serviciosDeValidacionDAO.ContarOcurrenciasDeMatricula("s11119999");

			Assert.AreEqual(0, ocurrencias);
		}
	}
}
