using System.Text.RegularExpressions;
using System;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace LogicaDeNegocios.Servicios
{
    /// <summary>
    /// Clase para validar campos.
    /// Contiene métods para validar correo, telefono, nombre, matrícula, contraseña, cadena, entero,
    /// disponibilidad del correo y disponibilidad de la matrícula.
    /// </summary>
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

        /// <summary>
        /// Valida la estructura de la cadena del correo del usuario.
        /// </summary>
        /// <param name="correoElectronico">Correo del usuario.</param>
        /// <returns>Si la cadena cumple con la validación.</returns>
		public static bool ValidarCorreoElectronico(string correoElectronico)
		{
			bool resultadoDeValidacion = false;

			if (correoElectronico.Length <= TAMAÑO_MAXIMO_VARCHAR)
			{
				if (RegexCorreoElectronico.IsMatch(correoElectronico))
				{
					resultadoDeValidacion = true;
				}
			}

			return resultadoDeValidacion;
		}

        /// <summary>
        /// Valida la estructura de la cadena del telefono del usuario.
        /// </summary>
        /// <param name="telefono">Telefono del usuario.</param>
        /// <returns>Si la cadena cumple con la validación.</returns>
		public static bool ValidarTelefono(string telefono)
		{
            bool resultadoDeValidacion = false;

			if (RegexTelefono.IsMatch(telefono))
			{
                resultadoDeValidacion = true;
			}
            return resultadoDeValidacion;
			
		}

        /// <summary>
        /// Valida la estructura de la cadena del nombre del usuario.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <returns>Si la cadena cumple con la validación.</returns>
		public static bool ValidarNombre(string nombre)
		{
            bool resultadoDeValidacion = false;
			if (nombre.Length <= TAMAÑO_MAXIMO_VARCHAR)
			{
				if (RegexNombre.IsMatch(nombre))
				{
					resultadoDeValidacion = true;
				}
			}
			return resultadoDeValidacion;
		}

        /// <summary>
        /// Valida la estructura de la cadena de la matrícula del usuario.
        /// </summary>
        /// <param name="matricula">Matrícula del usuario.</param>
        /// <returns>Si la cadena cumple con la validación.</returns>
		public static bool ValidarMatricula(string matricula)
		{
            bool resultadoDeValidacion = false;

			if (RegexMatricula.IsMatch(matricula))
			{
                resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

        /// <summary>
        /// Valida la estructura de la cadena de la contraseña del usuario.
        /// </summary>
        /// <param name="contraseña">Contraseña del usuario.</param>
        /// <returns>Si la cadena cumple con la validación.</returns>
        public static bool ValidarContraseña(string contraseña)
        {
            bool resultadoDeValidacion = false;

            if (RegexContraseña.IsMatch(contraseña))
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }

        /// <summary>
        /// Valida una cadena para la entrada a la base de datos.
        /// </summary>
        /// <param name="cadena">Cadena de carácteres.</param>
        /// <returns>Si la cadena cumple con la validación.</returns>
        public static bool ValidarCadena(string cadena)
        {
            bool resultadoDeValidacion = false;

            if (!string.IsNullOrEmpty(cadena) && cadena.Length <= TAMAÑO_MAXIMO_VARCHAR)
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }

        /// <summary>
        /// Valida si el <paramref name="correoElectronico"/> esta disponible en la base de datos.
        /// </summary>
        /// <param name="correoElectronico">Correo del usuario.</param>
        /// <returns>Si el correo esta disponible.</returns>
        public static bool ValidarDisponibilidadDeCorreo(string correoElectronico)
        {
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();

			bool resultadoDeValidacion = false;

			if (serviciosDeValidacionDAO.ContarOcurrenciasDeCorreo(correoElectronico) == 0)
			{
				resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

        /// <summary>
        /// Valida si la <paramref name="matricula"/> esta disponible en la base de datos.
        /// </summary>
        /// <param name="matricula">Matrícula del usuario.</param>
        /// <returns>Si la matrícula esta disponible.</returns>
		public static bool ValidarDisponibilidadDeMatricula(string matricula)
		{
			ServiciosDeValidacionDAO serviciosDeValidacionDAO = new ServiciosDeValidacionDAO();

			bool resultadoDeValidacion = false;

			if (serviciosDeValidacionDAO.ContarOcurrenciasDeMatricula(matricula) == 0)
			{
				resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

        /// <summary>
        /// Valida si la cadena es convertible a entero y tiene la estructura para insertar a la base de datos.
        /// </summary>
        /// <param name="numeroEntero">Cadena con un numero.</param>
        /// <returns>Si la cadena es convertiblea entero.</returns>
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
