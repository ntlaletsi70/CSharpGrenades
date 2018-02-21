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
using System.IO;
using System.Drawing.Imaging;
using CSharpGrenadesGASource.ErrorHandling;

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for Beverage.xaml
    /// </summary>
    public partial class Beverage : Window
    {
        public OrderBL orderBL;
        public MenuBL menuBL;

        BeveragesClass Beverage1 { get; set; }
        List<BeveragesClass> Bevs { get; set; }
        ErrorLogWriter logWriter = new ErrorLogWriter();

        CustomDialogOK ohk;
        byte[] data;

        public Beverage()
        {
            InitializeComponent();
            Beverage1 = new BeveragesClass();
            Bevs = new List<BeveragesClass>();
            ohk = new CustomDialogOK();

           // orderBL = new OrderBL("MenuSystemSQLProvider");
            menuBL = new MenuBL("MenuSystemSQLProvider");
        }

        private void btnCancelLunch_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lstBeverage_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                //breakfast load data

                Bevs = menuBL.SelectAllBeverage();

                lstBeverage.Items.Clear();
                foreach (BeveragesClass bevs in Bevs)
                {
                    lstBeverage.Items.Add(bevs);

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

        private void btnAddBeverage_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txtMealBeverage.Text.Length != 0 && txtPriceMealBeverage.Text.Length != 0)
                {
                    BeveragesClass Bev = new BeveragesClass();
                    Bev.Name = txtMealBeverage.Text;
                    Bev.Price = Convert.ToDouble(txtPriceMealBeverage.Text);
                    Bev.Image = data;

                    int addMeal = menuBL.InsertBeverage(Bev);
                    if (addMeal == 0)
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage(Bev.Name + " Added Successfully!");
                        ohk.Show();
                 

                    }
                    else
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Could not Add " + Bev.Name);
                        ohk.Show();
                      
                    }
                }
                else
                {
                    ohk.SetMessage("Please enter all fields or " + "\n" + "select an item to add");
                    ohk.Show();
                    
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage("Please enter all fields or and Image");
                ohk.Show();
                
            }
        }

        private void btnDeleteBeverage_Click(object sender, RoutedEventArgs e)
        {
            if (lstBeverage.Items.Count > 0)
            {

                Beverage1 = new BeveragesClass();
                Beverage1 = lstBeverage.SelectedItem as BeveragesClass;

                try
                {
                    int rc = menuBL.DeleteBeverages(Beverage1);

                    if (rc == 0 && Beverage1 != null)
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
                catch (Exception ex)
                {
                    logWriter.WriteToLog(ex.Message, ex);
                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter select a meal to delete");
                    ohk.Show();
                   
                }

            }
        }

        private void btnUpdateBeverage_Click(object sender, RoutedEventArgs e)
        {



            if (txtMealBeverage.Text.Length != 0 && txtPriceMealBeverage.Text.Length != 0)
            {

                try
                {
                    if (lstBeverage.Items.Count > 0)
                    {

                        Beverage1 = new BeveragesClass();
                        Beverage1 = lstBeverage.SelectedItem as BeveragesClass;
                        Beverage1.Id = Beverage1.Id;
                        Beverage1.Name = txtMealBeverage.Text;
                        Beverage1.Price = Convert.ToDouble(txtPriceMealBeverage.Text);
                        Beverage1.Image = data;
                        int rc = menuBL.UpdateBeverage(Beverage1);
                        if (rc == 0)
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(Beverage1.Name + " " + "Edited successfully");
                            ohk.Show();
                          

                        }
                        else
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(Beverage1.Name + " " + "Not edited" + "Please enter all fields");
                            ohk.Show();
                        
                        }
                    }
                }
                catch (Exception ex)
                {
                    logWriter.WriteToLog(ex.Message, ex);
                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter all fields or select " + "\n" + " a different image");
                    ohk.Show();
                    
                }
            }
        }

        private void lstBeverage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (lstBeverage.Items.Count > 0)
                {
                    Beverage1 = new BeveragesClass();
                    Beverage1 = lstBeverage.SelectedItem as BeveragesClass;
                    txtMealBeverage.Text = Beverage1.Name;
                    txtPriceMealBeverage.Text = Beverage1.Price.ToString();
                    MemoryStream ms = new MemoryStream();
                    ms.Write(Beverage1.Image, 0, Beverage1.Image.Length);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream mss = new MemoryStream();
                    img.Save(mss, ImageFormat.Bmp);
                    mss.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = mss;
                    bi.EndInit();
                    lunch.Source = bi;
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                //Error.WriteLine(ex.Message);
                ohk.Show();
            }


        }

        private void btnOrderBeverage_Click(object sender, RoutedEventArgs e)
        {
            MainFrameWindow.Content = new Food();
        }
    

        private void btnAddImageBeverage_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog(); dlg.ShowDialog();
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length]; fs.Read(data, 0, System.Convert.ToInt32(fs.Length)); fs.Close();
                ImageSourceConverter imgs = new ImageSourceConverter();
                lunch.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));
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
