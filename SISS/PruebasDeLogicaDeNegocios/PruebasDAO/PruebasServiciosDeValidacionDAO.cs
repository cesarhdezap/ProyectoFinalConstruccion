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
			int ocurrencias = serviciosDeValidacionDAO.ContarOcurrenciasDeCorreo("a@uv.mx");

			Assert.AreEqual(3, ocurrencias);
		}

		[TestMethod]
		public void ProbarContarOcurrenciasDeCorreo_Regresa0()
		{
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();
			int ocurrencias = serviciosDeValidacionDAO.ContarOcurrenciasDeCorreo("a@sdfsduv.mx");

			Assert.AreEqual(0, ocurrencias);
		}
	}
}
