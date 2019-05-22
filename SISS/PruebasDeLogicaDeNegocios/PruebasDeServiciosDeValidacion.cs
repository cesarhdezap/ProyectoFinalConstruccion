using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using LogicaDeNegocios.Servicios;

namespace PruebasDeLogicaDeNegocios
{

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
        [DataRow("elChema@hotmail.com")]
        [DataRow("elrevo@gmail.com")]
        [DataRow("marilu@yahoo.com")]
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
        [DataRow("2941393049")]
        [DataRow("7345129090")]
        [DataRow("2341256345")]
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
        [DataRow("Rodrigo")]
        [DataRow("Juan carlos")]
        [DataTestMethod]
        public void ProbarValidarNombreValido_RegresaValido(string Nombre)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarNombre(Nombre);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.Valido, resultadoDeValidacion);
        }
        [DataRow("z17012931")]
        [DataRow("z17012932")]
        [DataRow("z12345678")]
        [DataRow("z15345589")]
        [DataRow("z16548739")]
        [DataRow("z17013831")]
        [DataTestMethod]
        public void ProbarValidarMatriculaValida_RegresaValido(string Matricula)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarMatricula(Matricula);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.Valido, resultadoDeValidacion);
        }



        [DataRow("")]
        [DataTestMethod]
        public void ProbarValidarCorreoElectronicoVacio_RegresaNoValido(string CorreoElectronico)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarCorreoElectronico(CorreoElectronico);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.NoValido, resultadoDeValidacion);
        }

        [DataRow("")]
        [DataTestMethod]
        public void ProbarValidarTelefonoVacio_RegresaNoValido(string Telefono)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarTelefono(Telefono);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.NoValido, resultadoDeValidacion);
        }

        [DataRow("")]
        [DataTestMethod]
        public void ProbarValidarNombreVacio_RegresaNoValido(string Nombre)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarNombre(Nombre);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.NoValido, resultadoDeValidacion);
        }
        [DataRow("")]
        [DataTestMethod]
        public void ProbarValidarMatriculaVacio_RegresaNoValido(string Matricula)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarMatricula(Matricula);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.NoValido, resultadoDeValidacion);
        }
        [DataRow("aaaaaaaaaaaaaaaa@aaaaaaaaaaaaaaaaaaaaaa.aaa.aaa.aaa.aaa.aaa.aaa.aaa.aaa.aaa")]
        [DataRow("a@a.aa")]
        [DataTestMethod]
        public void ProbarValidarCorreoElectronicoDatosAlBorde_RegresaValido(string CorreoElectronico)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarCorreoElectronico(CorreoElectronico);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.Valido, resultadoDeValidacion);
        }

        [DataRow("0000000000")]
        [DataRow("1111111111")]
        [DataRow("2222222222")]
        [DataRow("3333333333")]
        [DataRow("4444444444")]
        [DataRow("5555555555")]
        [DataRow("6666666666")]
        [DataRow("7777777777")]
        [DataRow("8888888888")]
        [DataRow("9999999999")]
        [DataRow("0000000000")]
        [DataTestMethod]
        public void ProbarValidarTelefonoValidoDatosAlBorde_RegresaValido(string Telefono)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarTelefono(Telefono);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.Valido, resultadoDeValidacion);
        }

        [DataRow("Salvador Felipe Jacinto Dalí i Domènech marqués de Dalí de Púbol")]
        [DataRow("Diego María de la Concepción Juan Nepomuceno Estanislao de la Rivera y Barrientos Acosta y Rodríguez")]
        [DataRow("Pablo Diego José Francisco de Paula Juan Nepomuceno María de los Remedios Cipriano de la Santísima Trinidad Ruiz y Picasso")]
        [DataRow("Juan")]
        [DataRow("a")]
        [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [DataTestMethod]
        public void ProbarValidarNombreDatosAlBorde_RegresaValido(string Nombre)
        {
            ServiciosDeValidacion.ResultadoDeValidacion resultadoDeValidacion = ServiciosDeValidacion.ValidarNombre(Nombre);
            Assert.AreEqual(ServiciosDeValidacion.ResultadoDeValidacion.Valido, resultadoDeValidacion);
        }
        [DataRow("z00000000")]
        [DataRow("z11111111")]
        [DataRow("z22222222")]
        [DataRow("z33333333")]
        [DataRow("z44444444")]
        [DataRow("z55555555")]
        [DataRow("z66666666")]
        [DataRow("z77777777")]
        [DataRow("z88888888")]
        [DataRow("z99999999")]
        [DataTestMethod]
        public void ProbarValidarMatriculaDatosAlBorde_RegresaValido(string Matricula)
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
