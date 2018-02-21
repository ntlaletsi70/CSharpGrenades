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
using CSharpGrenadesGASource.ErrorHandling;
using System.IO;
using System.Drawing.Imaging;
using static System.Console;

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for BreakFast1.xaml
    /// </summary>
    public partial class BreakFast1 : Window
    {

        //private string _loggerPath = "c:\\DataStores\\CSharpGrenades";
       
        public OrderBL orderBL;
        public MenuBL menuBL;
        CustomDialogOK ohk;
        byte[] data;
        List<BreakfastClass> Breakfasts { get; set; }
        BreakfastClass BreakfastMeal { get; set; }
        OrderClass Order { get; set; }


        public BreakFast1()
        {
            InitializeComponent();

            Breakfasts = new List<BreakfastClass>();

           
            menuBL = new MenuBL("MenuSystemSQLProvider");
            ohk = new CustomDialogOK();
            //error handling

        }

        private void btnBreakFastCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lstBreakfast_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //breakfast load data

                Breakfasts = menuBL.SelectAllBreakFast();


                lstBreakfast.Items.Clear();
                foreach (BreakfastClass breakfast in Breakfasts)
                {
                    lstBreakfast.Items.Add(breakfast);

                }

            }
            catch (Exception ex)
            {
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
                

            }
        }

        private void btnCancelLunch_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void btnOrderBreakFast_Click(object sender, RoutedEventArgs e)
        {

            //dont forget quantity orderd and give access to pay and print
            //do this for all items in the menu
            //Food menu =  new Food();
            MainFrameWindow.Content = new Food();
        }

        private void btnAddBreakFast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtMealBreakfast.Text.Length != 0 && txtPriceMealBreakfast.Text.Length != 0)
                {
                    BreakfastClass Breakfast = new BreakfastClass();
                    Breakfast.Name = txtMealBreakfast.Text;
                    Breakfast.Price = Convert.ToDouble(txtPriceMealBreakfast.Text);
                    Breakfast.Image = data;


                    int addMeal = menuBL.InsertBreakfast(Breakfast);

                     if (addMeal == 0)
                    {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(Breakfast.Name + " Added Successfully!");
                            ohk.Show();
                         

                    }
                     else
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Could not Add " + Breakfast.Name);
                            ohk.Show();
                          
                    }
                }
               
                else
                {
                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter all fields or select an item to add");
                    ohk.Show();
                  
                }
            }
            catch (Exception)
            {
                ohk = new CustomDialogOK();
                ohk.SetMessage("Please enter all fields or and Image");
                ohk.Show();
                

            }
        }

        private void btnDeleteBreakFast_Click(object sender, RoutedEventArgs e)
        {
          
            if (lstBreakfast.Items.Count > 0)
            {

                BreakfastMeal = new BreakfastClass();
                BreakfastMeal = lstBreakfast.SelectedItem as BreakfastClass;

                try
                {
                    int rc = menuBL.DeleteBreakafast(BreakfastMeal);

                    if (rc == 0 && BreakfastMeal != null)
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Deleted Successfully!");
                        ohk.Show();

                    }
                    else
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Not Deleted!");
                        ohk.Show();
                    }
                }
                catch (Exception)
                {
                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter select a meal to delete");
                    ohk.Show();
                   
                }

            }
        }
        private void btnUpdateBreakFast_Click(object sender, RoutedEventArgs e)
        {
            if (txtMealBreakfast.Text.Length != 0 && txtPriceMealBreakfast.Text.Length != 0)
            {

                try
                {
                    if (lstBreakfast.Items.Count > 0)
                    {

                        BreakfastMeal = new BreakfastClass();
                        BreakfastMeal = lstBreakfast.SelectedItem as BreakfastClass;
                        BreakfastMeal.Id = BreakfastMeal.Id;
                        BreakfastMeal.Name = txtMealBreakfast.Text;
                        BreakfastMeal.Price = Convert.ToDouble(txtPriceMealBreakfast.Text);
                        BreakfastMeal.Image = data;
                        int rc = menuBL.UpdateBreakfast(BreakfastMeal);
                        if (rc == 0)
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(BreakfastMeal.Name + " " + "Edited successfully");
                            ohk.Show();
                       

                        }
                        else
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(BreakfastMeal.Name + " " + "Not edited" + "Please enter all fields");
                            ohk.Show();
                           
                        }
                    }
                }
                catch (Exception)
                {
                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter all fields");
                    ohk.Show();
                    Close();
                }

            }
            else
            {
                ohk = new CustomDialogOK();
                ohk.SetMessage("Please select an item to update or " + "\n" + 
                    "enter all fields");
                ohk.Show();
                
            }
        }

        private void lstBreakfast_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstBreakfast.Items.Count > 0)
                {
                    BreakfastMeal = new BreakfastClass();
                    BreakfastMeal = lstBreakfast.SelectedItem as BreakfastClass;
                    txtMealBreakfast.Text = BreakfastMeal.Name;
                    txtPriceMealBreakfast.Text = BreakfastMeal.Price.ToString();
                    MemoryStream ms = new MemoryStream();
                    ms.Write(BreakfastMeal.Image, 0, BreakfastMeal.Image.Length);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream mss = new MemoryStream();
                    img.Save(mss, ImageFormat.Bmp);
                    mss.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = mss;
                    bi.EndInit();
                    breakfastImage.Source = bi;
                }
            }
            catch (Exception ex)
            {
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
               
                ohk.Show();
            }
        }

        private void btnAddImageBreakFast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog(); dlg.ShowDialog();
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length]; fs.Read(data, 0, System.Convert.ToInt32(fs.Length)); fs.Close();
                ImageSourceConverter imgs = new ImageSourceConverter();
                breakfastImage.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));
            }
            catch (Exception ex)
            {

                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
            }          
        }     
    }
}


