using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LogicaDeNegocios;

namespace Pruebas
{
    [TestClass]
    public class PruebasDeAlumno
    {
        [TestMethod]
        public void ProbarDarDeBaja()
        {
            Alumno alumnoEsperado = new Alumno();
            alumnoEsperado.Matricula = "z17012931";
            alumnoEsperado.Carrera = "Licenciatura en Ingeniera de Software";
            alumnoEsperado.Contraseña = "ContraseñaChida123";
            alumnoEsperado.EstadoAlumno = EstadoAlumno.Desactivado;
            alumnoEsperado.Asignaciones = new List<Asignacion>();
            Asignacion asignacionAlumnoEsperado = new Asignacion();
            asignacionAlumnoEsperado.IDAsignacion = 1;
            alumnoEsperado.Asignaciones.Add(asignacionAlumnoEsperado);

            Alumno alumnoDePrueba = new Alumno();
            alumnoDePrueba.Matricula = "z17012931";
            alumnoDePrueba.Carrera = "Licenciatura en Ingeniera de Software";
            alumnoDePrueba.Contraseña = "ContraseñaChida123";
            alumnoDePrueba.EstadoAlumno = EstadoAlumno.Aceptado;
            alumnoDePrueba.Asignaciones = new List<Asignacion>();
            Asignacion asignacionAlumnoDePrueba = new Asignacion();
            asignacionAlumnoDePrueba.IDAsignacion = 1;
            alumnoDePrueba.Asignaciones.Add(asignacionAlumnoDePrueba);

            alumnoDePrueba.DarDeBaja();

            Assert.AreEqual<Alumno>(alumnoEsperado, alumnoDePrueba);
        }

        [TestMethod]
        public void ProbarAceptarAlumno()
        {

        }

        [TestMethod]
        public void ProbarRechazarAlumno()
        {

        }
    }
}
