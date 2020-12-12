using System;
using System.Collections.Generic;
using System.Text;
using DinkToPdf;

namespace CreatePDF
{
    // Test rama git
    public class Creator
    {
        public string CreatePDF(PlantillaParametros plantilla)
        {
            var convert = BasicConverterCustom.Instance;

            var plantillaHTML = plantilla.Plantilla;

            foreach (var parametro in plantilla.Parametros)
            {
                if (plantillaHTML.Contains("[" + parametro.NombreCampo + "]"))
                {
                    plantillaHTML = plantillaHTML.Replace("[" + parametro.NombreCampo + "]", parametro.Valor);
                }
            }

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.Letter,
                Margins = new MarginSettings { Top = 10, Left = 10, Right = 10, Bottom = 10 },
                DocumentTitle = "PDF Report"
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


            string file = Convert.ToBase64String(convert.Convert(pdf));

            return file;

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
