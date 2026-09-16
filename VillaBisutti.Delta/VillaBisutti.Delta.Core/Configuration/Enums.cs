using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillaBisutti.Delta.Core.Configuration
{
	public enum Status
	{
		Rodando = 0,
		Erro = 1,
		Standby = 2,
		Parado = 3
	}
	public enum TipoIntervalo
	{
		Segundo = 0,
		Minuto = 1,
		Hora = 2,
		Dia = 3,
		Semana = 4,
		Mes = 5,
		Ano = 6
	}
}
