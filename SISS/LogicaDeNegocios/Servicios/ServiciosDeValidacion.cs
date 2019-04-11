using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Services
{
	class ServiciosDeValidacion
	{
		private static readonly Regex regexTelefono = new Regex(@"^(1\s*[-\/\.]?)?(\((\d{3})\)|(\d{3}))\s*[-\/\.]?\s*(\d{3})\s*[-\/\.]?\s*(\d{4})\s*(([xX]|[eE][xX][tT])\.?\s*(\d+))*$");
		private static readonly Regex regexCorreoElectronico = new Regex(@"^(\D)+(\w)*((\.(\w)+)?)+@(\D)+(\w)*((\.(\D)+(\w)*)+)?(\.)[a-z]{2,}$");
		private static readonly Regex regexNombre = new Regex(@"/^[a - zA - ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]+$/u");
		private static readonly Regex regexMatricula = new Regex(@"^z[0-9]{8}$");

		public enum ResultadoDeValidacion
		{
			Valido = 1,
			NoValido = 0,
		}

		public static ResultadoDeValidacion ValidarCorreoElectronico(string CorreoElectronico)
		{
			ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

			if (regexCorreoElectronico.IsMatch(CorreoElectronico))
			{
				resultadoDeValidacion = ResultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static ResultadoDeValidacion ValidarTelefono(string Telefono)
		{
			ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

			if (regexTelefono.IsMatch(Telefono))
			{
				resultadoDeValidacion = ResultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static ResultadoDeValidacion ValidarNombre(string Nombre)
		{
			ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

			if (regexNombre.IsMatch(Nombre))
			{
				resultadoDeValidacion = ResultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static ResultadoDeValidacion ValidarMatricula(string Matricula)
		{
			ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

			if (regexMatricula.IsMatch(Matricula))
			{
				resultadoDeValidacion = ResultadoDeValidacion.Valido;
			}

			return resultadoDeValidacion;
		}
	}
}
