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
using MahApps.Metro.Controls;
using System.Windows.Threading;
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.BL;


namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public List<User> Users { get; set; }
        public bool IsTrue { get;set; }

        public UserBL userBl;
        LoginScreen screen;


        public MainWindow()
        {

            InitializeComponent();
            Users = new List<User>();
            userBl = new UserBL("SQLProvider");
            
            MainFrameWindow.Content = new Home();        
        }

        //public MainWindow(bool IsTrue)
        //{
        //    InitializeComponent();
        //    Users = new List<User>();
        //    userBl = new UserBL("SQLProvider");
        //    this.IsTrue = IsTrue;

        //    if (IsTrue)
        //    {
        //        MainFrameWindow.Content = new Food();
        //    }
        //    else
        //    {
        //        MainFrameWindow.Content = new Home();
        //    }

        //}

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainFrameWindow.Content = new Home();
        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2d);
            timer.Tick += TimerTick;
            timer.Start();
            //  appBar.IsOpen = true;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            // appBar.IsOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MainFrameWindow.Content = new Food();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            MainFrameWindow.Content = new Browser();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            MainFrameWindow.Content = new StudentManagement();
        }

        private void btDocumentation_Click(object sender, RoutedEventArgs e)
        {
            MainFrameWindow.Content = new Documentation();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            screen = new LoginScreen();
            screen.Show();
        }

        public void SetAdminView()
        {
            
        }

      

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnMaps_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrameWindow.Content = new Maps();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainFrameWindow.Content = new Logs();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainFrameWindow.Content = new UserGuide();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //screen = new LoginScreen();
            //IsTrue = screen.IsTrue;
            //if (IsTrue)
            //{
            //    MainFrameWindow.Content = new AdminPanel();
            //}
            //else
            //{
            //    //MainFrameWindow.Content = new AdminPanel();
            //}
        }
    }
}
