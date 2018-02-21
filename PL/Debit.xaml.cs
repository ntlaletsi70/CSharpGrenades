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
    /// Interaction logic for Debit.xaml
    /// </summary>
    public partial class Debit : Window
    {
        public Debit()
        {
            InitializeComponent();
        }

        private void btRegisterCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
