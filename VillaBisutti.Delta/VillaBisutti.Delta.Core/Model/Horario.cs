using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace VillaBisutti.Delta.Core.Model
{
	public class Horario
	{
		public string Horas
		{
			get
			{
				return Hora > 23 ? (Hora - 24).ToString().PadLeft(2, '0') : Hora.ToString().PadLeft(2, '0');
			}
		}
		public string Minutos
		{
			get
			{
				return Minuto.ToString().PadLeft(2, '0');
			}
		}
		[Display(Name = "Hora")]
		public int Hora { get; set; }
		[Display(Name = "Minuto")]
		public int Minuto { get; set; }
		internal static Horario Parse(int value)
		{
			Horario returnValue = new Horario();
			returnValue.Hora = (int)(value / 60);
			returnValue.Minuto = value - (returnValue.Hora * 60);
			return returnValue;
		}
		internal int ToInt()
		{
			return (Hora * 60) + Minuto;
		}
		public static Horario Parse(string value)
		{
			string[] args = value.Split(':');
			return new Horario { Hora = int.Parse(args[0]), Minuto = int.Parse(args[1]) };
		}
		public override string ToString()
		{
			return Horas + ":" + Minutos;
		}
		public void Span(int value)
		{
			Horario placeHolder = Horario.Parse(this.ToInt() + value);
			this.Hora = placeHolder.Hora;
			this.Minuto = placeHolder.Minuto;
		}
		public void Span(Horario value)
		{
			Span(value.ToInt());
		}
	}
}
