using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JunkCodeRemover
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class JunkCodeRemoverView : UserControl
    {
        public JunkCodeRemoverView()
        {
            InitializeComponent();
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.OnPropertyChanged(new DependencyPropertyChangedEventArgs(e.OriginalSource as DependencyProperty, e.OriginalSource as RichTextBox, e.Source as RichTextBo
            //MessageBox.Show("Hello World");
        }

        private void cbHTMLView_Checked(object sender, RoutedEventArgs e)
        {
            rtbhtml.Visibility = Visibility.Visible;
            
        }

        private void cbHTMLView_Unchecked(object sender, RoutedEventArgs e)
        {
            rtbhtml.Visibility = Visibility.Hidden;
            
        }
    }
}
