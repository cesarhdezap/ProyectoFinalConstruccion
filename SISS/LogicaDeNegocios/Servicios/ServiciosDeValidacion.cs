using System.Text.RegularExpressions;

namespace LogicaDeNegocios.Services
{
	public class ServiciosDeValidacion
	{
		private static readonly Regex regexTelefono = new Regex(@"^(1\s*[-\/\.]?)?(\((\d{3})\)|(\d{3}))\s*[-\/\.]?\s*(\d{3})\s*[-\/\.]?\s*(\d{4})\s*(([xX]|[eE][xX][tT])\.?\s*(\d+))*$");
		private static readonly Regex regexCorreoElectronico = new Regex(@"^(\D)+(\w)*((\.(\w)+)?)+@(\D)+(\w)*((\.(\D)+(\w)*)+)?(\.)[a-z]{2,}$");
		private static readonly Regex regexNombre = new Regex(@"^[a-zA-Z àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]+$");
		private static readonly Regex regexMatricula = new Regex(@"^z[0-9]{8}$");

		public enum EresultadoDeValidacion
		{
			Valido,
			NoValido,
		}

		public static EresultadoDeValidacion ValidarCorreoElectronico(string correoElectronico)
		{
			EresultadoDeValidacion resultadoDeValidacion = EresultadoDeValidacion.NoValido;

			if (regexCorreoElectronico.IsMatch(correoElectronico))
			{
				resultadoDeValidacion = EresultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static EresultadoDeValidacion ValidarTelefono(string telefono)
		{
			EresultadoDeValidacion resultadoDeValidacion = EresultadoDeValidacion.NoValido;

			if (regexTelefono.IsMatch(telefono))
			{
				resultadoDeValidacion = EresultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static EresultadoDeValidacion ValidarNombre(string nombre)
		{
			EresultadoDeValidacion resultadoDeValidacion = EresultadoDeValidacion.NoValido;

			if (regexNombre.IsMatch(nombre))
			{
				resultadoDeValidacion = EresultadoDeValidacion.Valido;
			}
			return resultadoDeValidacion;
		}

		public static EresultadoDeValidacion ValidarMatricula(string matricula)
		{
			EresultadoDeValidacion resultadoDeValidacion = EresultadoDeValidacion.NoValido;

			if (regexMatricula.IsMatch(matricula))
			{
				resultadoDeValidacion = EresultadoDeValidacion.Valido;
			}

			return resultadoDeValidacion;
		}
	}
}
