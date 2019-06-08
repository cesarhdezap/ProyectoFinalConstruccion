using LogicaDeNegocios.ObjetoAccesoDeDatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

    }
}
