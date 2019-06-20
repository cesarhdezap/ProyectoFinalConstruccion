using LogicaDeNegocios.ObjetoAccesoDeDatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas.PruebasDAO
{
	[TestClass]
    public class PruebasEncargadoDAO
    {
        [TestMethod]
        public void ProbarCargarEncargadosConIDYNombre()
        {
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            encargadoDAO.CargarEncargadosConIDNombreYOrganizacion();
            Assert.IsTrue(true);
        }
    }
}
