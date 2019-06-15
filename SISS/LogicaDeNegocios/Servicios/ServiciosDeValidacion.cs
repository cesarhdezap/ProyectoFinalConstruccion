using System.Text.RegularExpressions;
using System;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios.Servicios
{
    public class ServiciosDeValidacion
    {
        private static readonly Regex RegexTelefono = new Regex(@"^(1\s*[-\/\.]?)?(\((\d{3})\)|(\d{3}))\s*[-\/\.]?\s*(\d{3})\s*[-\/\.]?\s*(\d{4})\s*(([xX]|[eE][xX][tT])\.?\s*(\d+))*$");
        private static readonly Regex RegexCorreoElectronico = new Regex(@"^(\D)+(\w)*((\.(\w)+)?)+@(\D)+(\w)*((\.(\D)+(\w)*)+)?(\.)[a-z]{2,}$");
        private static readonly Regex RegexNombre = new Regex(@"^[a-zA-Z àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]+$");
        private static readonly Regex RegexMatricula = new Regex(@"^s[0-9]{8}$");
        private static readonly Regex RegexContraseña = new Regex(@"^\S{6,255}$");
        public const int TAMAÑO_MAXIMO_VARCHAR = 255;
		public const int VALOR_ENTERO_MINIMO_PERMITIDO = 0;
		private const int VALOR_ENTERO_MAXIMO_PERMITIDO = 255;

		public static bool ValidarCorreoElectronico(string correoElectronico)
		{
			bool resultadoDeValidacion = false;

			if (RegexCorreoElectronico.IsMatch(correoElectronico))
			{
				resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

		public static bool ValidarTelefono(string telefono)
		{
            bool resultadoDeValidacion = false;

			if (RegexTelefono.IsMatch(telefono))
			{
                resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

		public static bool ValidarNombre(string nombre)
		{
            bool resultadoDeValidacion = false;

			if (RegexNombre.IsMatch(nombre))
			{
                resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

		public static bool ValidarMatricula(string matricula)
		{
            bool resultadoDeValidacion = false;

			if (RegexMatricula.IsMatch(matricula))
			{
                resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}
        
        public static bool ValidarContraseña(string contraseña)
        {
            bool resultadoDeValidacion = false;

            if (RegexContraseña.IsMatch(contraseña))
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }

        public static bool ValidarCadena(string cadena)
        {
            bool resultadoDeValidacion = false;

            if (!string.IsNullOrEmpty(cadena) && cadena.Length < TAMAÑO_MAXIMO_VARCHAR)
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }

        public static bool ValidarExistenciaDeCorreo(string correoElectronico)
        {
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();

			bool resultadoDeValidacion = false;

			if (serviciosDeValidacionDAO.ContarOcurrenciasDeCorreo(correoElectronico) == 0)
			{
				resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

		public static bool ValidarExistenciaDeMatricula(string matricula)
		{
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();

			bool resultadoDeValidacion = false;

			if (serviciosDeValidacionDAO.ContarOcurrenciasDeMatricula(matricula) == 0)
			{
				resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

		public static bool ValidarEntero(string numeroEntero)
        {
            bool resultadoDeValidacion = false;

            if(Int32.TryParse(numeroEntero, out int numeroConvertido) && numeroConvertido > VALOR_ENTERO_MINIMO_PERMITIDO && numeroConvertido <= VALOR_ENTERO_MAXIMO_PERMITIDO)
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }
    }
}
