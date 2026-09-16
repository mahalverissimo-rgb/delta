using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace VillaBisutti.Delta
{
	public static class Util
	{
		public static string GetDescription(this Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute DescAttribute =
				field.GetCustomAttribute<DescriptionAttribute>();
			if (DescAttribute != null)
				return DescAttribute.Description;
			DisplayAttribute DisplayAttribute =
				field.GetCustomAttribute<DisplayAttribute>();
			if (DisplayAttribute != null)
				return DisplayAttribute.Name;
			return value.ToString();
		}
		public static bool IsIn(this string text, string value)
		{
			return (!(value == null)) && value.Replace(text, "") != value;
		}
		public static string ToIcon(this string value)
		{
			//|BE|BD|DR|DC|FV|MS|OI|SI|
			string retVal = "<span class=\"fa fa-{0}\"></span>";
			switch(value)
			{
				case "BE":
					return string.Format(retVal, "glass");
				case "BD":
					return string.Format(retVal, "birthday-cake");
				case "DR":
					return string.Format(retVal, "leaf");
				case "DC":
					return string.Format(retVal, "heart");
				case "FV":
					return string.Format(retVal, "camera-retro");
				case "MS":
					return string.Format(retVal, "trophy");
				case "OI":
					return string.Format(retVal, "gift");
				case "SI":
					return string.Format(retVal, "headphones");
				default:
					return string.Format(retVal, "question");
			}
		}
		public static T Get<T>(string name)
		{
			string config = ConfigurationManager.AppSettings[name];
			T value = default(T);
			if (!String.IsNullOrEmpty(config))
			{
				value = (T)Convert.ChangeType(config, typeof(T));
				return value;
			}
			return value;
		}
		public static List<T> GetList<T>(string name, char separator = ',')
		{
			string config = ConfigurationManager.AppSettings[name];
			List<T> value = new List<T>();
			if (!String.IsNullOrEmpty(config))
			{
				value = config.Split(separator).Cast<T>().ToList();
				return value;
			}
			return value;
		}
		public static string ToEscapedName(this string name)
		{
			name = name.ToLower();
			name = name.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u');
			name = name.Replace('à', 'a').Replace('è', 'e').Replace('ì', 'i').Replace('ò', 'o').Replace('ù', 'u');
			name = name.Replace('ä', 'a').Replace('ë', 'e').Replace('ï', 'i').Replace('ö', 'o').Replace('ü', 'u');
			name = name.Replace('â', 'a').Replace('ê', 'e').Replace('î', 'i').Replace('ô', 'o').Replace('û', 'u');
			name = name.Replace('ã', 'a').Replace('õ', 'o').Replace('ñ', 'n');
			name = name.Replace('ç', 'c');
			name = name.Replace('/', '-').Replace(' ', '-').Replace('\\', '-').Replace('|', '-');
			name = name.Replace(";", string.Empty).Replace(".", string.Empty).Replace(":", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty);
			name = name.Replace("{", string.Empty).Replace("}", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Replace("!", string.Empty);
			name = name.Replace("@", string.Empty).Replace("#", string.Empty).Replace("$", string.Empty).Replace("%", string.Empty).Replace("&", string.Empty);
			name = name.Replace("(", string.Empty).Replace(")", string.Empty).Replace("+", string.Empty).Replace("*", string.Empty).Replace("'", string.Empty);
			name = name.Replace("\"", string.Empty);
			return name;
		}
		public static int GetDaysInSpan(this DateTime begin, DateTime end)
		{
			int gt = int.Parse(end.ToString("yyyyMMdd"));
			int lt = int.Parse(begin.ToString("yyyyMMdd"));
			return gt - lt;
		}
		public static int GetDaysInSpan(this DateTime begin, int months)
		{
			return begin.GetDaysInSpan(begin.AddMonths(months));
		}
		public static string Pluralize(this string word, int quantity)
		{
			return word.Pluralize(quantity, word + "s");
		}
		public static string Pluralize(this string word, int quantity, string plural)
		{
			if (quantity == 1)
				return word;
			return plural;
		}
		public static string GetOSFileName(Core.Model.Evento evento, string tipo)
		{
			return HttpContext.Current.Server.MapPath(GetOSFileUrl(evento, tipo));
		}
		public static string GetOSFileUrl(Core.Model.Evento evento, string tipo)
		{
			string name = string.Format("{0}/{1}/{2}/{0}-{3}-{4}-{5}-{6}.pdf", 
				tipo, evento.Data.Year, evento.Data.Month, evento.Data.ToString("dd-MM-yyyy"),
				evento.Local.SiglaCasa, evento.NomeHomenageados.ToEscapedName(),
				evento.TipoEvento);
			if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/OS/" + tipo)))
				System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/OS/" + tipo));
			if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/OS/" + tipo + "/" + evento.Data.Year)))
				System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/OS/" + tipo + "/" + evento.Data.Year));
			if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/OS/" + tipo + "/" + evento.Data.Year + "/" + evento.Data.Month)))
				System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/OS/" + tipo + "/" + evento.Data.Year + "/" + evento.Data.Month));
			return "~/OS/" + name;
		}
		public static string GetImageName(string fileName)
		{
			string fileExtension = fileName.Split('.')[fileName.Split('.').Length - 1];
			return DateTime.Now.ToString("yyyyMMddhhmmss") + "." + fileExtension;
		}
		public static void HandleImage(HttpPostedFileBase URL, string path)
		{
			HandleImage(URL, path, true);
		}
		public static void HandleImage(HttpPostedFileBase URL, string path, bool resize)
		{
			Image image = Image.FromStream(URL.InputStream);
			if (resize)
			{
				System.Drawing.Bitmap imageResized = ResizeImage(image);
				imageResized.Save(path);
			}
			else
			{
				Image.FromStream(URL.InputStream).Save(path);
			}
		}
		private static Bitmap ResizeImage(Image image)
		{
			//TODO: Não redimensionar imagem menor que o padrão
			int defaultWidth = Get<int>("largura");
			int defaultHeight = Get<int>("altura");
			int width = defaultWidth;
			int height = defaultHeight;
			double x = (double)defaultWidth / (double)image.Width;
			double y = (double)defaultHeight / (double)image.Height;
			if (x < y)
				height = (int)(image.Height * x);
			else
				width = (int)(image.Width * y);
			var destRect = new Rectangle(0, 0, width, height);
			Bitmap destImage = new Bitmap(width, height);
			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				using (ImageAttributes wrapMode = new ImageAttributes())
				{
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
				}
				Rectangle rect = new Rectangle(0, height - 20, destImage.Width, destImage.Height);
				graphics.FillRectangle(Brushes.White, rect);
				graphics.DrawString(Get<string>("disclaimerImagem"), new Font("Helvetica", 11, FontStyle.Bold, GraphicsUnit.Pixel), new SolidBrush(Color.Black), rect);
				graphics.Flush();
			}
			return destImage;
		}
		private static Dictionary<Core.Model.LocalCerimonia, string> locaisCerimonia;
		public static Dictionary<Core.Model.LocalCerimonia, string> LocalCerimonia
		{
			get
			{
				if (locaisCerimonia == null)
				{
					locaisCerimonia = new Dictionary<Core.Model.LocalCerimonia, string>();
					foreach (Delta.Core.Model.LocalCerimonia item in Enum.GetValues(typeof(Delta.Core.Model.LocalCerimonia)).Cast<Delta.Core.Model.LocalCerimonia>())
						locaisCerimonia[item] = item.GetDescription();
				}
				return locaisCerimonia;
			}
		}
		private static Dictionary<Core.Model.TipoEvento, string> tiposEvento;
		public static Dictionary<Core.Model.TipoEvento, string> TiposEvento
		{
			get
			{
				if (tiposEvento == null)
				{
					tiposEvento = new Dictionary<Core.Model.TipoEvento, string>();
					foreach (Delta.Core.Model.TipoEvento item in Enum.GetValues(typeof(Delta.Core.Model.TipoEvento)).Cast<Delta.Core.Model.TipoEvento>())
						tiposEvento[item] = item.GetDescription();
				}
				return tiposEvento;
			}
		}
		public static Core.Data.Context context_;
		public static Core.Data.Context context
		{
			get
			{
				if (context_ == null)
					context_ = new Core.Data.Context();
				return context_;
			}
		}
		public static void ResetContext()
		{
			if (context_ == null)
				return;
			context_.Dispose();
			context_ = null;
		}
		public static string ReadFileEmail(string nomeArquivoEmail)
		{
			string message = string.Empty;
			string file = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)) + "\\Padrao Emails\\" + nomeArquivoEmail;
			if (File.Exists(file))
			{
				using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					using (StreamReader reader = new StreamReader(fileStream))
					{
						message = reader.ReadToEnd();
						reader.Close();
						reader.Dispose();
					}
					fileStream.Close();
					fileStream.Dispose();
				}
			}
			return message;
		}

		public static string TextoFornecimentoBisutti
		{
			get
			{
				return "Itens fornecidos pela Villa Bisutti";
			}
		}
		public static string TextoFornecimentoTerceiro
		{
			get
			{
				return "Itens a ser contratados pela Villa Bisutti";
			}
		}
		public static string TextoFornecimentoContratante
		{
			get
			{
				return "Itens disponibilizados pelo contratante";
			}
		}
		public static string WaterMark
		{ 
			get
			{
				return HttpContext.Current.Server.MapPath("~/Content/Images/bg-logo-villa-bisutti.png");
			}
		}
		public static string GenerateMailtoString(string unformattedList)
		{
			unformattedList = unformattedList.Replace(" ", string.Empty);
			unformattedList = unformattedList.Replace(';', ',');
			unformattedList = unformattedList.Replace('/', ',');
			return unformattedList;
		}
		public static bool CheckInUrl(string actionName)
		{
			string[] baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Split('/');
			return baseUrl[baseUrl.Length - 2].ToLower().Trim() != actionName.ToLower().Trim();
		}
		public static int[] ToIntArray(this string s, char separator)
		{
			if (s[0] == separator)
				s = s.Substring(1);
			if(s[s.Length - 1] == separator)
				s = s.Substring(0, s.Length - 1);
			return s.Split(separator).Select(i => int.Parse(i)).ToArray();

		}
		public static string TipoDocumentoOS { get { return "OS"; } }
		public static string TipoDocumentoDegustacao { get { return "Degustacao"; } }
		public static string TipoDocumentoCapa { get { return "Capa"; } }
	}
}
