using Highlight;
using Highlight.Engines;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;
using Xceed.Wpf.Toolkit;

namespace JunkCodeRemover
{

    /// <summary>
    /// Custom formatter for use with Extended WPF RichTextBox to allow sytax highlighting of html code. 
    /// </summary>
    class HtmlSyntaxFormatter : ITextFormatter
    {
        public string GetText(System.Windows.Documents.FlowDocument document)
        {
            return new TextRange(document.ContentStart, document.ContentEnd).Text;
        }

        public void SetText(System.Windows.Documents.FlowDocument document, string text)
        {
            string output = (new Highlighter(new RtfEngine())).Highlight("HTML", text);
            //The following set of code is to fix a issue regarding the html encoding done by the Highlighter. 

            output = output.Replace(@"&lt;", @"<");
            output = output.Replace(@"&gt;", @">");
            output = output.Replace(@"&quot;", "\"");
            while (output != output.Replace(@"&amp;", @"&"))
            {
                output = output.Replace(@"&amp;", @"&");
            }



            TextRange selection = new TextRange(document.ContentStart, document.ContentEnd);  
            
            if (selection.CanLoad(DataFormats.Rtf) && string.IsNullOrEmpty(output) == false)
            {
                // If so then load it with RTF, using UTF8 encoding to allow more variety of characters than ASCII
                byte[] valueArray = Encoding.UTF8.GetBytes(output);
                using (MemoryStream stream = new MemoryStream(valueArray))
                {
                    selection.Load(stream, DataFormats.Rtf);
                }
            }
        }
    }
}
