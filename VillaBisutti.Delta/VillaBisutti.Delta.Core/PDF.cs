using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText = iTextSharp.text;
using iPdf = iTextSharp.text.pdf;
using System.Drawing.Imaging;
using System.Web;

namespace VillaBisutti.Delta.Core
{
	public class PDF : IDisposable
	{
		private int headingSize = 16;
		private int leadSize = 12;
		private int normalSize = 11;
		private int smallSize = 9;
		public void SetHeadingSize(int value)
		{
			headingSize = value;
		}
		public void SetLeadSize(int value)
		{
			leadSize = value;
		}
		public void SetNormalSize(int value)
		{
			normalSize = value;
		}
		public void SetSmallSize(int value)
		{
			smallSize = value;
		}
		string baseFont = "Verdana";
		public string FileName { get; set; }
		private FileStream _stream;
		private FileStream stream
		{
			get
			{
				if (_stream == null)
					_stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
				return _stream;
			}
		}
		private iText.Document _document;
		private iText.Document document
		{
			get
			{
				if (_document == null)
					_document = new iText.Document(iText.PageSize.A4);
				return _document;
			}
		}
		private iPdf.PdfWriter _writer;
		private iPdf.PdfWriter writer
		{
			get
			{
				if (_writer == null)
				{
					_writer = iPdf.PdfWriter.GetInstance(document, stream);
					_writer.PageEvent = new PDFWriterEvents();
				}
				return _writer;
			}
		}
		public PDF(string file)
		{
			FileName = file;
		}
		public void PrepareForWriting()
		{
			writer.Open();
			document.Open();
		}
		public void FinishWriting()
		{
			document.Close();
			document.Dispose();
			writer.Close();
			writer.Dispose();
			_document = null;
			_writer = null;
		}
		private iPdf.PdfPTable header;
		private iPdf.PdfPTable Header
		{
			get
			{
				if (header == null)
					throw new InvalidOperationException("Cabeçalho não definido");
				return header;
			}
		}
		private iText.Phrase MakePhrase(string lead, string text)
		{
			iText.Chunk leadChunk = new iText.Chunk(lead);
			leadChunk.Font = iText.FontFactory.GetFont(baseFont, normalSize, iText.Font.BOLD);
			iText.Chunk textChunck = new iText.Chunk(text);
			iText.Phrase phrase = new iText.Phrase();
			phrase.Add(leadChunk);
			phrase.Add(textChunck);
			return phrase;
		}
		public void SetHeader(DateTime data, string casa, string tipoEvento, string homenageados, int pax, string horario, string cerimonial, string produtor, string contatoProdutor, string assessoria, string contatoAssessoria, string responsavel, string contatoResponsavel, string perfil)
		{
			header = new iPdf.PdfPTable(3);
			header.WidthPercentage = 100F;
			header.SetWidths(new float[] { 1F, 1F, 1F });
			header.SpacingBefore = 10F;
			header.SpacingAfter = 10F;

			iText.Chunk headChunk = new iText.Chunk(
				string.Format("{0} > {1} > {2} > {3}", data.ToString("dd/MM/yyyy"), casa, tipoEvento, homenageados));
			headChunk.Font = iText.FontFactory.GetFont(baseFont, leadSize, iText.Font.BOLD);
			iPdf.PdfPCell headerCell = new iPdf.PdfPCell(new iText.Phrase(headChunk));
			headerCell.Colspan = 3;
			header.AddCell(headerCell);

			header.AddCell(new iPdf.PdfPCell(MakePhrase("Pax: ", string.Format("{0} +10% ({1})", pax, (int)(pax * 1.1)))));
			header.AddCell(new iPdf.PdfPCell(MakePhrase("Horário:", horario)));
			header.AddCell(new iPdf.PdfPCell(MakePhrase("Cerimonial: ", cerimonial)));

			iPdf.PdfPCell detailCell = new iPdf.PdfPCell(MakePhrase("Observações: ", perfil));
			detailCell.Rowspan = 3;
			header.AddCell(new iPdf.PdfPCell(detailCell));

			header.AddCell(new iPdf.PdfPCell(MakePhrase("Produtor(a): ", produtor)));
			header.AddCell(new iPdf.PdfPCell(MakePhrase("Contato: ", contatoProdutor)));

			header.AddCell(new iPdf.PdfPCell(MakePhrase("Assessoria: ", assessoria)));
			if (string.IsNullOrEmpty(contatoAssessoria))
				header.AddCell(new iPdf.PdfPCell(new iText.Phrase("")));
			else
				header.AddCell(new iPdf.PdfPCell(MakePhrase("Contato: ", contatoAssessoria)));

			header.AddCell(new iPdf.PdfPCell(MakePhrase("Responsável(a): ", responsavel)));
			header.AddCell(new iPdf.PdfPCell(MakePhrase("Contato: ", contatoResponsavel)));
		}
		public void AddHeader()
		{
			document.Add(Header);
		}
		public void AddHeaderText(string text)
		{
			iText.Chunk chunck = new iText.Chunk(text);
			chunck.Font = iText.FontFactory.GetFont(baseFont, headingSize, iText.Font.BOLD, iText.BaseColor.BLACK);
			iText.Paragraph paragraph = new iText.Paragraph(chunck);
			document.Add(paragraph);
		}
		public void AddLeadText(string text)
		{
			iText.Chunk chunck = new iText.Chunk(text);
			chunck.Font = iText.FontFactory.GetFont(baseFont, leadSize, iText.Font.BOLD, iText.BaseColor.BLACK);
			iText.Paragraph paragraph = new iText.Paragraph(chunck);
			document.Add(paragraph);
		}
		internal void AddItemLine(string item, string subItems)
		{
			iText.Chunk itemChunck = new iText.Chunk(item);
			itemChunck.Font = iText.FontFactory.GetFont(baseFont, leadSize, iText.Font.BOLD, iText.BaseColor.BLACK);
			iText.Chunk subItemsChunck = new iText.Chunk(subItems);
			subItemsChunck.Font = iText.FontFactory.GetFont(baseFont, normalSize, iText.Font.NORMAL, iText.BaseColor.BLACK);
			iText.Phrase phrase = new iText.Phrase();
			phrase.Add(itemChunck);
			phrase.Add(subItemsChunck);
			iText.Paragraph paragraph = new iText.Paragraph(phrase);
			document.Add(paragraph);
		}
		public void AddLineNoEmphasis(string text)
		{
			iText.Chunk chunck = new iText.Chunk(text);
			chunck.Font = iText.FontFactory.GetFont(baseFont, smallSize, iText.Font.NORMAL, iText.BaseColor.BLACK);
			iText.Paragraph paragraph = new iText.Paragraph(chunck);
			document.Add(paragraph);
		}
		public void AddLine(string text)
		{
			AddLine(text, false);
		}
		public void AddLine(string text, bool important)
		{
			iText.Chunk chunck = new iText.Chunk(text);
			if (important)
				chunck.Font = iText.FontFactory.GetFont(baseFont, leadSize, iText.Font.BOLD, iText.BaseColor.BLACK);
			else
				chunck.Font = iText.FontFactory.GetFont(baseFont, normalSize, iText.Font.NORMAL, iText.BaseColor.BLACK);
			iText.Paragraph paragraph = new iText.Paragraph(chunck);
			document.Add(paragraph);
		}
		public void AddImage(Dictionary<string, string> images)
		{
			//if (images.Count == 0)
			//	return;
			//iPdf.PdfPTable table = new iPdf.PdfPTable(2);
			//table.SplitLate = true;
			//table.KeepTogether = true;
			//table.WidthPercentage = 100F;
			//table.SetWidths(new float[] { 50F, 50F });
			//table.SpacingBefore = 10F;
			//table.SpacingAfter = 10F;
			//table.DefaultCell.Border = iText.Rectangle.NO_BORDER;
			//foreach(string path in images.Keys)
			//{
			//	iPdf.PdfPTable imageTable = new iPdf.PdfPTable(1);
			//	imageTable.WidthPercentage = 100F;
			//	imageTable.SetWidths(new float[] { 1F });
			//	imageTable.SpacingBefore = 2F;
			//	imageTable.SpacingAfter = 2F;
			//	imageTable.SplitLate = true;

			//	iText.Image image = iText.Image.GetInstance(path);
			//	image.Alignment = iText.Image.ALIGN_LEFT;
			//	iPdf.PdfPCell imageCell = new iPdf.PdfPCell(image, true);
			//	imageTable.AddCell(imageCell);

			//	iText.Chunk chunk = new iText.Chunk(images[path]);
			//	chunk.Font = iText.FontFactory.GetFont(baseFont, normalSize, iText.BaseColor.DARK_GRAY);
			//	iPdf.PdfPCell titleCell = new iPdf.PdfPCell(new iText.Phrase(chunk));
			//	imageTable.AddCell(titleCell);

			//	table.AddCell(imageTable);
			//}
			//table.AddCell("");
			//document.Add(table);
			if (images.Count == 0)
				return;
			foreach (string path in images.Keys)
			{
				iPdf.PdfPTable imageTable = new iPdf.PdfPTable(1);
				imageTable.WidthPercentage = 55F;
				imageTable.SetWidths(new float[] { 1F });
				imageTable.SpacingBefore = 0F;
				imageTable.SpacingAfter = 0F;
				imageTable.SplitLate = true;
				imageTable.KeepTogether = true;
				imageTable.DefaultCell.Border = iText.Rectangle.NO_BORDER;

				iText.Image image = iText.Image.GetInstance(path);
				image.Alignment = iText.Image.ALIGN_LEFT;
				iPdf.PdfPCell imageCell = new iPdf.PdfPCell(image, true);
				imageTable.AddCell(imageCell);

				iText.Chunk chunk = new iText.Chunk(images[path]);
				chunk.Font = iText.FontFactory.GetFont(baseFont, normalSize, iText.BaseColor.DARK_GRAY);
				iPdf.PdfPCell titleCell = new iPdf.PdfPCell(new iText.Phrase(chunk));
				imageTable.AddCell(titleCell);
				imageTable.AddCell(" ");
				document.Add(imageTable);
			}
		}
		public void AddBreakRule()
		{
			iPdf.draw.LineSeparator line = new iPdf.draw.LineSeparator(1F, 100F, iText.BaseColor.DARK_GRAY, iText.Element.ALIGN_CENTER, -8F);
			document.Add(line);
			AddLine(" ", false);
		}
		public void AddTable(List<CellRow> tableRows, float[] cellWidths)
		{
			iPdf.PdfPTable table = new iPdf.PdfPTable(cellWidths.Length);
			for (int i = 0; i < cellWidths.Length; i++)
				table.AbsoluteWidths[i] = cellWidths[i];
			table.WidthPercentage = 100F;
			table.SetWidths(cellWidths);
			table.SpacingBefore = 10F;
			table.SpacingAfter = 10F;
			table.SplitRows = false;
			table.SplitLate = false;
			table.DefaultCell.Border = 0; // 

			foreach (CellRow row in tableRows)
			{
				for (int i = 0; i < row.CellTexts.Length; i++)
				{
					iText.Chunk cellChunk = new iText.Chunk(row.CellTexts[i]);
					cellChunk.Font = iText.FontFactory.GetFont(baseFont, row.IsRowImportant ? leadSize : normalSize, row.CellBolds[i] ? iText.Font.BOLD : iText.Font.NORMAL);
					iPdf.PdfPCell cell = new iPdf.PdfPCell(new iText.Phrase(cellChunk));
					//cell.Width = (float)(cellWidths[i] * (document.PageSize.Width / 100));
					cell.Border = iText.Rectangle.NO_BORDER;
					cell.NoWrap = false;
					table.AddCell(cell);
				}
			}
			table.SetWidths(cellWidths);
			document.Add(table);
		}
		public void BreakLine()
		{
			AddLine("");
		}
		public void BreakPage()
		{
			document.NewPage();
		}
		public void Dispose()
		{
			FinishWriting();
		}
		public class CellRow
		{
			public bool IsRowImportant { get; set; }
			public string[] CellTexts { get; set; }
			public bool[] CellBolds { get; set; }
		}
	}
	internal class PDFWriterEvents : iPdf.IPdfPageEvent
	{
		public iText.Image waterMark
		{
			get
			{
				return iText.Image.GetInstance(Util.WaterMark);
			}
		}
		public void OnStartPage(iPdf.PdfWriter writer, iText.Document document)
		{
			iPdf.PdfContentByte under = writer.DirectContentUnder;
			under.BeginText();
			iText.Image imagem = waterMark;
			imagem.SetAbsolutePosition((float)((document.PageSize.Width - imagem.Width) / 2), ((float)(document.PageSize.Height - imagem.Height) / 2));
			under.AddImage(imagem);
			under.EndText();
		}
		public void OnChapter(iPdf.PdfWriter writer, iText.Document document, float paragraphPosition, iText.Paragraph title)
		{
		}
		public void OnChapterEnd(iPdf.PdfWriter writer, iText.Document document, float paragraphPosition)
		{
		}
		public void OnCloseDocument(iPdf.PdfWriter writer, iText.Document document)
		{
		}
		public void OnEndPage(iPdf.PdfWriter writer, iText.Document document)
		{
			iPdf.BaseFont helv = iPdf.BaseFont.CreateFont(iPdf.BaseFont.HELVETICA, iPdf.BaseFont.WINANSI, iPdf.BaseFont.NOT_EMBEDDED);
			iPdf.PdfContentByte cb = writer.DirectContent;
			int pageN = writer.PageNumber;
			string text = pageN.ToString();
			iText.Rectangle pageSize = document.PageSize;
			cb.SetRGBColorFill(100, 100, 100);
			cb.BeginText();
			cb.SetFontAndSize(helv, 12);
			cb.SetTextMatrix(document.LeftMargin, pageSize.GetBottom(document.BottomMargin));
			cb.ShowText(text);
			cb.EndText();
		}
		public void OnGenericTag(iPdf.PdfWriter writer, iText.Document document, iText.Rectangle rect, string text)
		{
		}
		public void OnOpenDocument(iPdf.PdfWriter writer, iText.Document document)
		{
		}
		public void OnParagraph(iPdf.PdfWriter writer, iText.Document document, float paragraphPosition)
		{
		}
		public void OnParagraphEnd(iPdf.PdfWriter writer, iText.Document document, float paragraphPosition)
		{
		}
		public void OnSection(iPdf.PdfWriter writer, iText.Document document, float paragraphPosition, int depth, iText.Paragraph title)
		{
		}
		public void OnSectionEnd(iPdf.PdfWriter writer, iText.Document document, float paragraphPosition)
		{
		}
	}
}
