using System.Text.RegularExpressions;

namespace LogicaDeNegocios.Services
{
	class ServiciosDeValidacion
	{
		private static readonly Regex regexTelefono = new Regex(@"^(1\s*[-\/\.]?)?(\((\d{3})\)|(\d{3}))\s*[-\/\.]?\s*(\d{3})\s*[-\/\.]?\s*(\d{4})\s*(([xX]|[eE][xX][tT])\.?\s*(\d+))*$");
		private static readonly Regex regexCorreoElectronico = new Regex(@"^(\D)+(\w)*((\.(\w)+)?)+@(\D)+(\w)*((\.(\D)+(\w)*)+)?(\.)[a-z]{2,}$");
		private static readonly Regex regexNombre = new Regex(@"/^[a - zA - ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]+$/u");
		private static readonly Regex regexMatricula = new Regex(@"^z[0-9]{8}$");

		public enum EResultadoDeValidacion
		{
			Valido = 1,
			NoValido = 0,
		}

		public static EResultadoDeValidacion ValidarCorreoElectronico(string correoElectronico)
		{
			EResultadoDeValidacion resultadoDeValidacion = EResultadoDeValidacion.NoValido;

			if (regexCorreoElectronico.IsMatch(correoElectronico))
			{
				resultadoDeValidacion = EResultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static EResultadoDeValidacion ValidarTelefono(string telefono)
		{
			EResultadoDeValidacion resultadoDeValidacion = EResultadoDeValidacion.NoValido;

			if (regexTelefono.IsMatch(telefono))
			{
				resultadoDeValidacion = EResultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static EResultadoDeValidacion ValidarNombre(string nombre)
		{
			EResultadoDeValidacion resultadoDeValidacion = EResultadoDeValidacion.NoValido;

			if (regexNombre.IsMatch(nombre))
			{
				resultadoDeValidacion = EResultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static EResultadoDeValidacion ValidarMatricula(string matricula)
		{
			EResultadoDeValidacion resultadoDeValidacion = EResultadoDeValidacion.NoValido;

			if (regexMatricula.IsMatch(matricula))
			{
				resultadoDeValidacion = EResultadoDeValidacion.Valido;
			}

			return resultadoDeValidacion;
		}
	}
}
