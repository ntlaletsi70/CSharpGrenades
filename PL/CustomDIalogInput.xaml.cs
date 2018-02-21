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
    /// Interaction logic for CustomDIalogInput.xaml
    /// </summary>
    public partial class CustomDIalogInput : Window
    {

        public string TopMessage { get; set; }

        public double getAmount { get; set; }
        public string questionMessage { get; set; }

        public string Answer { get; set; }

        public double differenceAmount { get; set; }
        public CustomDIalogInput()
        {
            InitializeComponent();

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            double moneyInput = Convert.ToDouble(tbAmount.Text);
            double totalAmount = getAmount;
            if (moneyInput > totalAmount)
            {
                double difference = moneyInput - totalAmount;
                Answer = "Yes";
                if (difference > 0)
                {
                    differenceAmount = difference;

                }

            }
            else
            {
                Answer = "Insuficient Amount";
            }
            this.Close();
            return;
        }

     

        private void btnCancelPrint_Click(object sender, RoutedEventArgs e)
        {
            Answer = "No";
            Close();
            return;
        }

        //function to ask for number of itms ordered


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            lbSetMessage.Content = questionMessage;
            lbSetMessage.Content = TopMessage;


        }
    }
}
