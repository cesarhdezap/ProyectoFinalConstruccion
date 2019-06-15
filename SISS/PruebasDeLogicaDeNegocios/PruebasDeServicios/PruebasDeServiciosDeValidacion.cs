using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using LogicaDeNegocios.Servicios;

namespace Pruebas.PruebasDeServicios
{

    [TestClass]
    public class PruebasDeServiciosDeValidacion
    {
        [DataRow("Patchinster@Gmail.com")]
        [DataRow("Wenlock0999@gmail.om")]
        [DataRow("elChema@hotmail.com")]
        [DataRow("elrevo@gmail.com")]
        [DataRow("marilu@yahoo.com")]
        [DataRow("zS17012931@estudiantes.uv.com")]
        [DataTestMethod]
        public void ProbarValidarCorreoElectronicoValido_RegresaValido(string CorreoElectronico)
        {
            Assert.IsTrue(ValidarCorreoElectronico(CorreoElectronico));
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
            Assert.IsTrue(ValidarTelefono(Telefono));
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
            Assert.IsTrue(ValidarNombre(Nombre));
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
            Assert.IsTrue(ValidarMatricula(Matricula));
        }



        [DataRow("")]
        [DataTestMethod]
        public void ProbarValidarCorreoElectronicoVacio_RegresaNoValido(string CorreoElectronico)
        {
            
        }

        [DataRow("")]
        [DataTestMethod]
        public void ProbarValidarTelefonoVacio_RegresaNoValido(string Telefono)
        {
            Assert.IsFalse(ValidarTelefono(Telefono));
        }

        [DataRow("")]
        [DataTestMethod]
        public void ProbarValidarNombreVacio_RegresaNoValido(string Nombre)
        {
            Assert.IsFalse(ValidarNombre(Nombre));
        }
        [DataRow("")]
        [DataTestMethod]
        public void ProbarValidarMatriculaVacio_RegresaNoValido(string Matricula)
        {
            Assert.IsFalse(ValidarMatricula(Matricula));
        }
        [DataRow("aaaaaaaaaaaaaaaa@aaaaaaaaaaaaaaaaaaaaaa.aaa.aaa.aaa.aaa.aaa.aaa.aaa.aaa.aaa")]
        [DataRow("a@a.aa")]
        [DataTestMethod]
        public void ProbarValidarCorreoElectronicoDatosAlBorde_RegresaValido(string CorreoElectronico)
        {
            Assert.IsTrue(ValidarCorreoElectronico(CorreoElectronico));
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
            Assert.IsTrue(ValidarTelefono(Telefono));
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
            Assert.IsTrue(ValidarNombre(Nombre));
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
            Assert.IsTrue(ValidarMatricula(Matricula));
        }

        [DataTestMethod]
        [DataRow("email")]
        [DataRow("@gmail.com")]
        [DataRow("email@gmail")]
        [DataRow("email.com")]
        public void ProbarValidarCorreoElectronico_Incorrecto_RegresaNoValido(string correo)
        {
            Assert.IsFalse(ValidarCorreoElectronico(correo));
        }
        
        [DataTestMethod]
        [DataRow("2224")]
        [DataRow("222 333 111")]
        [DataRow("abc")]
        [DataRow("777777'111")]
        public void ProbarValidarTelefono_Incorrecto_RegresaNoValido(string telefono)
        {
            Assert.IsFalse(ValidarTelefono(telefono));
        }

        [DataTestMethod]
        [DataRow("Brhadaranyakopanishadvivekachudamani_Herrera_Carrasco")]
        [DataRow("Cesar21 Hernandez")]
        [DataRow("")]
        [DataRow("24534o")]
        public void ProbarValidarNombre_Incorrecto_RegresaNoValido(string nombre)
        {
            Assert.IsFalse(ValidarNombre(nombre));
        }

        [DataTestMethod]
        [DataRow("S29 05 62 17")]
        [DataRow("zS29056217")]
        [DataRow("")]
        [DataRow("290066217")]
        public void ProbarValidarMatricula_Incorrecto_RegresaNoValido(string matricula)
        {
            Assert.IsFalse(ValidarMatricula(matricula));
        }

        [DataTestMethod]
        [DataRow("cesarhdez@gmail.com")]
        [DataRow("chema@gmail.com")]
        [DataRow("joseph@correo.com")]
        [DataRow("julio@correo.com")]
        [DataRow("raul@correo.com")]

        public void ProbarValidarExistenciaDeCorreo_CorreoExistente_RegresaFalse(string correo)
        {
            bool resultadoDeValidacion;
            resultadoDeValidacion = ValidarDisponibilidadDeCorreo(correo);
            Assert.IsFalse(resultadoDeValidacion);
        }

		[DataTestMethod]
		[DataRow("999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999")]
		[DataRow("99999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999")]
		[DataRow("contraseña")]
		[DataRow("12345678900987654321")]
		[DataRow("pedro345")]

		public void ProbarValidarContraseña_Correcto_RegresaTrue(string correo)
		{
			bool resultadoDeValidacion;
			resultadoDeValidacion = ValidarDisponibilidadDeCorreo(correo);
			Assert.IsTrue(resultadoDeValidacion);
		}

	}
}
