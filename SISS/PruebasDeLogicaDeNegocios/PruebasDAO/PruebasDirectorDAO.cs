using LogicaDeNegocios.ObjetoAccesoDeDatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas.PruebasDAO
{
	[TestClass]
    public class PruebasDirectorDAO
    {
        [DataTestMethod]
        [DataRow("raul@correo.com","1")]
        [DataRow("jose@correo.com","2")]
        public void ProbarCargarIDPorCorreo_Correcto_RegresaString(string correo, string idEsperada)
        {
            DirectorDAO directorDAO = new DirectorDAO();
            string idActual = directorDAO.CargarIDPorCorreo(correo);
            Assert.AreEqual(idEsperada, idActual);
        }
    }
}
