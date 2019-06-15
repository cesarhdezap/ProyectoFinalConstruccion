using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using System.Collections.Generic;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocios
{
	public class Alumno : Persona
	{
		private const int MAXIMO_DE_ASIGNACIONES = 2;
		public string Matricula { get; set; }
		public string Carrera { get; set; }
		public string Contraseña { get; set; }
		public EstadoAlumno EstadoAlumno { get; set; }
		public Asignacion Asignacion { get; set; }	

        public Alumno ()
        {
            Matricula = string.Empty;
        }

        public void Guardar()
        {
			string contraseñaEncriptada = Servicios.ServiciosDeAutenticacion.EncriptarContraseña(Contraseña);
			Contraseña = contraseñaEncriptada;
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            alumnoDAO.GuardarAlumno(this);

        }

		public Asignacion CargarAsignacion()
		{
			AsignacionDAO asignacionDAO = new AsignacionDAO();
			Asignacion asignacion = new Asignacion();
			asignacion = asignacionDAO.CargarIDPorMatriculaDeAlumno(Matricula);
			asignacion = asignacionDAO.CargarAsignacionPorID(asignacion.IDAsignacion);
			asignacion.Alumno = this;
			return asignacion;
		}

        public bool Validar()
        {
            bool resultadoDeValidacion = false;
            if (ValidarContraseña(Contraseña) 
                && ValidarCorreoElectronico(CorreoElectronico) 
                && ValidarMatricula(Matricula) 
                && ValidarNombre(Nombre) 
                && ValidarTelefono(Telefono))
            {
                resultadoDeValidacion = true;
            }

            return resultadoDeValidacion;
        }

		public void DarDeBaja()
		{
			EstadoAlumno = EstadoAlumno.DadoDeBaja;
            ActualizarRegistroDeAlumno();
        }

		public void Aceptar()
		{
			EstadoAlumno = EstadoAlumno.Aceptado;
            ActualizarRegistroDeAlumno();
        }

		public void Rechazar()
		{
			EstadoAlumno = EstadoAlumno.Rechazado;
            ActualizarRegistroDeAlumno();
        }

        public void Solicitar()
        {
            EstadoAlumno = EstadoAlumno.EsperandoAsignacion;
            ActualizarRegistroDeAlumno();
        }

        public override string ToString()
        {
            
            string alumno = System.Environment.NewLine +
                            "Matricula: " + this.Matricula + System.Environment.NewLine +
                            "Nombre: " + this.Nombre + System.Environment.NewLine +
                            "Correo Electronico: " + this.CorreoElectronico + System.Environment.NewLine +
                            "Telefono: " + this.Telefono + System.Environment.NewLine +
                            "Estado: " + this.EstadoAlumno.ToString() + System.Environment.NewLine +
                            "Carrera" + this.Carrera;

            return alumno;
        }

        private void ActualizarRegistroDeAlumno()
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            alumnoDAO.ActualizarAlumnoPorMatricula(this.Matricula, this);
        }

		public void Asignar()
		{
			EstadoAlumno = EstadoAlumno.Asignado;
			ActualizarRegistroDeAlumno();
		}
	}

	public enum EstadoAlumno
	{
		EsperandoAceptacion, 
        Aceptado,
        Rechazado,
        EsperandoAsignacion,
        Asignado,
		Liberado,
		DadoDeBaja,
		
	}
}
