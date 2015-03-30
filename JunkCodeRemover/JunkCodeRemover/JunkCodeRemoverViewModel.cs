using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMObjectLibrary;
using Ganss.XSS;
using System.Windows.Input;

namespace JunkCodeRemover
{
    public class JunkCodeRemoverViewModel : ViewModelBase 
    {
        private string _html;
        private ICommand _sanitizeCommand;
        private HtmlSanitizer _sanitizer;

        public JunkCodeRemoverViewModel()
        {
            _sanitizeCommand = new RelayCommand(Sanitize);
            _sanitizer = new HtmlSanitizer();
            _sanitizer.AllowedCssProperties.Remove("color");
            _sanitizer.AllowedCssProperties.Remove("font-family");
            _sanitizer.AllowedCssProperties.Remove("font-size");
            _sanitizer.AllowedCssProperties.Remove("line-height");
        }

        private void Sanitize(object obj)
        {           
            var sanitized = _sanitizer.Sanitize(_html);
            _html = sanitized;
            OnPropertyChanged("HTML");
        }

        public ICommand SanitizeCommand { get { return _sanitizeCommand; } }

        public string HTML 
        { 
            get { return _html; }
            set { _html = value; }
        }
    }
}
