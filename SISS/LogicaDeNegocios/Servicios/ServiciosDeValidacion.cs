using System.Text.RegularExpressions;

namespace LogicaDeNegocios.Servicios
{
	public class ServiciosDeValidacion
	{
		private static readonly Regex regexTelefono = new Regex(@"^(1\s*[-\/\.]?)?(\((\d{3})\)|(\d{3}))\s*[-\/\.]?\s*(\d{3})\s*[-\/\.]?\s*(\d{4})\s*(([xX]|[eE][xX][tT])\.?\s*(\d+))*$");
		private static readonly Regex regexCorreoElectronico = new Regex(@"^(\D)+(\w)*((\.(\w)+)?)+@(\D)+(\w)*((\.(\D)+(\w)*)+)?(\.)[a-z]{2,}$");
		private static readonly Regex regexNombre = new Regex(@"^[a-zA-Z àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]+$");
		private static readonly Regex regexMatricula = new Regex(@"^z[0-9]{8}$");
        private static readonly Regex regexContraseña = new Regex(@"^\S{6,255}$");
        

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
        
        public static ResultadoDeValidacion ValidarContraseña(string contraseña)
        {
            ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

            if (regexContraseña.IsMatch(contraseña))
            {
                resultadoDeValidacion = ResultadoDeValidacion.Valido;
            }

            return resultadoDeValidacion;
        }

        public static ResultadoDeValidacion ValidarAlumno(Alumno alumno)
        {
            ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

            if (ValidarContraseña(alumno.Contraseña) == ResultadoDeValidacion.Valido && ValidarCorreoElectronico(alumno.CorreoElectronico) == ResultadoDeValidacion.Valido && ValidarMatricula(alumno.Matricula) == ResultadoDeValidacion.Valido && ValidarNombre(alumno.Nombre) == ResultadoDeValidacion.Valido && ValidarTelefono(alumno.Telefono) == ResultadoDeValidacion.Valido)
            {
                resultadoDeValidacion = ResultadoDeValidacion.Valido;
            }

            return resultadoDeValidacion;
        }

        public static ResultadoDeValidacion ValidarCoordinador(DocenteAcademico docenteAcademico)
        {
            ResultadoDeValidacion resultadoDeValidacion = ResultadoDeValidacion.NoValido;

            if (ValidarContraseña(docenteAcademico.Contraseña) == ResultadoDeValidacion.Valido && ValidarCorreoElectronico(docenteAcademico.CorreoElectronico) == ResultadoDeValidacion.Valido && ValidarNombre(docenteAcademico.Nombre) == ResultadoDeValidacion.Valido && ValidarTelefono(docenteAcademico.Telefono) == ResultadoDeValidacion.Valido && docenteAcademico.Cubiculo > 0 && docenteAcademico.Coordinador == null)
            {
                resultadoDeValidacion = ResultadoDeValidacion.Valido;
            }

            return resultadoDeValidacion;
        }
	}
}
