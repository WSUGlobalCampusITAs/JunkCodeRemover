using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMObjectLibrary;
using Ganss.XSS;
using System.Windows.Input;
using JunkCodeRemover.Properties;

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
            _sanitizer.AllowedTags.Clear();
            _sanitizer.AllowedCssProperties.Clear();
            _html = "Paste HTML Code Here";

            //using a string splitter variable to hold the tags after they 
            //are seperated from the Allowed Tags or Styles settings. 
            var splitstring = Settings.Default.AllowedTags.Split(',');

            foreach (string tag in splitstring)
            {
                _sanitizer.AllowedTags.Add(tag.Trim());
            }

            splitstring = Settings.Default.AllowedStyles.Split(',');

            foreach (string tag in splitstring)
            {
                _sanitizer.AllowedCssProperties.Add(tag.Trim());
            }

        }

        /// <summary>
        /// Using the HtmlSanitizer class, it clears the undesired html code from the string. 
        /// </summary>
        /// <param name="obj"></param>
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
