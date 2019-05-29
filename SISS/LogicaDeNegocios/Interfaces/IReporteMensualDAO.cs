﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Interfaces
{
	interface IReporteMensualDAO
	{
        void ActualizarReporteMensualPorID(int IDReporteMensual, ReporteMensual reporteMensual);
        List<ReporteMensual> CargarIDsPorIDAsignacion(int IDasignacion);
        ReporteMensual CargarReporteMensualPorID(int IDReporteMensual);
        int GuardarReporteMensual(ReporteMensual reporteMensual);

	}
}
