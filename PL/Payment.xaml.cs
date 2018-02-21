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
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.BL;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CSharpGrenadesGASource.ErrorHandling;

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Page
    {

        public OrderBL orderBL;
        public MenuBL menuBL;

        OrderClass order { get; set; }
        List<OrderClass> orderList { get; set; }
        CustomDialogOK ohk;
        ErrorLogWriter logWriter = new ErrorLogWriter();


        public Payment()
        {
            InitializeComponent();
            order = new OrderClass();
            orderList = new List<OrderClass>();


            orderBL = new OrderBL("MenuSystemSQLProvider");
            menuBL = new MenuBL("MenuSystemSQLProvider");
            ohk = new CustomDialogOK();
        }

        private void btnCash_Click(object sender, RoutedEventArgs e)
        {
            SnackbarTwo.Message.Content = "Paid with Cash!";
            SnackbarTwo.IsActive = true;
            StartCloseTimer();

        }

   
        private void btnDebit_Click(object sender, RoutedEventArgs e)
        {
            Debit deb = new Debit();
            deb.Show();
        }

        private void btnCredit_Click(object sender, RoutedEventArgs e)
        {
            Credit cre = new Credit();
            cre.Show();
        }
        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5d);
            timer.Tick += TimerTick;
            timer.Start();

        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;

            SnackbarTwo.IsActive = false;
        }

        private void listOrders_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                //breakfast load data

                orderList = orderBL.SelectAllOrders();

                listOrders.Items.Clear();
                foreach (OrderClass order in orderList)
                {
                    listOrders.Items.Add(order);

                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
               
            }
        }
    }
}
