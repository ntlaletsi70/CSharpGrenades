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

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for Food.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Lunch lunch = new Lunch();
            lunch.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Dinner dinner = new Dinner();
            dinner.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Beverage bev = new Beverage();
            bev.Show();
        }

        private void btnDinnerOpen_Click(object sender, RoutedEventArgs e)
        {
            Dinner dinner = new PL.Dinner();
            dinner.Show();

        }

        private void btnBeverageOpen_Click(object sender, RoutedEventArgs e)
        {
            Beverage bev = new Beverage();
            bev.Show();
        }

        private void btnLunchOpen_Click(object sender, RoutedEventArgs e)
        {
            Lunch lunch = new Lunch();
            lunch.Show();
        }

        private void btnLunchOpen_Click_1(object sender, RoutedEventArgs e)
        {
            Lunch lunch = new Lunch();
            lunch.Show();
        }

        private void btnBreakFastPage_Click(object sender, RoutedEventArgs e)
        {
            BreakFast1 breakfast = new PL.BreakFast1();
            breakfast.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrameWindow.Content = new Food();
        }
    }
}
