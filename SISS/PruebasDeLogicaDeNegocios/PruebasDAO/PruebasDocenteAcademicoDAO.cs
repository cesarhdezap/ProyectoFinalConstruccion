using System;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicaDeNegocios.ClasesDominio;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class PruebasDocenteAcademicoDAO
    {
        [TestMethod]
        public void ProbarGuardarDocenteAcademico_SinExcepciones_RegresaVoid()
        {
            DocenteAcademico docenteAcademico = new DocenteAcademico()
            {
                Nombre = "Docente academico Coordinador",
                Rol = Rol.Coordinador,
                CorreoElectronico = "coordinadorDePrueba@correo.com",
                Telefono = "0011001100",
                Carrera = Carrera.RYSC.ToString(),
                EsActivo = true,
                Cubiculo = 999,
                Contraseña = "passwordDeCoordinadorDePrueba"
            };
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            docenteAcademicoDAO.GuardarDocenteAcademico(docenteAcademico);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ProbarActualizarDocenteAcademicoPorIDPersonal_SinExcepciones_RegresaVoid()
        {
            DocenteAcademico docenteAcademico = new DocenteAcademico()
            {
                IDPersonal = 8,
                Nombre = "Docente academico Coordinador actualizado",
                Rol = Rol.Coordinador,
                CorreoElectronico = "coordinadorDePrueba@correo.com",
                Telefono = "1111111111",
                Carrera = Carrera.LIS.ToString(),
                EsActivo = true,
                Cubiculo = 1000,
                Contraseña = "passwordDePrueba"
            };
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            docenteAcademicoDAO.ActualizarDocenteAcademicoPorIDPersonal(docenteAcademico.IDPersonal, docenteAcademico);
            Assert.IsTrue(true);
        }

        [DataTestMethod]
        [DataRow("coordinadorDePrueba@correo.com", Rol.Coordinador,"8")]
        public void ProbarCargarIDPorCorreoYRol_Correcto_RegresaString(string correo,Rol rol, string idActual)
        {
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO()
;            string idEsperada = docenteAcademicoDAO.CargarIDPorCorreoYRol(correo, rol);
            Assert.AreEqual(idEsperada, idActual);
        }


    }
}
