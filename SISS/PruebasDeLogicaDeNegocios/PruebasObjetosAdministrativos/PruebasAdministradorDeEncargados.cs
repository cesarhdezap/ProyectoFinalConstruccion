using System;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetosAdministrador;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas.PruebasObjetosAdministrativos
{
    [TestClass]
    public class PruebasAdministradorDeEncargados
    {
        [TestMethod]
        public void ProbarCrearEncargado_Correcto_RegresaBool()
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

            AdministradorDeEncargados admininistradorDeEncargados = new AdministradorDeEncargados();
            if (admininistradorDeEncargados.ValidarExistencia(encargado))
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
