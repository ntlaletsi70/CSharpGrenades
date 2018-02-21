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
    /// Interaction logic for CustomDialogInputPin.xaml
    /// </summary>
    public partial class CustomDialogInputPin : Window
    {

        public string TopMessage { get; set; }

        public double getPin { get; set; }
        public string questionMessage { get; set; }

        public string Answer { get; set; }
        public CustomDialogInputPin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbSetMessage.Content = questionMessage;
            lbMessageInputPin.Content = TopMessage;
        }

        private void btnCancelPin_Click(object sender, RoutedEventArgs e)
        {
            Answer = "No";
            Close();
            return;
        }

        private void btnSubmitPin_Click(object sender, RoutedEventArgs e)
        {
            if (tbPin.Password.Length > 0)
            {

                Answer = "Yes";
                Close();
                return;
            }
            else
            {
                CustomDialogOK dialogOk = new CustomDialogOK();
                dialogOk.SetMessage("No Pin entered! Please enter a Pin");
                dialogOk.ShowDialog();

            }

        }
    }
}
