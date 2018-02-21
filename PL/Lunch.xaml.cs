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
    /// Interaction logic for Lunch.xaml
    /// </summary>
    public partial class Lunch : Window
    {
        List<LunchClass> LunchMeals { get; set; }
        LunchClass LunchMeal { get; set; }
        public OrderBL orderBL;
        public MenuBL menuBL;
        ErrorLogWriter logWriter = new ErrorLogWriter();

        CustomDialogOK ohk;
        byte[] data;

        public Lunch()
        {
            InitializeComponent();
            LunchMeal = new LunchClass();
            LunchMeals = new List<LunchClass>();
            //orderBL = new OrderBL("MenuSystemSQLProvider");
            menuBL = new MenuBL("MenuSystemSQLProvider");
            ohk = new CustomDialogOK();
        }

        private void btnCancelLunch_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lstLunch_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LunchMeals = menuBL.SelectAllLunch();

                foreach (LunchClass lunchMeal in LunchMeals)
                {
                    lstLunch.Items.Add(lunchMeal);

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

        private void btnAddLunch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtMealLunch.Text.Length != 0 && txtPriceMealLunch.Text.Length != 0)
                {
                    LunchClass LunchClass = new LunchClass();
                    LunchClass.Name = txtMealLunch.Text;
                    LunchClass.Price = Convert.ToDouble(txtPriceMealLunch.Text);
                    LunchClass.Image = data;

                    int addMeal = menuBL.InsertLunch(LunchClass);
                    if (addMeal == 0)
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage(LunchClass.Name + " Added Successfully!");
                        ohk.Show();


                    }
                    else
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Could not Add " + LunchClass.Name);
                        ohk.Show();
                       
                    }
                }
                else
                {
                    ohk = new CustomDialogOK();
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

        private void btnDeleteLunch_Click(object sender, RoutedEventArgs e)
        {


            if (lstLunch.Items.Count > 0)
            {

                LunchMeal = new LunchClass();
                LunchMeal = lstLunch.SelectedItem as LunchClass;

                try
                {
                    int rc = menuBL.DeleteLunch(LunchMeal);

                    if (rc == 0 && LunchMeal != null)
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

        private void btnUpdateLunch_Click(object sender, RoutedEventArgs e)
        {
            if (txtMealLunch.Text.Length != 0 && txtPriceMealLunch.Text.Length != 0)
            {

                try
                {
                    if (lstLunch.Items.Count > 0)
                    {

                        LunchMeal = new LunchClass();
                        LunchMeal = lstLunch.SelectedItem as LunchClass;
                        LunchMeal.Id = LunchMeal.Id;
                        LunchMeal.Name = txtMealLunch.Text;
                        LunchMeal.Price = Convert.ToDouble(txtPriceMealLunch.Text);
                        LunchMeal.Image = data;
                        int rc = menuBL.UpdateLunch(LunchMeal);
                        if (rc == 0)
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(LunchMeal.Name + " " + "Edited successfully");
                            ohk.Show();
                          

                        }
                        else
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(LunchMeal.Name + " " + "Not edited" + "Please enter all fields");
                            ohk.Show();
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    logWriter.WriteToLog(ex.Message, ex);
                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter all fields or select a different image");
                    ohk.Show();
                  
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

        private void lstLunch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstLunch.Items.Count > 0)
                {
                    LunchMeal = new LunchClass();
                    LunchMeal = lstLunch.SelectedItem as LunchClass;
                    txtMealLunch.Text = LunchMeal.Name;
                    txtPriceMealLunch.Text = LunchMeal.Price.ToString();
                    MemoryStream ms = new MemoryStream();
                    ms.Write(LunchMeal.Image, 0, LunchMeal.Image.Length);
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

        private void btnOrderLunch_Click(object sender, RoutedEventArgs e)
        {
            MainFrameWindow.Content = new Food();
        }

        private void btnAddImageLunch_Click(object sender, RoutedEventArgs e)
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
