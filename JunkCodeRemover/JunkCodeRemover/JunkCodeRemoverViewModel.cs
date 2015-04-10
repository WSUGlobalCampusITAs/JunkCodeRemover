using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMObjectLibrary;
using Ganss.XSS;
using System.Windows.Input;
using JunkCodeRemover.Properties;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace JunkCodeRemover
{
    public class JunkCodeRemoverViewModel : ViewModelBase 
    {
        private string _html;
        private ICommand _sanitizeCommand;
        private HtmlSanitizer _sanitizer;
        private Visibility _settingsViewVisibility;
        private ObservableCollection<TreeViewItem> _allowedTags;
        private ObservableCollection<TreeViewItem> _allowedStyles;
        private ObservableCollection<TreeViewItem> _allowedAttributes;

        public JunkCodeRemoverViewModel()
        {
            _sanitizeCommand = new RelayCommand(Sanitize);
            _sanitizer = new HtmlSanitizer();
            _sanitizer.AllowedTags.Clear();
            _sanitizer.AllowedCssProperties.Clear();
            _html = "Paste HTML Code Here";
            this.SettingsCommand = new RelayCommand(DisplaySettings);
            _settingsViewVisibility = Visibility.Hidden;
            _allowedTags = new ObservableCollection<TreeViewItem>();
            _allowedStyles = new ObservableCollection<TreeViewItem>();
            _allowedAttributes = new ObservableCollection<TreeViewItem>();

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

        public void DisplaySettings(object obj)
        {
            this._settingsViewVisibility = Visibility.Visible;
        }

        public Visibility SettingsViewVisibility { get { return this._settingsViewVisibility; } }

        public ICommand SanitizeCommand { get { return _sanitizeCommand; } }

        public ObservableCollection<TreeViewItem> AllowedTags { get { return _allowedTags; } }
        public ObservableCollection<TreeViewItem> AllowedAttributes { get { return _allowedAttributes; } }
        public ObservableCollection<TreeViewItem> AllowedStyles { get { return _allowedStyles; } }

        public string HTML 
        { 
            get { return _html; }
            set { _html = value; }
        }


    }
}
