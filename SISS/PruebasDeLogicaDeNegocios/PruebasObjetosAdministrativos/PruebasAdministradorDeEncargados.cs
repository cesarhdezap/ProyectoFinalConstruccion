using System;
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
            administradorDeEncargados.Encargados.Add(new Encargado()
            {
                Nombre = "Rosa",
                IDEncargado = 23,
                Organizacion = new Organizacion
                {
                    IDOrganizacion = 9
                }
            });
            administradorDeEncargados.Encargados.Add(new Encargado()
            {
                Nombre = "Roberto",
                IDEncargado = 24,
                Organizacion = new Organizacion
                {
                    IDOrganizacion = 9
                }
            });
            administradorDeEncargados.Encargados.Add(new Encargado()
            {
                Nombre = "Marisol",
                IDEncargado = 25,
                Organizacion = new Organizacion
                {
                    IDOrganizacion = 10
                }
            });
            int numeroDeCoincidencias = administradorDeEncargados.SeleccionarEncargadosPorIDOrganizacion(9).Count;

            Assert.AreEqual(2, numeroDeCoincidencias);
        }
    }
}
