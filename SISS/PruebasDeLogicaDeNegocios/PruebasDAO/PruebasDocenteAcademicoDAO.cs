using System;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class PruebasDocenteAcademicoDAO
    {
        [DataTestMethod]
        [DataRow("julio@correo.com",Rol.Coordinador,"1")]
        [DataRow("joseph@correo.com",Rol.TecnicoAcademico,"4")]
        public void ProbarCargarIDPorCorreoYRol_Correcto_RegresaString(string correo,Rol rol, string idActual)
        {
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO()
;            string idEsperada = docenteAcademicoDAO.CargarIDPorCorreoYRol(correo, rol);
            Assert.AreEqual(idEsperada, idActual);
        }
    }
}
