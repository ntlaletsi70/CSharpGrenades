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
using System.IO;


namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for Documentation.xaml
    /// </summary>
    public partial class Documentation : Page
    {


        public Documentation()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDocs.SelectedIndex == 0)
            {

                browser.Navigate(new Uri("C:\\DataStores\\CSharpGrenades\\Documentation\\Intro Document.pdf"));
                browser.Visibility = Visibility.Visible;
                labelCaption.Content = "Intro Document.pdf";
            }
            else if (lstDocs.SelectedIndex == 1)
            {
                browser.Navigate(new Uri("C:\\DataStores\\CSharpGrenades\\Documentation\\REQUIMENTS AND SPECIFICATIONS.pdf"));
                labelCaption.Content = "REQUIMENTS AND SPECIFICATIONS.pdf";
            }
            else if (lstDocs.SelectedIndex == 2)
            {
                browser.Navigate(new Uri("C:\\DataStores\\CSharpGrenades\\Documentation\\Analysis and design.pdf"));
                labelCaption.Content = "Analysis.pdf";
            }
            else if (lstDocs.SelectedIndex == 3)
            {
                browser.Navigate(new Uri("C:\\DataStores\\CSharpGrenades\\Documentation\\UML DIAGRAMS.pdf"));
                labelCaption.Content = "UML DIAGRAMS.pdf";
            }
        }
    }
}
