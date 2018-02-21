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

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for CustomQuantityInputDialog.xaml
    /// </summary>
    public partial class CustomQuantityInputDialog : Window
    {
        public int numberOfItems { get; set; }
        public string toppings { get; set; }
        CustomDialogOK ohk;
        ErrorLogWriter logWriter = new ErrorLogWriter();

        public string cancelClicked { get; set; }
        public string typeOfFood { get; set; }

        public CustomQuantityInputDialog()
        {
            InitializeComponent();
        }


        private void btnCancelPrint_Click(object sender, RoutedEventArgs e)
        {
         
            Close();
           
        }


        public void SetNameOfItem(string nameOfItem)
        {
            lbMessageInput.Content = nameOfItem;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            lbSetMessage.Content = "Enter Number of items";
        }

      

        private void btnSubmit_Click_1(object sender, RoutedEventArgs e)
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
                        dialog2.SetMessage("Add Toppings on what you Ordered?");
                        dialog2.ShowDialog();
                        string yesOrNo = dialog2.YesOrNo;
                        if (yesOrNo.Equals("Yes"))
                        {
                            toppings = "+ Toppings";
                            Close();

                        }
                        else
                        {
                            Close();
                            //ConfirmOrder order = new ConfirmOrder();
                            //order.ShowDialog();

                        }
                    }
                    else
                    {
                        Close();
                    }
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
    
    }
}
