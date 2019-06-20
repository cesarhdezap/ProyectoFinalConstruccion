using LogicaDeNegocios.ObjetoAccesoDeDatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicaDeNegocios.ClasesDominio;

namespace Pruebas.PruebasDAO
{
	[TestClass]
    public class PruebasDirectorDAO
    {
        [DataTestMethod]
        [DataRow("raul@gmail.com","1")]
        [DataRow("juanj@correo.com","2")]
        public void ProbarCargarIDPorCorreo_Correcto_RegresaString(string correo, string idEsperada)
        {
            DirectorDAO directorDAO = new DirectorDAO();
            string idActual = directorDAO.CargarIDPorCorreo(correo);
            Assert.AreEqual(idEsperada, idActual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void ProbarCargarDirectorPorIDPersonal_SinExcepciones_RegresaDirector(int IDDirector)
        {
            DirectorDAO directorDAO = new DirectorDAO();
            Director director = directorDAO.CargarDirectorPorIDPersonal(IDDirector);
            Assert.AreEqual(IDDirector, director.IDPersonal);
        }
    }
}
