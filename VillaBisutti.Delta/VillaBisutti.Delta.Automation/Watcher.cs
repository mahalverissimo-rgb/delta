using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VillaBisutti.Delta.Automation.Helpers;
using VillaBisutti.Delta.Core.Configuration;

namespace VillaBisutti.Delta.Automation
{
	public abstract class Watcher : IWatcherBase
	{
		public int Intervalo { get; set; }
		public TipoIntervalo TipoIntervalo { get; set; }
		public bool On { get; set; }
		public DateTime UltimaExecucao { get; set; }
		public Status Status { get; set; }
		public Watcher()
		{
			Status = Status.Standby;
		}
		private int TotalInterval
		{
			get
			{
				int factor = 0;
				switch (TipoIntervalo)
				{
					case TipoIntervalo.Segundo:
						factor = 1000;
						break;
					case TipoIntervalo.Minuto:
						factor = 1000 * 60;
						break;
					case TipoIntervalo.Hora:
						factor = 1000 * 60 * 60;
						break;
					case TipoIntervalo.Dia:
						factor = 1000 * 60 * 60;
						break;
					case TipoIntervalo.Semana:
						factor = 1000 * 60 * 60 * 24 * 7;
						break;
					case TipoIntervalo.Mes:
						factor = 1000 * 60 * 60 * 24 * DateTime.Now.GetDaysInSpan(1);
						break;
					case TipoIntervalo.Ano:
						factor = 1000 * 60 * 60 * 24 * DateTime.Now.GetDaysInSpan(12);
						break;
				}
				return Intervalo * factor;
			}
		}
		/// <summary>
		/// Executa o processo em loop infinito
		/// </summary>
		public void Start()
		{
			GetConfig();
			if (On)
				try
				{
					Status = Status.Rodando;
					Execute();
					UltimaExecucao = DateTime.Now;
				}
				catch (Exception)
				{
					Status = Status.Erro;
					//Log exception for further evolution
				}
			else
				Status = Status.Parado;
			Thread.Sleep(TotalInterval);
			Start();
		}
		/// <summary>
		/// O processo que deve ser executado
		/// </summary>
		public abstract void Execute();
		/// <summary>
		/// Carrega em memória as configurações para a execução do processo
		/// </summary>
		public abstract void GetConfig();

	}
}
