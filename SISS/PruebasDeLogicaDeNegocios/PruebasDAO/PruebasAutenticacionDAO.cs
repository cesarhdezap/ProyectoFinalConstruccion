using LogicaDeNegocios.ObjetoAccesoDeDatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class PruebasAutenticacionDAO
    {
        [TestMethod]
        public void ProbarCargarCorreosDeUsuarios_SinExcepciones_RegresaListaDeString()
        {
            List<string> correos;
            AutenticacionDAO autenticacionDAO = new AutenticacionDAO();
            correos = autenticacionDAO.CargarCorreosDeUsuarios();
            Assert.IsTrue(true);
        }

        [DataTestMethod]
        [DataRow("cesarhdez@gmail.com", "passwordsegura")]
        public void ProbarCargarContraseñaPorCorreo_SinExcepciones_RegresaString(string correo, string contraseña)
        {
            string contraseñaActual;
            AutenticacionDAO autenticacionDAO = new AutenticacionDAO();
            contraseñaActual = autenticacionDAO.CargarContraseñaPorCorreo(correo);
            Assert.AreEqual(contraseña, contraseñaActual);
        }
    }
}
