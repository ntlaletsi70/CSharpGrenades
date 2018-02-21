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
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.BL;
using System.Windows.Threading;
using CSharpGrenadesGASource.ErrorHandling;

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for ConfirmOrder.xaml
    /// </summary>
    public partial class ConfirmOrder : Window
    {

        public OrderBL orderBL;
        public MenuBL menuBL;
        CustomDialogOK ohk;
        ErrorLogWriter logWriter = new ErrorLogWriter();

        OrderClass order { get; set; }
        List<OrderClass> orderList { get; set; }

        private double total = 0;
        private int quantity = 0;
        public ConfirmOrder()
        {
            InitializeComponent();
            order = new OrderClass();
            orderList = new List<OrderClass>();
            orderBL = new OrderBL("MenuSystemSQLProvider");
            menuBL = new MenuBL("MenuSystemSQLProvider");
        }

        private void btnCancelBeverage_Click(object sender, RoutedEventArgs e)
        {

            Close();
            return;

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                orderList = orderBL.SelectAllOrders();
                listOrdersComplete.Items.Clear();
                foreach (OrderClass order in orderList)
                {
                    listOrdersComplete.Items.Add(order);
                    total += order.Price;
                    quantity += order.NumberOfItems;
                }

                txtTotalOrder.Text = total.ToString("C");



                ImageSource imageSource = new BitmapImage(new Uri("Images/lunch.jpg", UriKind.Relative));

                imageLogo.Source = imageSource;

                Random rand = new Random();
                double number = rand.Next(1000000, 5000000);
                //token generation
                txtTokenNumber.Text = number.ToString();
                txtDate.Text = DateTime.Now.Date.ToString();
                txtPaymentType.Text = "Not yet Chosen";
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
            }

        }

        private void btnDebit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomDialogInputPin dialogInput = new CustomDialogInputPin();
                dialogInput.TopMessage = "Credit Card Number";
                dialogInput.questionMessage = "Please type in your Pin?";
                dialogInput.ShowDialog();
                string answer = dialogInput.Answer;
                if (answer == "Yes")
                {
                    CustomDialogOK dialogOk = new CustomDialogOK();
                    dialogOk.SetMessage("Pin Accepeted");
                    dialogOk.ShowDialog();
                    //CustomDialog yesOrnoDialog = new CustomDialog();
                    //yesOrnoDialog.Message = "Do you want to Print Slip?";
                    //yesOrnoDialog.ShowDialog();
                    //string printAnswer = yesOrnoDialog.YesOrNo;
                    //if (printAnswer == "Yes")
                    //{

                        txtPaymentType.Text = "Payed with Debit";
                        SnackbarConfirm.Message.Content = "Payed with Debit";
                        SnackbarConfirm.IsActive = true;
                        StartCloseTimer();

                    //{
                    PrintDialog printDialog = new PrintDialog();

                    //printDialog.ShowDialog();
                    printDialog.PrintVisual(Slip, "print customer slip");
                    // printDialog


                    DeleteDatabaseOrders();
                    Close();


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

        private void btnCredit_Click(object sender, RoutedEventArgs e)
        {
            //Credit account = new Credit();
            //account.Show();

            try
            {
                CustomDialogInputPin dialogInput = new CustomDialogInputPin();
                dialogInput.TopMessage = "Credit Card Number";
                dialogInput.questionMessage = "Please type in your Pin?";
                dialogInput.ShowDialog();
                string answer = dialogInput.Answer;
                if (answer == "Yes")
                {

                    CustomDialogOK dialogOk = new CustomDialogOK();
                    dialogOk.SetMessage("Pin Accepeted");
                    dialogOk.ShowDialog();
                    //CustomDialog yesOrnoDialog = new CustomDialog();
                    //yesOrnoDialog.Message = "Do you want to Print Slip?";
                    //yesOrnoDialog.ShowDialog();
                    //string printAnswer = yesOrnoDialog.YesOrNo;
                    //if (printAnswer == "Yes")
                    //{

                    txtPaymentType.Text = "Payed with Credit";
                    SnackbarConfirm.Message.Content = "Payed with Credit";
                    SnackbarConfirm.IsActive = true;
                        StartCloseTimer();
                    //{
                    PrintDialog printDialog = new PrintDialog();

                    //printDialog.ShowDialog();
                    printDialog.PrintVisual(Slip, "print customer slip");
                    // printDialog


                    DeleteDatabaseOrders();
                    Close();

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

        private void btnCash_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomDIalogInput money = new CustomDIalogInput();
                money.TopMessage = "Paying with Cash";
                money.questionMessage = "Please enter the amount?";
                money.getAmount = total;
                money.ShowDialog();
                string YesOrNo = money.Answer;
                if (YesOrNo == "Yes")
                {
                    txtPaymentType.Text = "Payed with Cash";
                    SnackbarConfirm.Message.Content = "Payed with cash";
                    SnackbarConfirm.IsActive = true;
                    StartCloseTimer();
                    double moneyBalance = money.differenceAmount;
                    CustomDialogOK dialogOk = new CustomDialogOK();
                    // dialogOk.MessageOK = "Your Change is :" + moneyBalance.ToString("C");
                    dialogOk.SetMessage("Your Change is :" + moneyBalance.ToString("C"));
                    dialogOk.ShowDialog();
                    //CustomDialog yesOrnoDialog = new CustomDialog();
                    //yesOrnoDialog.Message = "Do you want to print a slip?";
                    //yesOrnoDialog.ShowDialog();
                    //string answer = yesOrnoDialog.YesOrNo;


                    //if (answer == "Yes")
                    //{
                    PrintDialog printDialog = new PrintDialog();

                    //printDialog.ShowDialog();
                    printDialog.PrintVisual(Slip, "print customer slip");
                   // printDialog


                    DeleteDatabaseOrders();
                    Close();
                    //}
                    //else
                    //{

                   // SnackbarConfirm.Message.Content = "No Slip will Be Printed";
                    //SnackbarConfirm.IsActive = true;
                    //StartCloseTimer();
                    //DeleteDatabaseOrders();
                    //Close();

                }

                else
                {
                    if (YesOrNo == "Insuficient Amount")
                    {
                        CustomDialogOK dialog = new CustomDialogOK();
                        dialog.SetMessage("Insuficient Amount");
                        dialog.ShowDialog();

                    }
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(4d);
            timer.Tick += TimerTick;
            timer.Start();

        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;

            SnackbarConfirm.IsActive = false;
        }

        public void DeleteDatabaseOrders()
        {
            try
            {
                int rc = orderBL.DeleteAllOrders();


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

