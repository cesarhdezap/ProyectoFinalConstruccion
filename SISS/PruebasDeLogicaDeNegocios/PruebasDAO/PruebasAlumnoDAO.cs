using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;

namespace Pruebas.PruebasDAO
{
    /// <summary>
    /// Pruebas de los métodos de <see cref="AlumnoDAO"/>.
    /// </summary>
    [TestClass]
    public class PruebasAlumnoDAO
    {
        private string MENSAJE_DE_ERROR_EXCEPCION_ESPERADA = "Se esperaba AccesoADatosException de tipo de error: ";
        [TestMethod]
        public void ProbarGuardarAlumno_Correcto_RegresaVoid()
        {
            Alumno alumnoDePrueba = new Alumno();
            alumnoDePrueba.Nombre = "Cesar Andres Alarcon Anteo";
            alumnoDePrueba.CorreoElectronico = "Welock099@Gmail.com";
            alumnoDePrueba.Telefono = "2281346756";
            alumnoDePrueba.Matricula = "s16012931";
            alumnoDePrueba.Carrera = "LIS";
            alumnoDePrueba.Contraseña = "Contraseña122333";
            alumnoDePrueba.EstadoAlumno = EstadoAlumno.EsperandoAceptacion;
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            alumnoDAO.GuardarAlumno(alumnoDePrueba);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ProbarGuardarAlumno_IDRepetida_RegresaAccesoADatosException()
        {
            Alumno alumnoDePrueba = new Alumno();
            alumnoDePrueba.Nombre = "Cesar Andres Alarcon Anteo";
            alumnoDePrueba.CorreoElectronico = "Welock099@Gmail.com";
            alumnoDePrueba.Telefono = "2281346756";
            alumnoDePrueba.Matricula = "s16012931";
            alumnoDePrueba.Carrera = "LIS";
            alumnoDePrueba.Contraseña = "Contraseña122333";
            alumnoDePrueba.EstadoAlumno = EstadoAlumno.EsperandoAceptacion;

            AlumnoDAO alumnoDAO = new AlumnoDAO();
            TipoDeErrorDeAccesoADatos tipoDeError = TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada;
            bool excepcionTirada = false;
            try
            {
                alumnoDAO.GuardarAlumno(alumnoDePrueba);
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == tipoDeError)
            {
                excepcionTirada = true;
            }

            if (!excepcionTirada)
            {
                throw new AssertFailedException(MENSAJE_DE_ERROR_EXCEPCION_ESPERADA + tipoDeError.ToString());
            }

        }

        [DataTestMethod]
        [DataRow("s16012931")]
        public void PruebaCargarAlumnoPorMatricula_MatriculaCorrecta_RegresaAlumno(string matricula)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            Alumno alumnoActual = alumnoDAO.CargarAlumnoPorMatricula(matricula);
            string matriculaActual = alumnoActual.Matricula;
            Assert.AreEqual(matricula, matriculaActual);
        }

        [TestMethod]
        public void PruebaActualizarAlumnoPorMatricula_EnLimiteSuperior_RegresaVoid()
        {
            const string CADENA_DE_255_CARACTERES = "ABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABCABC";
            Alumno alumno = new Alumno()
            {
                Matricula = "s16012931",
                Nombre = CADENA_DE_255_CARACTERES,
                CorreoElectronico = CADENA_DE_255_CARACTERES,
                Telefono = "123456789101112",
                Carrera = CADENA_DE_255_CARACTERES,
                Contraseña = CADENA_DE_255_CARACTERES,
                EstadoAlumno = EstadoAlumno.DadoDeBaja
            };

            AlumnoDAO alumnoDAO = new AlumnoDAO();
            alumnoDAO.ActualizarAlumnoPorMatricula(alumno.Matricula, alumno);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void PruebaActualizarAlumnoPorMatricula_MatriculaNoExiste_RegresaAccesoADatosException()
        {
            Alumno alumno = new Alumno()
            {
                Matricula = "s17171717",
                Nombre = "AlumnoNoExistente",
                CorreoElectronico = "pruebaAlumnoInexistente@correo.com",
                Telefono = "123456789101112",
                Carrera = Carrera.LIS.ToString(),
                Contraseña = "passworddeejemplo",
                EstadoAlumno = EstadoAlumno.DadoDeBaja
            };
            TipoDeErrorDeAccesoADatos tipoDeError = TipoDeErrorDeAccesoADatos.ObjetoNoExiste;
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            bool excepcionTirada = false;
            try
            {
                alumnoDAO.ActualizarAlumnoPorMatricula(alumno.Matricula, alumno);
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == tipoDeError)
            {
                excepcionTirada = true;
            }

            if (!excepcionTirada)
            {
                throw new AssertFailedException(MENSAJE_DE_ERROR_EXCEPCION_ESPERADA + tipoDeError.ToString());
            }
        }

        [DataTestMethod]
        [DataRow("cesarhdez@gmail.com", "s17012947")]
        public void ProbarCargarMatriculaPorCorreoElectronico_MatriculaCorrecta(string correo, string matriculaEsperada)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            string matriculaActual;
            matriculaActual = alumnoDAO.CargarMatriculaPorCorreoElectronico(correo);
            Assert.AreEqual(matriculaEsperada, matriculaActual);
        }

        [TestMethod]
        public void ProbarCargarMatriculaPorCorreoElectronico_CorreoNoExiste_RegresaStringVacia()
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            string matricula;
            matricula = alumnoDAO.CargarMatriculaPorCorreoElectronico("correoinexistente@correos.com");
            Assert.AreEqual(string.Empty, matricula);
        }

        [TestMethod]
        public void ProbarCargarAlumnosPorEstado_SinExcepciones_RegresaListaDeAlumnos()
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            EstadoAlumno estadoAlumno = EstadoAlumno.Aceptado;
            List<Alumno> listaDeAlumnos;
            listaDeAlumnos = alumnoDAO.CargarAlumnosPorEstado(estadoAlumno);
            bool estadoDeAlumnoDiferente = listaDeAlumnos.Exists(alumno => alumno.EstadoAlumno != estadoAlumno);
            Assert.IsFalse(estadoDeAlumnoDiferente);
        }

        [TestMethod]
        public void ProbarCargarAlumnosPorEstado_NoSeEncuentranAlumnos_RegresaListaVacia()
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            EstadoAlumno estadoAlumno = EstadoAlumno.Rechazado;
            List<Alumno> listaDeAlumnos = alumnoDAO.CargarAlumnosPorEstado(estadoAlumno);
            bool listaVacia = true;
            if (listaDeAlumnos.Count > 0)
            {
                Assert.Fail("Se encontraron alumnos en CargarAlumnosPorEstado en un Estado Sin Alumnos");
                listaVacia = false;
            }
            Assert.IsTrue(listaVacia);
        }

        [TestMethod]
        public void ProbarCargarAlumnosPorCarrera_SinExcepciones_RegresaListaDeAlumnos()
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            string carrera = Carrera.LIS.ToString();
            List<Alumno> listaDeAlumnos;
            listaDeAlumnos = alumnoDAO.CargarAlumnosPorCarrera(carrera);
            bool carreraDeAlumnoDiferente = listaDeAlumnos.Exists(alumno => alumno.Carrera != carrera);
            Assert.IsFalse(carreraDeAlumnoDiferente);
        }

        [TestMethod]
        public void ProbarCargarAlumnosPorCarrera_NoSeEncuentranAlumnos_RegresaListaVacia()
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            string carrera = Carrera.TC.ToString();
            List<Alumno> listaDeAlumnos = alumnoDAO.CargarAlumnosPorCarrera(carrera);
            bool listaVacia = true;
            if (listaDeAlumnos.Count > 0)
            {
                Assert.Fail("Se encontraron alumnos en CargarAlumnosPorEstado en un Estado Sin Alumnos");
                listaVacia = false;
            }
            Assert.IsTrue(listaVacia);
        }

        [DataTestMethod]
        [DataRow(4, "s16012931")]
        public void ProbarCargarMatriculaPorIDAsignacion_SinExcepciones_RegresaString(int IDAsignacion, string matriculaEsperada)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            Alumno alumno = alumnoDAO.CargarMatriculaPorIDAsignacion(IDAsignacion);
            Assert.AreEqual(matriculaEsperada, alumno.Matricula);
        }

        [TestMethod]
        public void ProbarCargarMatriculaPorIDAsignacion_NoSeEncuentraAsignacion_RegresaAlumnoInicializadoSinDatos()
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            Alumno alumno = alumnoDAO.CargarMatriculaPorIDAsignacion(2999);
            Assert.AreEqual(string.Empty, alumno.Matricula);
        }
        
    }
}