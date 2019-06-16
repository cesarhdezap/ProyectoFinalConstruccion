﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Querys
{
	public static class QuerysDeDirector
	{
		public const string CARGAR_ID_POR_CORREO = "SELECT IDPersonal FROM Directores WHERE CorreoElectronico = @CorreoElectronico";
		public const string CARGAR_DIRECTOR_POR_ID = "SELECT * FROM Directores WHERE IDPersonal = @IDPersonal";
	}
}
