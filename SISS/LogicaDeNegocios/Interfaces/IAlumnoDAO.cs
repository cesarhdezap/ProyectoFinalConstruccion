using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LogicaDeNegocios.Interfaces
{
	interface IAlumnoDAO
	{
		void GuardarAlumno(Alumno alumno);
		List<Alumno> CargarAlumnosTodos();
		Alumno CargarAlumnoPorMatricula(string matricula);
		List<Alumno> CargarAlumnosPorEstado(EEstadoAlumno estadoAlumno);
        List<Alumno> CargarAlumnosPorCorreo(string correo);
		void ActualizarAlumnoPorMatricula(string matricula, Alumno alumno);
		DataTable ConvertirAlumnoADataTable(Alumno alumno);
	}
}
