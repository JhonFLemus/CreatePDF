using System;
using System.Collections.Generic;
using System.Text;
using DinkToPdf;
using HtmlAgilityPack;

namespace CreatePDF
{
    // Test rama comparecientes
    public class Creator
    {
        public string CreatePDF(PlantillaParametros plantilla)
        {
            var convert = BasicConverterCustom.Instance;

            var plantillaHTML = new PlantillaParametros().Plantilla;


            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlNode nodo;

            html.LoadHtml(plantilla.Plantilla);

            var tablaComparecientes = html.DocumentNode.SelectSingleNode("//*[contains(@class,'comparecientes')]");

            var auxComparecientes = diligenciarPlantilla(tablaComparecientes.InnerHtml, plantilla.Comparecientes);

            nodo = HtmlAgilityPack.HtmlNode.CreateNode("<div>" + auxComparecientes + "</div>");

            tablaComparecientes.RemoveAllChildren();

            tablaComparecientes.PrependChild(nodo);

            plantillaHTML = html.DocumentNode.InnerHtml;


            foreach (var param in plantilla.Parametros)
            {
                if (plantillaHTML.Contains("[" + param.NombreCampo + "]"))
                {
                    plantillaHTML = plantillaHTML.Replace("[" + param.NombreCampo + "]", param.Valor);
                }
            }


            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.Letter,
                Margins = new MarginSettings { Top = 5, Left = 5, Right = 5, Bottom = 5 },
                DocumentTitle = "PDF Report",
                Out = @"D:\PDFCreator\Employee_Report.pdf"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = plantillaHTML,
                WebSettings = { DefaultEncoding = "utf-8" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };


            string pdfBase64 = Convert.ToBase64String(convert.Convert(pdf));

            return plantillaHTML;

        }

        public string diligenciarPlantilla(string plantilla, List<IEnumerable<Parametro>> parametros)
        {
            string aux = "";

            foreach (var parametro in parametros)
            {
                aux += plantilla;
                foreach (var param in parametro)
                {
                    if (aux.Contains("[" + param.NombreCampo + "]"))
                    {
                        aux = aux.Replace("[" + param.NombreCampo + "]", param.Valor);
                    }
                }
            }
            return aux;
        }
    }

    public class BasicConverterCustom 
    {
        private static BasicConverter _Instance;
        private BasicConverterCustom()
        {

        }

        public static BasicConverter Instance 
        {
            get
            {
                return _Instance = _Instance ?? new BasicConverter(new PdfTools());
            }
        }
    }
}
