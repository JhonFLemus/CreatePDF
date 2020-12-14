using System;
using System.Collections.Generic;
using System.Text;
using DinkToPdf;

namespace CreatePDF
{
    // Test rama comparecientes
    public class Creator
    {
        public string CreatePDF(PlantillaParametros plantilla)
        {
            var convert = BasicConverterCustom.Instance;

            var plantillaHTML = new PlantillaParametros().Plantilla;

            foreach (var parametro in plantilla.Parametros)
            {
                plantillaHTML += plantilla.Plantilla;

                foreach (var par in parametro)
                {
                    if (plantillaHTML.Contains("[" + par.NombreCampo + "]"))
                    {
                        plantillaHTML = plantillaHTML.Replace("[" + par.NombreCampo + "]", par.Valor);
                    }
                }
            }

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.Letter,
                Margins = new MarginSettings { Top = 5, Left = 5, Right = 5, Bottom = 5 },
                DocumentTitle = "PDF Report",
                //Out = @"D:\PDFCreator\Employee_Report.pdf"
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

            return pdfBase64;

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
