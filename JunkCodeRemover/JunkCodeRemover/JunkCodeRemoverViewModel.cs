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
using System.Windows.Media;

namespace JunkCodeRemover
{
    public class JunkCodeRemoverViewModel : ViewModelBase 
    {
        private string _html;
        private ICommand _sanitizeCommand;
        private HtmlSanitizer _sanitizer;
        private Visibility _settingsViewVisibility;
        private ObservableCollection<CheckBox> _allowedTags;
        private ObservableCollection<CheckBox> _allowedStyles;
        private ObservableCollection<CheckBox> _allowedAttributes;
        private AllowedItemRepository _repository;

        public JunkCodeRemoverViewModel()
        {
            _sanitizeCommand = new RelayCommand(Sanitize);
            _sanitizer = new HtmlSanitizer();
            _sanitizer.AllowedTags.Clear();
            _sanitizer.AllowedCssProperties.Clear();
            _html = "Paste HTML Code Here";
            this.SettingsCommand = new RelayCommand(DisplaySettings);
            _settingsViewVisibility = Visibility.Hidden;
            _allowedTags = new ObservableCollection<CheckBox>();
            _allowedStyles = new ObservableCollection<CheckBox>();
            _allowedAttributes = new ObservableCollection<CheckBox>();
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

            _sanitizer.AllowedTags.Clear();
            _sanitizer.AllowedAttributes.Clear();
            _sanitizer.AllowedCssProperties.Clear();

            foreach (CheckBox item in _allowedTags)
            {
                if((bool)item.IsChecked)
                { 
                    _sanitizer.AllowedTags.Add((string)item.Content);
                }
            }

            foreach (CheckBox item in _allowedStyles)
            {
                if ((bool)item.IsChecked)
                {
                    _sanitizer.AllowedCssProperties.Add((string)item.Content);
                }
            }

            foreach (CheckBox item in _allowedAttributes)
            {
                if ((bool)item.IsChecked)
                {
                    _sanitizer.AllowedAttributes.Add((string)item.Content);
                }
            }

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

        public ObservableCollection<CheckBox> AllowedTags { get { return _allowedTags; } }
        public ObservableCollection<CheckBox> AllowedAttributes { get { return _allowedAttributes; } }
        public ObservableCollection<CheckBox> AllowedStyles { get { return _allowedStyles; } }

        public string HTML 
        { 
            get { return _html; }
            set { _html = value; }
        }


    }
}
