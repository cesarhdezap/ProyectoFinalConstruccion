using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace PruebasDeLogicaDeNegocios.PruebasDAO
{
    /// <summary>
    /// Descripción resumida de PruebasDeAlumnoDAO
    /// </summary>
    [TestClass]
    public class PruebasDeAlumnoDAO
    {
        public PruebasDeAlumnoDAO()
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

        //[DataTestMethod]
        //[DataRow("s17012947")]
        public void PruebaCargarAlumnoPorMatricula_RegresaAlumno(string input)
        {
            //Organizar
            Alumno alumnoEsperado = new Alumno();
            alumnoEsperado.Matricula = input;
            Alumno alumnoActual;
            AlumnoDAO alumnoDAO = new AlumnoDAO();

            //Actuar
            alumnoActual = alumnoDAO.CargarAlumnoPorMatricula(input);

            //Declarar
            Assert.AreEqual(alumnoEsperado, alumnoActual);
        }
    }
}
