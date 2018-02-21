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
using System.Windows.Shapes;

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for CustomDialogOK.xaml
    /// </summary>
    public partial class CustomDialogOK : Window
    {
        public string MessageOK { get; set; }
        public CustomDialogOK()
        {
            InitializeComponent();
        }

        private void btnOkDialog_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SetMessage(string Message)
        {
            lbSetMessageDialogOK.Content = Message;
         
        }


    }
}
