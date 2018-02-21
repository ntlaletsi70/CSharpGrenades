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
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    /// 

    public partial class CustomDialog : Window
    {
        public string Message { get; set; }
        public string YesOrNo { get; set; }
        public CustomDialog()
        {
            InitializeComponent();

        }

        private void btnNoDialog_Click(object sender, RoutedEventArgs e)
        {
            YesOrNo = "No";
            Close();
            //return;
        }

        private void btnYesDialog_Click(object sender, RoutedEventArgs e)
        {

            YesOrNo = "Yes";
            Close();
            //return;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbSetMessageDialog.Content = Message.ToString();
            
        }

        public void SetMessage(string message)
        {
            Message = message;
            lbSetMessageDialog.Content = Message;
           
        }
    }
}
