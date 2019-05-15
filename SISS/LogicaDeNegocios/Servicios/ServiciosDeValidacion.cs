using System.Text.RegularExpressions;

namespace LogicaDeNegocios.Servicios
{
	public class ServiciosDeValidacion
	{
		private static readonly Regex regexTelefono = new Regex(@"^(1\s*[-\/\.]?)?(\((\d{3})\)|(\d{3}))\s*[-\/\.]?\s*(\d{3})\s*[-\/\.]?\s*(\d{4})\s*(([xX]|[eE][xX][tT])\.?\s*(\d+))*$");
		private static readonly Regex regexCorreoElectronico = new Regex(@"^(\D)+(\w)*((\.(\w)+)?)+@(\D)+(\w)*((\.(\D)+(\w)*)+)?(\.)[a-z]{2,}$");
		private static readonly Regex regexNombre = new Regex(@"^[a-zA-Z àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]+$");
		private static readonly Regex regexMatricula = new Regex(@"^z[0-9]{8}$");

		public enum ResultadoDeValidacion
		{
			Valido,
			NoValido,
		}

		public static ResultadoDeValidacion ValidarCorreoElectronico(string correoElectronico)
		{
			ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

			if (regexCorreoElectronico.IsMatch(correoElectronico))
			{
				resultadoDeValidacion = ResultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static ResultadoDeValidacion ValidarTelefono(string telefono)
		{
			ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

			if (regexTelefono.IsMatch(telefono))
			{
				resultadoDeValidacion = ResultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static ResultadoDeValidacion ValidarNombre(string nombre)
		{
			ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

			if (regexNombre.IsMatch(nombre))
			{
				resultadoDeValidacion = ResultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static ResultadoDeValidacion ValidarMatricula(string matricula)
		{
			ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

			if (regexMatricula.IsMatch(matricula))
			{
				resultadoDeValidacion = ResultadoDeValidacion.Valido;
			}

			return resultadoDeValidacion;
		}
	}
}
