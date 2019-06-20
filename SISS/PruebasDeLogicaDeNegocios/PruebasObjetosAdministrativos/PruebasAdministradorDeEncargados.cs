using System.Collections.Generic;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetosAdministrador;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas.PruebasObjetosAdministrativos
{
	[TestClass]
    public class PruebasAdministradorDeEncargados
    {
        [TestMethod]
        public void ProbarSeleccionarEncargadosPorIDOrganizacion_Correcto_RegresaListaDeEncargados()
        {
            AdministradorDeEncargados administradorDeEncargados = new AdministradorDeEncargados();
            administradorDeEncargados.Encargados = new List<Encargado>();
            int numeroDeCoincidencias = administradorDeEncargados.SeleccionarEncargadosPorIDOrganizacion(9).Count;

            Assert.AreEqual(2, numeroDeCoincidencias);
        }
    }
}
