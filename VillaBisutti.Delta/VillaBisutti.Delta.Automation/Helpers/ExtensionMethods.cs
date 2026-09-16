using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Automation.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gets the date to 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateXML(string chaveAppConfig)
        {
            DateTime date;
            DateTime.TryParseExact(Util.Get<string>(chaveAppConfig), "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out date);
            return date;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ReturnTimeToRun(DateTime date)
        {
            long value = Convert.ToInt32(date.Subtract(DateTime.Now).TotalMilliseconds);
            if (value <= 0)
            {
               //Milisegundos menor que 0. Próxima Thread ocorrerá em 5 minutos.
                return 300000;
            }
            return value;
        }

        /// <summary>
        /// Change date to the next Execution
        /// </summary>
        public static void ModifyDate(string chaveAppConfig, string chaveFrequenciaAppConfig)
        {
            if (!string.IsNullOrEmpty(chaveAppConfig))
            {
                string completeDate = Util.Get<string>(chaveAppConfig);
                DateTime newDate = DateTime.ParseExact(completeDate, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                newDate = newDate.AddDays(Util.Get<int>(chaveFrequenciaAppConfig));

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[chaveAppConfig].Value = newDate.ToString("dd/MM/yyyy HH:mm:ss");
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings"); 
            }
        }
    }
}
