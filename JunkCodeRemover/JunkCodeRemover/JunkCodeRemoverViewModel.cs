using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMObjectLibrary;
using System.Windows.Input;
using JunkCodeRemover.Properties;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using Westwind.Web.Utilities;
using System.Windows.Data;
using System.Xml;
using HtmlAgilityPack;
using Highlight;
using Highlight.Engines;
using System.Windows.Documents;
using System.IO;


namespace JunkCodeRemover
{
    public class JunkCodeRemoverViewModel : ViewModelBase 
    {
        private FlowDocument _html;
        private string _htmlsource;
        private ICommand _sanitizeCommand;
        private HtmlSanitizer _sanitizer;
        private Visibility _settingsViewVisibility;
        private ObservableCollection<CheckBox> _allowedTags;
        private ObservableCollection<CheckBox> _allowedStyles;
        private ObservableCollection<CheckBox> _allowedAttributes; 
        private ObservableCollection<CheckBox> _allowedHTMLProperties;
        private AllowedItemRepository _repository;
        private FlowDocumentConverter _converter;

        public JunkCodeRemoverViewModel()
        {
            
            _sanitizeCommand = new RelayCommand(Sanitize);
            _sanitizer = new HtmlSanitizer();
            _converter = new FlowDocumentConverter();
            _htmlsource = "Paste HTML Code Here";
            this.HTML = _converter.Convert(_htmlsource, _htmlsource.GetType(), null, System.Globalization.CultureInfo.DefaultThreadCurrentUICulture) as FlowDocument;
            this.SettingsCommand = new RelayCommand(DisplaySettings);
            _settingsViewVisibility = Visibility.Hidden;
            _allowedTags = new ObservableCollection<CheckBox>();
            _allowedStyles = new ObservableCollection<CheckBox>();
            _allowedAttributes = new ObservableCollection<CheckBox>();
            _allowedHTMLProperties = new ObservableCollection<CheckBox>();
            _repository = new AllowedItemRepository();
            
            
 
            foreach(AllowedItemModel item in _repository.Tags)
            {
                CheckBox cbItem = new CheckBox();
                cbItem.Content = item.ItemName;
                cbItem.IsChecked = item.IsChecked;
                cbItem.Foreground = Brushes.White;
                _allowedTags.Add(cbItem);
            }

            foreach (AllowedItemModel item in _repository.Styles)
            {
                CheckBox cbItem = new CheckBox();
                cbItem.Content = item.ItemName;
                cbItem.IsChecked = item.IsChecked;
                cbItem.Foreground = Brushes.White;
                _allowedStyles.Add(cbItem);
            }
            
            foreach (AllowedItemModel item in _repository.Attributes)
            {
                CheckBox cbItem = new CheckBox();
                cbItem.Content = item.ItemName;
                cbItem.IsChecked = item.IsChecked;
                cbItem.Foreground = Brushes.White;
                _allowedAttributes.Add(cbItem);
            }

        }

        /// <summary>
        /// Using the HtmlSanitizer class, it clears the undesired html code from the string. 
        /// </summary>
        /// <param name="obj"></param>
        private void Sanitize(object obj)
        {
            
            HashSet<string> blacklist = new HashSet<string>();
            
            _htmlsource = _converter.ConvertBack(HTML, HTML.GetType(), null, System.Globalization.CultureInfo.DefaultThreadCurrentCulture) as string;

            foreach (CheckBox item in _allowedTags)
            {
                if((bool)item.IsChecked)
                { 
                    blacklist.Add((string)item.Content);
                }
            }

            foreach (CheckBox item in _allowedStyles)
            {
                if ((bool)item.IsChecked)
                {
                    blacklist.Add((string)item.Content);
                }
            }

            foreach (CheckBox item in _allowedAttributes)
            {
                if ((bool)item.IsChecked)
                {
                    blacklist.Add((string)item.Content);
                }
            }

            _sanitizer.BlackList = blacklist;
            var sanitized = _sanitizer.Sanitize(_htmlsource);
            HTML = _converter.Convert(sanitized, HTML.GetType(), null, System.Globalization.CultureInfo.DefaultThreadCurrentUICulture) as FlowDocument;
           
        }

        public void DisplaySettings(object obj)
        {
            this._settingsViewVisibility = Visibility.Visible;
        }

        public Visibility SettingsViewVisibility { get { return this._settingsViewVisibility; } }

        public ICommand SanitizeCommand { get { return _sanitizeCommand; } }

        public ObservableCollection<CheckBox> AllowedTags { get { return _allowedTags; } }
        public ObservableCollection<CheckBox> AllowedAttributes { get { return _allowedAttributes; } }
        public ObservableCollection<CheckBox> AllowedStyles { get { return _allowedStyles; } }
        public ObservableCollection<CheckBox> AllowedHTMLProperties { get { return _allowedHTMLProperties; } }

        public FlowDocument HTML 
        { 
            get { return _html; }
            set 
            { 
                _html = value;
                OnPropertyChanged("HTML");
            }
        }

    }

        public class DivisionConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                double divideBy = double.Parse(parameter as string);
                double input = (double)value;
                return input / divideBy;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }

        public class FlowDocumentConverter : IValueConverter
        {
            public object Convert(object value, Type targetType,
                object parameter, System.Globalization.CultureInfo culture)
            {
                FlowDocument document = new FlowDocument();

                string s = value as string;
                if (s != null)
                {
                            var highlight = new Highlighter(new RtfEngine());
                            var highlighted = highlight.Highlight("HTML", s);
                    {
                    
                        document.SetValue(FlowDocument.TextAlignmentProperty, TextAlignment.Left);
                        TextRange content = new TextRange(document.ContentStart, document.ContentEnd);

                        if (content.CanLoad(DataFormats.Rtf) && string.IsNullOrEmpty(highlighted) == false)
                        {
                            // If so then load it with RTF
                            byte[] valueArray = Encoding.ASCII.GetBytes(highlighted);
                            using (MemoryStream stream = new MemoryStream(valueArray))
                            {
                                content.Load(stream, DataFormats.Rtf);
                            }
                        }
                    
                    }
                }
                return document;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                FlowDocument doc = value as FlowDocument;

                var tr = new TextRange(doc.ContentStart, doc.ContentEnd);

                return tr.Text;
            }
        }

    public class BindableRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(FlowDocument),
            typeof(BindableRichTextBox), new FrameworkPropertyMetadata
            (null, new PropertyChangedCallback(OnDocumentChanged)));

        public new FlowDocument Document
        {
            get
            {
                return (FlowDocument)this.GetValue(DocumentProperty);
            }

            set
            {
                this.SetValue(DocumentProperty, value);
            }
        }

        public static void OnDocumentChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs args)
        {
            RichTextBox rtb = (RichTextBox)obj;
            rtb.Document = (FlowDocument)args.NewValue;
        }
    }


}
