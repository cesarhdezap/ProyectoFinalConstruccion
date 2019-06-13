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
    }
}
