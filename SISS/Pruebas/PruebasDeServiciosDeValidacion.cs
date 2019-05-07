using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicaDeNegocios.Servicios;

namespace Pruebas
{
    [TestClass]
    public class PruebaDeServiciosDeValidacion
    {
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

    }
}
