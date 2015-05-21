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
        string input;
        public string GetText(System.Windows.Documents.FlowDocument document)
        {
            return new TextRange(document.ContentStart, document.ContentEnd).Text;
        }

        public void SetText(System.Windows.Documents.FlowDocument document, string text)
        {
            string output = (new Highlighter(new RtfEngine())).Highlight("HTML", text);
            output = output.Replace(@"&lt;", @"<");
            output = output.Replace(@"&gt;", @">");
            output = output.Replace(@"&quot;", "\"");
            while (output != output.Replace(@"&amp;", @"&"))
            {
                output = output.Replace(@"&amp;", @"&");
            }
            output = output.Replace(@"“", "\"");
            output = output.Replace(@"”", "\"");
            output = output.Replace(@"’", "'");
            

            TextRange selection = new TextRange(document.ContentStart, document.ContentEnd);

            

            if (selection.CanLoad(DataFormats.Rtf) && string.IsNullOrEmpty(output) == false)
            {
                // If so then load it with RTF
                byte[] valueArray = Encoding.ASCII.GetBytes(output);
                using (MemoryStream stream = new MemoryStream(valueArray))
                {
                    selection.Load(stream, DataFormats.Rtf);
                }
            }
        }
    }
}
