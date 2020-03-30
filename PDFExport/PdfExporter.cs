using System;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using File = System.IO.File;

namespace PDFExport
{
    public class PdfExporter
    {
        public PdfExporter()
        {
            CreateDocument();
            resourceManager = new ResourceManager();
        }

        private FlowDocument _pdfDocument;
        private ResourceManager resourceManager;
        private void CreateDocument()
        {
            _pdfDocument = new FlowDocument
            {
                Margin = new Thickness(
                    Length.FromCentimeters(1.0),
                    Length.FromCentimeters(1.0),
                    Length.FromCentimeters(1.0),
                    Length.FromCentimeters(1.0)
                ),
                Align = Align.Justify
            };
        }

        public void SaveDocument(string path)
        {

            using (FileStream outPutStream = GetOutputStream(path))
            {
                try
                {
                    Section section = new Section();
                    //    //// add some text content
                    TextBlock text =
                        new TextBlock("Hessfjkljsdfsdfsdfsdcześć ty tyłek, przejdźdsfdfsdsfsdff你好你屁股jflksdfljeeeeeeeeehalloxydsfdfsdsfsdffjflksdfljeeeeeeeeehallo");
                    text.Display = Display.Block;
                    section.Add(text);
                    _pdfDocument.Add(section);
                    _pdfDocument.Write(outPutStream, resourceManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private FileStream GetOutputStream(string filePath)
        {
            return File.Create(filePath);
        }
    }
}
