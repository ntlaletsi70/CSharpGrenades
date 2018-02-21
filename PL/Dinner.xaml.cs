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
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.BL;
using System.Windows.Shapes;
using System.IO;
using System.Drawing.Imaging;
using CSharpGrenadesGASource.ErrorHandling;

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for Dinner.xaml
    /// </summary>
    public partial class Dinner : Window
    {
        public OrderBL orderBL;
        public MenuBL menuBL;
        DinnerClass DinnerMeal { get; set; }
        List<DinnerClass> DinnerMeals { get; set; }
        ErrorLogWriter logWriter = new ErrorLogWriter();
        CustomDialogOK ohk;
        byte[] data;


        public Dinner()
        {
            InitializeComponent();
            DinnerMeals = new List<DinnerClass>();
            DinnerMeal = new DinnerClass();
           // orderBL = new OrderBL("MenuSystemSQLProvider");
            menuBL = new MenuBL("MenuSystemSQLProvider");
            ohk = new CustomDialogOK();
        }

     
        private void lstDinner_Loaded(object sender, RoutedEventArgs e)
        {
        
            try
            {
               
                DinnerMeals = menuBL.SelectAllDinner();
                foreach (DinnerClass dinnerMeal in DinnerMeals)
                {
                    lstDinner.Items.Add(dinnerMeal);
                
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

        private void btnCancelDinner_Click_1(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void btnAddDinner_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txtMealDinner.Text.Length != 0 && txtPriceMealDinner.Text.Length != 0)
                {
                    DinnerClass DinnerClass = new DinnerClass();
                    DinnerClass.Name = txtMealDinner.Text;
                    DinnerClass.Price = Convert.ToDouble(txtPriceMealDinner.Text);
                    DinnerClass.Image = data;

                    int addMeal = menuBL.InsertDinner(DinnerClass);
                    if (addMeal == 0)
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage(DinnerClass.Name + " Added Successfully!");
                        ohk.Show();
                  

                    }
                    else
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Could not Add " + DinnerClass.Name);
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

        private void btnDeleteDinner_Click(object sender, RoutedEventArgs e)
        {


            if (lstDinner.Items.Count > 0)
            {

                DinnerMeal = new DinnerClass();
                DinnerMeal = lstDinner.SelectedItem as DinnerClass;

                try
                {
                    int rc = menuBL.DeleteDinner(DinnerMeal);

                    if (rc == 0 && DinnerMeal != null)
                    {
                        ohk.SetMessage("Deleted Successfully!");
                        ohk.Show();
                        Close();

                    }
                    else
                    {

                        ohk.SetMessage("Not Deleted!");
                        ohk.Show();
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    logWriter.WriteToLog(ex.Message, ex);
                    ohk.SetMessage("Please enter select a meal to delete");
                    ohk.Show();
                    Close();
                }
            }
        }

        private void btnUpdateDinner_Click(object sender, RoutedEventArgs e)
        {
            if (txtMealDinner.Text.Length != 0 && txtPriceMealDinner.Text.Length != 0)
            {

                try
                {
                    if (lstDinner.Items.Count > 0)
                    {

                        DinnerMeal = new DinnerClass();
                        DinnerMeal = lstDinner.SelectedItem as DinnerClass;
                        DinnerMeal.Id = DinnerMeal.Id;
                        DinnerMeal.Name = txtMealDinner.Text;
                        DinnerMeal.Price = Convert.ToDouble(txtPriceMealDinner.Text);
                        DinnerMeal.Image = data;
                        int rc = menuBL.UpdateDinner(DinnerMeal);
                        if (rc == 0)
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(DinnerMeal.Name + " " + "Edited successfully");
                            ohk.Show();
                          
                        }
                        else
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(DinnerMeal.Name + " " + "Not edited" + "Please enter all fields");
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

        private void lstDinner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (lstDinner.Items.Count > 0)
                {
                    DinnerMeal = new DinnerClass();
                    DinnerMeal = lstDinner.SelectedItem as DinnerClass;
                    txtMealDinner.Text = DinnerMeal.Name;
                    txtPriceMealDinner.Text = DinnerMeal.Price.ToString();
                    MemoryStream ms = new MemoryStream();
                    ms.Write(DinnerMeal.Image, 0, DinnerMeal.Image.Length);
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

        private void btnAddImageDinner_Click(object sender, RoutedEventArgs e)
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
