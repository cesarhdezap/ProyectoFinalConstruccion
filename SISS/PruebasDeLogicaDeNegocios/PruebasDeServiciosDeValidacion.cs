using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LogicaDeNegocios.Services.ServiciosDeValidacion;
using LogicaDeNegocios.Services;

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

        [DataRow("Patchinster@Gmail.com")]
        [DataRow("Wenlock0999@gmail.om")]
        [DataRow("a@a.abc")]
        [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa.aaa")]
        [DataRow("#$%&@yy3.com")]
        [DataRow("zS17012931@estudiantes.uv.com")]
        [DataTestMethod]
        public void ProbarValidarCorreoElectronicoValido_RegresaValido(string CorreoElectronico)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarCorreoElectronico(CorreoElectronico);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.Valido, resultadoDeValidacion);
        }

        [DataRow("1234567899")]
        [DataRow("2942393046")]
        [DataRow("0987654321")]
        [DataRow("1111111111")]
        [DataRow("2222222222")]
        [DataRow("0000000000")]
        [DataTestMethod]
        public void ProbarValidarTelefonoValido_RegresaValido(string Telefono)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarTelefono(Telefono);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.Valido, resultadoDeValidacion);
        }

        [DataRow("Josué Alejandro Díaz Rojas")]
        [DataRow("César Andrés Alarcón Anteo")]
        [DataRow("César Christopher Hernández Aparicio")]
        [DataRow("Chema")]
        [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [DataRow("Pablo Diego José Francisco de Paula Nepomuceno Maria de los Remedios de la Santisima Trinidad Ruiz Picasso")]
        [DataTestMethod]
        public void ProbarValidarNombreValido_RegresaValido(string Nombre)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarNombre(Nombre);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.Valido, resultadoDeValidacion);
        }

        [DataRow("z17012931")]
        [DataRow("z17012932")]
        [DataRow("z00000000")]
        [DataRow("z11111111")]
        [DataRow("z16548739")]
        [DataRow("z17013831")]
        [DataTestMethod]
        public void ProbarValidarMatriculaValida_RegresaValido(string Matricula)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarMatricula(Matricula);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.Valido, resultadoDeValidacion);
        }


        [DataTestMethod]
        [DataRow("email")]
        [DataRow("@gmail.com")]
        [DataRow("email@gmail")]
        [DataRow("email.com")]
        public void ProbarValidarCorreoElectronico_Incorrecto_RegresaNoValido(string input)
        {
            ResultadoDeValidacion resultadoActual = new ResultadoDeValidacion();
            ResultadoDeValidacion resultadoEsperado = ResultadoDeValidacion.NoValido;

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
            ResultadoDeValidacion resultadoActual = new ResultadoDeValidacion();
            ResultadoDeValidacion resultadoEsperado = ResultadoDeValidacion.NoValido;

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
            ResultadoDeValidacion resultadoActual = new ResultadoDeValidacion();
            ResultadoDeValidacion resultadoEsperado = ResultadoDeValidacion.NoValido;

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
            ResultadoDeValidacion resultadoActual = new ResultadoDeValidacion();
            ResultadoDeValidacion resultadoEsperado = ResultadoDeValidacion.NoValido;

            resultadoActual = ValidarMatricula(input);

            Assert.AreEqual(resultadoEsperado, resultadoActual);
        }
    }
}
