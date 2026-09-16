using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;

namespace VillaBisutti.Delta
{
	public static class Settings
	{
        //TODO: mudar para JSON
		public static int[] QuantidadesDiasAntesEvento 
		{ 
			get
			{
				return new int[] {17, 20, 30};
			}
		}
        //TODO: mudar para JSON
		public static int AcabouPrazoEvento
		{
			get
			{
				return 47;
			}
		}
        //TODO: mudar para JSON
		public static int EventosProximosDias
		{
			get
			{
				return 15;
			}
		}
        //TODO: mudar para JSON
		public static int EnviarEmailAposXDiasEvento 
		{
			get
			{
				return 5;
			}
		}
        
    
        //TODO: mudar para JSON
        //Email para envio do robo Contratação Serviço Terceiro
        public static List<String> EmailResponsavelTerceiro
        {
            get
            {
                return new List<string> {"talesdealmeida@gmail.com", "rafael.ravena@gmail.com"};
            }
        }
        //TODO: mudar para JSON
        //Nome da pessoa responsavel do robo Contratação Serviço Terceiro
		public static string  NomeResponsavel 
		{
			get
			{
                //TODO: tirar
				return "Biazinha Lindinha";
			}
		}

        public static String LoadTemplate
        {
            get
            {
                return ConfigurationManager.AppSettings["TimeToRun"].ToString();
            }
        }
	}
}
