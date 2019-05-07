using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasDeLogicaDeNegocios
{
    /// <summary>
    /// Descripción resumida de PruebasDeServiciosDeAutenticacion
    /// </summary>
    [TestClass]
    public class PruebasDeServiciosDeAutenticacion
    {
        public PruebasDeServiciosDeAutenticacion()
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
        [DataRow("contraseñaInsegura25*", "0553920422b2dda037d95007ccef8c5ac4c8802afeaf213af3ed8dcf0b857522")]
        [DataRow("*seC43ure1Pass?", "4d59227354fd822e8de6b50ee03da1c7a90170d0a81782d7043ac1a68c79165d")]
        [DataRow("56N9F;wJsK", "754ae9f7ca38a4f6cd5ed45ceba4ce91dbfba1193000661e2135a0ff9c8614c6")]
        [DataRow("c8A_7FRnEBB]9v", "497b69033b944db49e0ab1fbe0e088734db03e5d51c10e3d5b995be6a6a8ceb7")]
        [DataRow("gCsC?~af_Q<b)e}PEU>SKu[A>wC]<[<uf5},aq3kke%}aJwbgH", "162f07cd00e241d6533ea31acc6dcf2db24d9b5482fb2df3977c0f5c807aebe0")]

        public void ProbarEncriptarContraseña_RegresaContraseñaConHash(string input, string contrasenaHasheadaEsperada)
        {
            //Organizar
            string contrasenaHasheadaActual;

            //Actuar
            contrasenaHasheadaActual = LogicaDeNegocios.Servicios.ServiciosDeAutenticacion.EncriptarContraseña(input);

            //Declarar
            Console.WriteLine("\n\n\n" + contrasenaHasheadaActual);
            Assert.AreEqual(contrasenaHasheadaEsperada, contrasenaHasheadaActual);
        }

    }
}
