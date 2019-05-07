using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LogicaDeNegocios.Services.ServiciosDeValidacion;

namespace PruebasDeLogicaDeNegocios
{
    /// <summary>
    /// Descripción resumida de PruebasDeServiciosDeValidacion
    /// </summary>
    [TestClass]
    public class PruebasDeServiciosDeValidacion
    {
        public PruebasDeServiciosDeValidacion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [DataTestMethod]
        [DataRow("email")]
        [DataRow("@gmail.com")]
        [DataRow("email@gmail")]
        [DataRow("email.com")]
        public void ProbarValidarCorreoElectronico_Incorrecto_RegresaNoValido(string input)
        {
            EresultadoDeValidacion resultadoActual = new EresultadoDeValidacion();
            EresultadoDeValidacion resultadoEsperado = EresultadoDeValidacion.NoValido;

            resultadoActual = ValidarCorreoElectronico(input);

            Assert.AreEqual(resultadoEsperado, resultadoActual);
        }
        
        [DataTestMethod]
        [DataRow("2224")]
        [DataRow("222 333 111")]
        [DataRow("abc")]
        [DataRow("777777'111")]
        public void ProbarValidarTelefono_Incorrecto_RegresaNoValido(string input)
        {
            EresultadoDeValidacion resultadoActual = new EresultadoDeValidacion();
            EresultadoDeValidacion resultadoEsperado = EresultadoDeValidacion.NoValido;

            resultadoActual = ValidarTelefono(input);

            Assert.AreEqual(resultadoEsperado, resultadoActual);
        }

        [DataTestMethod]
        [DataRow("Brhadaranyakopanishadvivekachudamani_Herrera_Carrasco")]
        [DataRow("Cesar21 Hernandez")]
        [DataRow("")]
        [DataRow("24534o")]
        public void ProbarValidarNombre_Incorrecto_RegresaNoValido(string input)
        {
            EresultadoDeValidacion resultadoActual = new EresultadoDeValidacion();
            EresultadoDeValidacion resultadoEsperado = EresultadoDeValidacion.NoValido;

            resultadoActual = ValidarNombre(input);

            Assert.AreEqual(resultadoEsperado, resultadoActual);
        }

        [DataTestMethod]
        [DataRow("S29 05 62 17")]
        [DataRow("zS29056217")]
        [DataRow("")]
        [DataRow("290066217")]
        public void ProbarValidarMatricula_Incorrecto_RegresaNoValido(string input)
        {
            EresultadoDeValidacion resultadoActual = new EresultadoDeValidacion();
            EresultadoDeValidacion resultadoEsperado = EresultadoDeValidacion.NoValido;

            resultadoActual = ValidarMatricula(input);

            Assert.AreEqual(resultadoEsperado, resultadoActual);
        }
    }
}
