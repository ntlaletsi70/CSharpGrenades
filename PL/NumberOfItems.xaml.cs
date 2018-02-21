using CSharpGrenadesGASource.ErrorHandling;
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
using System.Windows.Threading;

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for NumberOfItems.xaml
    /// </summary>
    public partial class NumberOfItems : Window
    {

        public int numberOfItems { get; set; }
        public string toppings { get; set; }
        CustomDialogOK ohk;
        ErrorLogWriter logWriter = new ErrorLogWriter();

        public string cancelClicked { get; set; }
        public string typeOfFood { get; set; }
        public NumberOfItems()
        {
            InitializeComponent();
        }

        private void btnSubmitNoItems_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (txtNumberOfItems.Text.Length <= 0)
                {
                    CustomDialogOK dialog = new CustomDialogOK();
                    dialog.SetMessage("Please select an item to order");
                    dialog.ShowDialog();

                }
                else
                {
                    cancelClicked = "Yes";
                    int number = Convert.ToInt32(txtNumberOfItems.Text);
                    if (number == 0)
                    {
                        numberOfItems = 1;
                    }
                    else
                    {
                        numberOfItems = number;

                    }

                    if (typeOfFood != "Beverages")
                    {
                        CustomDialog dialog2 = new CustomDialog();
                        dialog2.Message = "Add Toppings on what you Ordered?";
                        dialog2.ShowDialog();
                        string yesOrNo = dialog2.YesOrNo;
                        if (yesOrNo.Equals("Yes"))
                        {
                            toppings = "+ Toppings";

                        }
                    }
                    Close();
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage("Please enter a whole number");
                ohk.Show();
            }
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

            SnackbarItems.IsActive = false;
        }

        private void btnCancelNoItems_Click(object sender, RoutedEventArgs e)
        {

            Close();
            cancelClicked = "No";
        }


    }
}
