using System;
using LogicaDeNegocios;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas.PruebasClasesDominio
{
    [TestClass]
    public class PruebasEncargado
    {
        [TestMethod]
        public void ProbarGuardar_Correcto_RegresaBool()
        {
            Encargado encargado = new Encargado();
            encargado.Nombre = "EncargadoDePrueba";
            encargado.Organizacion = new Organizacion()
            {
                IDOrganizacion = 4
            };
            encargado.Puesto = "PuestoDePrueba";
            encargado.Telefono = "12345678";
            encargado.CorreoElectronico = "encargado@prueba.com";

            if (encargado.Guardar())
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("No se guardo el encargado.");
            }
        }
    }
}
