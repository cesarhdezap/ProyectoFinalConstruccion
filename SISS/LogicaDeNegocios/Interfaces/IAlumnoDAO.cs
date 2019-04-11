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
		Alumno CargarAlumnoPorMatricula(string matricula);
		List<Alumno> CargarAlumnosPorEstado(EestadoAlumno estadoAlumno);
		List<Alumno> CargarAlumnosTodos();
		DataTable AlumnoADataTable(Alumno alumno);
	}
}
