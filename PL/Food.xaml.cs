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
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.BL;
using CSharpGrenadesGASource.ErrorHandling;
using System.Windows.Threading;
using System.IO;
using static System.Console;

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for Food.xaml
    /// </summary>
    public partial class Food : Page
    {

        //XML Logger Information:
            
        public OrderBL orderBL;
        public MenuBL menuBL;
        CustomDialogOK ohk;
        int number;
        string answerDialog;
        String selectedFood;

        BeveragesClass Beverage1 { get; set; }

        List<BeveragesClass> Bevs { get; set; }
        List<BreakfastClass> Breakfasts { get; set; }
        BreakfastClass BreakfastMeal { get; set; }
        ErrorLogWriter logWriter = new ErrorLogWriter();
        DinnerClass DinnerMeal { get; set; }
        List<DinnerClass> DinnerMeals { get; set; }

        List<LunchClass> LunchMeals { get; set; }
        LunchClass LunchMeal { get; set; }

        OrderClass order { get; set; }
        List<OrderClass> orderList { get; set; }



        public Food()
        {
            InitializeComponent();
            Beverage1 = new BeveragesClass();
            Bevs = new List<BeveragesClass>();

            Breakfasts = new List<BreakfastClass>();
            DinnerMeals = new List<DinnerClass>();
            DinnerMeal = new DinnerClass();

            LunchMeal = new LunchClass();
            LunchMeals = new List<LunchClass>();
            orderBL = new OrderBL("MenuSystemSQLProvider");
            menuBL = new MenuBL("MenuSystemSQLProvider");
            order = new OrderClass();
            orderList = new List<OrderClass>();

            listOrders.Items.Clear();

            //error handling


        }

        private void btnLunchOpen_Click(object sender, RoutedEventArgs e)
        {
            selectedFood = "Lunch";
            ImageSource imageSource = new BitmapImage(new Uri("Images/lunch.jpg", UriKind.Relative));

            ImageFood.Source = imageSource;
            txtSetNameFood.Text = "Available Lunch";
            try
            {
                LunchMeals = menuBL.SelectAllLunch();
                listFoodOrders.Items.Clear();
                foreach (LunchClass lunchMeal in LunchMeals)
                {
                    listFoodOrders.Items.Add(lunchMeal);

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


        private void btnDinnerOpen_Click_1(object sender, RoutedEventArgs e)
        {
            selectedFood = "Dinner";
            ImageSource imageSource = new BitmapImage(new Uri("Images/dinner.jpg", UriKind.Relative));

            ImageFood.Source = imageSource;
            txtSetNameFood.Text = "Available Dinner";
            try
            {
                //breakfast load data


                DinnerMeals = menuBL.SelectAllDinner();

                listFoodOrders.Items.Clear();
                foreach (DinnerClass dinnerMeal in DinnerMeals)
                {
                    listFoodOrders.Items.Add(dinnerMeal);

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

        private void btnBeverageOpen_Click_1(object sender, RoutedEventArgs e)
        {
            selectedFood = "Beverages";
            ImageSource imageSource = new BitmapImage(new Uri("Images/drinks.jpg", UriKind.Relative));

            ImageFood.Source = imageSource;
            txtSetNameFood.Text = "Available Beverages";
            try
            {
               
                Bevs = menuBL.SelectAllBeverage();

                listFoodOrders.Items.Clear();
                foreach (BeveragesClass bevs in Bevs)
                {
                    listFoodOrders.Items.Add(bevs);

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

        private void btnBreakFastOpen_Click(object sender, RoutedEventArgs e)
        {
            selectedFood = "BreakFast";
            ImageSource imageSource = new BitmapImage(new Uri("Images/breakfast.jpg", UriKind.Relative));

            ImageFood.Source = imageSource;
            txtSetNameFood.Text = "Available BreakFast";
            try
            {
                Breakfasts = menuBL.SelectAllBreakFast();


                listFoodOrders.Items.Clear();
                foreach (BreakfastClass breakfast in Breakfasts)
                {
                    listFoodOrders.Items.Add(breakfast);

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

        public void orderRefresh()
        {
            try
            {

                double total = 0;
                orderList = orderBL.SelectAllOrders();

                listOrders.Items.Clear();
                foreach (OrderClass order in orderList)
                {
                    listOrders.Items.Add(order);
                    total += order.Price;
                }

                //Next the reciept and order dialog
                lblTotal.Content = "Total: "+  total.ToString("C");
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
              
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

            SnackbarFood.IsActive = false;
        }
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {

            if (listOrders.SelectedItems.Count > 0)
            {
                order = new OrderClass();
                order = listOrders.SelectedItem as OrderClass;
                try
                {
                    int rc = orderBL.DeleteOrder(order);

                    if (rc == 0)
                    {

                        SnackbarFood.Message.Content = "Item deleted successfully";
                        SnackbarFood.IsActive = true;
                        StartCloseTimer();
                        orderRefresh();

                    }
                    else
                    {
                        SnackbarFood.Message.Content = "Item Not deleted successfully";
                        SnackbarFood.IsActive = true;
                        StartCloseTimer();

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
            else
            {
                SnackbarFood.Message.Content = "Please Choose an item to Remove";
                SnackbarFood.IsActive = true;
                StartCloseTimer();
            }
        }

        private void btnComfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            if (listOrders.Items.Count > 0)
            {

                ConfirmOrder order = new ConfirmOrder();
                order.ShowDialog();
                orderRefresh();
            }
            else
            {
                SnackbarFood.Message.Content = "Cannot Confirm Empty Order";
                SnackbarFood.IsActive = true;
                StartCloseTimer();

            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.btnLunchOpen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            orderRefresh();
        }

        private void listOrders_Loaded(object sender, RoutedEventArgs e)
        {
            listOrders.Items.Clear();
            orderRefresh();
        }



        private void listFoodOrders_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (selectedFood.Equals("Beverages"))
                {
                    if (listFoodOrders.SelectedItems.Count > 0)
                    {
                        Beverage1 = new BeveragesClass();
                        Beverage1 = listFoodOrders.SelectedItem as BeveragesClass;

                        CustomQuantityInputDialog quantity = new CustomQuantityInputDialog();
                        quantity.typeOfFood = "Beverages";
                        quantity.SetNameOfItem(Beverage1.Name);
                        quantity.ShowDialog();
                        number = quantity.numberOfItems;
                     
                        OrderClass BeverageOrdered = new OrderClass();
                        BeverageOrdered.Name = Beverage1.Name;
                        BeverageOrdered.NumberOfItems = number;
                        BeverageOrdered.Price = Convert.ToDouble(Beverage1.Price.ToString()) * number;



                        int addMealBeverage = orderBL.InsertOrder(BeverageOrdered);

                            if (addMealBeverage == 0)
                            {
                                SnackbarFood.Message.Content = "Item Ordered successfully";
                                listFoodOrders.SelectedIndex = -1;
                                SnackbarFood.IsActive = true;
                                StartCloseTimer();
                                orderRefresh();
                            }
                            else
                            {

                                SnackbarFood.Message.Content = "Item Not Ordered";
                                SnackbarFood.IsActive = true;
                                StartCloseTimer();
                            }
                        
                    }
                    else
                    {
                        if (listFoodOrders.SelectedIndex != -1)
                        {
                            SnackbarFood.Message.Content = "Please choose an item to order";
                            SnackbarFood.IsActive = true;
                            StartCloseTimer();
                        }
                    }
                }
                else if (selectedFood.Equals("Dinner"))
                {

                    if (listFoodOrders.SelectedItems.Count > 0)
                    {
                        DinnerMeal = new DinnerClass();
                        DinnerMeal = listFoodOrders.SelectedItem as DinnerClass;

                        CustomQuantityInputDialog quantity = new CustomQuantityInputDialog();
                        quantity.SetNameOfItem(DinnerMeal.Name);
                        quantity.ShowDialog();

                        string toppings = quantity.toppings;
                        string truth = "Yes";
                        double toppingPrice = 10;
                        number = quantity.numberOfItems;
                        if (number > 0 && truth != "No")
                        {
                            OrderClass DinnerfastOrdered = new OrderClass();
                            if (toppings != null)
                            {
                                DinnerfastOrdered.Name = DinnerMeal.Name + toppings;
                                DinnerfastOrdered.Price = Convert.ToDouble(DinnerMeal.Price.ToString()) * number + toppingPrice;
                            }
                            else
                            {
                                DinnerfastOrdered.Name = DinnerMeal.Name;
                                DinnerfastOrdered.Price = Convert.ToDouble(DinnerMeal.Price.ToString()) * number;
                            }
                            DinnerfastOrdered.NumberOfItems = number;
                           

                            int addMealDinner = orderBL.InsertOrder(DinnerfastOrdered);

                            if (addMealDinner == 0)
                            {
                                SnackbarFood.Message.Content = "Item Ordered successfully";
                                listFoodOrders.SelectedIndex = -1;
                                SnackbarFood.IsActive = true;
                                StartCloseTimer();
                                orderRefresh();
                            }
                            else
                            {
                                SnackbarFood.Message.Content = "Item Not Ordered";
                                SnackbarFood.IsActive = true;
                                StartCloseTimer();
                            }
                        }
                    }
                    else
                    {
                        if (listFoodOrders.SelectedIndex != -1)
                        {
                            SnackbarFood.Message.Content = "Please choose an item to order";
                            SnackbarFood.IsActive = true;
                            StartCloseTimer();
                        }
                    }


                }
                else if (selectedFood.Equals("Lunch"))
                {
                    if (listFoodOrders.SelectedItems.Count > 0)
                    {
                        LunchMeal = new LunchClass();
                        LunchMeal = listFoodOrders.SelectedItem as LunchClass;

                        CustomQuantityInputDialog quantity = new CustomQuantityInputDialog();
                        quantity.SetNameOfItem(LunchMeal.Name);
                        quantity.ShowDialog();

                        string toppings = quantity.toppings;
                        string truth = "Yes";
                        double toppingPrice = 10;
                        number = quantity.numberOfItems;

                        if (number > 0 && truth != "No")
                        {
                            OrderClass LunchOrdered = new OrderClass();
                            if (toppings != null)
                            {
                                LunchOrdered.Name = LunchMeal.Name + toppings;
                                LunchOrdered.Price = Convert.ToDouble(LunchMeal.Price.ToString()) * number + toppingPrice;
                            }
                            else
                            {
                                LunchOrdered.Name = LunchMeal.Name;
                                LunchOrdered.Price = Convert.ToDouble(LunchMeal.Price.ToString());
                            }
                            LunchOrdered.NumberOfItems = number;
                           

                            int addMealLunch = orderBL.InsertOrder(LunchOrdered);

                            if (addMealLunch == 0)
                            {
                                SnackbarFood.Message.Content = "Item Ordered successfully";
                                listFoodOrders.SelectedIndex = -1;
                                SnackbarFood.IsActive = true;
                                StartCloseTimer();
                                orderRefresh();
                            }
                            else
                            {
                                SnackbarFood.Message.Content = "Item Not Ordered";
                                SnackbarFood.IsActive = true;
                                StartCloseTimer();
                            }
                        }

                    }
                    else
                    {
                        if (listFoodOrders.SelectedIndex != -1)
                        {
                            SnackbarFood.Message.Content = "Please choose an item to order";
                            SnackbarFood.IsActive = true;
                            StartCloseTimer();
                        }
                    }

                }
                else if (selectedFood.Equals("BreakFast"))
                {
                    if (listFoodOrders.SelectedItems.Count > 0)
                    {
                        BreakfastMeal = new BreakfastClass();
                        BreakfastMeal = listFoodOrders.SelectedItem as BreakfastClass;
                       
                        CustomQuantityInputDialog quantity = new CustomQuantityInputDialog();
                        quantity.SetNameOfItem(BreakfastMeal.Name);
                        quantity.ShowDialog();
          
                        string toppings = quantity.toppings;
                        string truth = "Yes";
                        double toppingPrice = 10;
                        number = quantity.numberOfItems;
                       
                        if (number > 0 && truth != "No")
                        {
                            OrderClass BreakfastOrdered = new OrderClass();
                            if (toppings != null)
                            {
                                BreakfastOrdered.Name = BreakfastMeal.Name + toppings;
                                BreakfastOrdered.Price = Convert.ToDouble(BreakfastMeal.Price.ToString()) * number + toppingPrice;
                            }
                            else
                            {
                                BreakfastOrdered.Name = BreakfastMeal.Name;
                                BreakfastOrdered.Price = Convert.ToDouble(BreakfastMeal.Price.ToString()) * number;
                            }
                            BreakfastOrdered.NumberOfItems = number;
                          

                            int addMealBreakFast = orderBL.InsertOrder(BreakfastOrdered);

                            if (addMealBreakFast == 0)
                            {
                                SnackbarFood.Message.Content = "Item Ordered successfully";
                                listFoodOrders.SelectedIndex = -1;
                                SnackbarFood.IsActive = true;
                                StartCloseTimer();
                                orderRefresh();
                            }
                            else
                            {
                                SnackbarFood.Message.Content = "Item Not Ordered";
                                SnackbarFood.IsActive = true;
                                StartCloseTimer();
                            }
                        }
                    }
                    else
                    {
                        if (listFoodOrders.SelectedIndex != -1)
                        {
                            SnackbarFood.Message.Content = "Please choose an item to order";
                            SnackbarFood.IsActive = true;
                            StartCloseTimer();
                        }
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

        private void btnOrderChange_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (listOrders.Items.Count > 0)
                {
                    CustomDialog dialog = new CustomDialog();
                    dialog.Message = "Are you sure you want to Delete Order?";
                    dialog.ShowDialog();
                    answerDialog = dialog.YesOrNo;

                    if (answerDialog == "Yes")//////yesssss
                    {


                        int rc = orderBL.DeleteAllOrders();
                        orderRefresh();
                        SnackbarFood.Message.Content = "Order Cleared You can make new Order!";
                        SnackbarFood.IsActive = true;
                        StartCloseTimer();



                    }
                    else
                    if (answerDialog == "No")
                    {
                        SnackbarFood.Message.Content = "Your old Order is Still available!";
                        SnackbarFood.IsActive = true;
                        StartCloseTimer();
                    }///no end
                }
                else
                {
                    SnackbarFood.Message.Content = "No order Item Made Please make an Order";
                    SnackbarFood.IsActive = true;
                    StartCloseTimer();
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

        private void listFoodOrders_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (selectedFood.Equals("Beverages"))
            //{
            //    if (listFoodOrders.SelectedItems.Count > 0)
            //    {
            //        Beverage1 = new BeveragesClass();
            //        Beverage1 = listFoodOrders.SelectedItem as BeveragesClass;
            //        NumberOfItems items = new NumberOfItems();
            //        items.typeOfFood = "Beverages";
            //        items.ShowDialog();
            //        string truth = items.cancelClicked;
            //        number = items.numberOfItems;
            //        if (number > 0 && truth != "No")
            //        {
            //            OrderClass BeverageOrdered = new OrderClass();
            //            BeverageOrdered.Name = Beverage1.Name;
            //            BeverageOrdered.NumberOfItems = number;
            //            BeverageOrdered.Price = Convert.ToDouble(Beverage1.Price.ToString()) * number;

            //            int addMealBeverage = orderBL.InsertOrder(BeverageOrdered);

            //            if (addMealBeverage == 0)
            //            {
            //                SnackbarFood.Message.Content = "Item Ordered successfully";
            //                SnackbarFood.IsActive = true;
            //                StartCloseTimer();
            //                orderRefresh();

            //            }
            //            else
            //            {

            //                SnackbarFood.Message.Content = "Item Not Ordered successfully";
            //                SnackbarFood.IsActive = true;
            //                StartCloseTimer();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        SnackbarFood.Message.Content = "Please choose an item to order";
            //        SnackbarFood.IsActive = true;
            //        StartCloseTimer();

            //    }
            //}
            //else if (selectedFood.Equals("Dinner"))
            //{

            //    if (listFoodOrders.SelectedItems.Count > 0)
            //    {
            //        DinnerMeal = new DinnerClass();
            //        DinnerMeal = listFoodOrders.SelectedItem as DinnerClass;
            //        NumberOfItems items = new NumberOfItems();
            //        items.typeOfFood = "Dinner";
            //        items.ShowDialog();
            //        string truth = items.cancelClicked;
            //        string toppings = items.toppings;
            //        number = items.numberOfItems;
            //        if (number > 0 && truth != "No")
            //        {
            //            OrderClass DinnerfastOrdered = new OrderClass();
            //            if (toppings != null)
            //            {
            //                DinnerfastOrdered.Name = DinnerMeal.Name + toppings;
            //            }
            //            DinnerfastOrdered.NumberOfItems = number;
            //            DinnerfastOrdered.Price = Convert.ToDouble(DinnerMeal.Price.ToString()) * number;

            //            int addMealDinner = orderBL.InsertOrder(DinnerfastOrdered);

            //            if (addMealDinner == 0)
            //            {
            //                SnackbarFood.Message.Content = "Item Ordered successfully";
            //                SnackbarFood.IsActive = true;
            //                StartCloseTimer();
            //                orderRefresh();
            //            }
            //            else
            //            {
            //                SnackbarFood.Message.Content = "Item Not Ordered successfully";
            //                SnackbarFood.IsActive = true;
            //                StartCloseTimer();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        SnackbarFood.Message.Content = "Please choose an item to order";
            //        SnackbarFood.IsActive = true;
            //        StartCloseTimer();
            //    }


            //}
            //else if (selectedFood.Equals("Lunch"))
            //{
            //    if (listFoodOrders.SelectedItems.Count > 0)
            //    {
            //        LunchMeal = new LunchClass();
            //        LunchMeal = listFoodOrders.SelectedItem as LunchClass;
            //        NumberOfItems items = new NumberOfItems();
            //        items.typeOfFood = "Lunch";
            //        items.ShowDialog();
            //        string truth = items.cancelClicked;
            //        string toppings = items.toppings;
            //        number = items.numberOfItems;
            //        if (number > 0 && truth != "No")
            //        {
            //            OrderClass LunchOrdered = new OrderClass();
            //            if (toppings != null)
            //            {
            //                LunchOrdered.Name = LunchMeal.Name + toppings;
            //            }
            //            LunchOrdered.NumberOfItems = number;
            //            LunchOrdered.Price = Convert.ToDouble(LunchMeal.Price.ToString()) * number;

            //            int addMealLunch = orderBL.InsertOrder(LunchOrdered);

            //            if (addMealLunch == 0)
            //            {
            //                SnackbarFood.Message.Content = "Item Ordered successfully";
            //                SnackbarFood.IsActive = true;
            //                StartCloseTimer();
            //                orderRefresh();
            //            }
            //            else
            //            {
            //                SnackbarFood.Message.Content = "Item Not Ordered successfully";
            //                SnackbarFood.IsActive = true;
            //                StartCloseTimer();
            //            }
            //        }

            //    }
            //    else
            //    {
            //        SnackbarFood.Message.Content = "Please choose an item to order";
            //        SnackbarFood.IsActive = true;
            //        StartCloseTimer();
            //    }

            //}
            //else
            //{
            //    if (listFoodOrders.SelectedItems.Count > 0)
            //    {
            //        BreakfastMeal = new BreakfastClass();
            //        BreakfastMeal = listFoodOrders.SelectedItem as BreakfastClass;
            //        NumberOfItems items = new NumberOfItems();
            //        items.typeOfFood = "BreakFast";
            //        string toppings = items.toppings;
            //        items.ShowDialog();
            //        string truth = items.cancelClicked;
            //        number = items.numberOfItems;
            //        if (number > 0 && truth != "No")
            //        {
            //            OrderClass BreakfastOrdered = new OrderClass();
            //            if (toppings != null)
            //            {
            //                BreakfastOrdered.Name = BreakfastMeal.Name + toppings;
            //            }
            //            BreakfastOrdered.NumberOfItems = number;
            //            BreakfastOrdered.Price = Convert.ToDouble(BreakfastMeal.Price.ToString()) * number;

            //            int addMealBreakFast = orderBL.InsertOrder(BreakfastOrdered);

            //            if (addMealBreakFast == 0)
            //            {
            //                SnackbarFood.Message.Content = "Item Ordered successfully";
            //                SnackbarFood.IsActive = true;
            //                StartCloseTimer();
            //                orderRefresh();
            //            }
            //            else
            //            {
            //                SnackbarFood.Message.Content = "Item Not Ordered successfully";
            //                SnackbarFood.IsActive = true;
            //                StartCloseTimer();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        SnackbarFood.Message.Content = "Please choose an item to order";
            //        SnackbarFood.IsActive = true;
            //        StartCloseTimer();
            //    }
            //}


        }

        private void listFoodOrders_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btCloseOrder_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            
            MainWindow main = new MainWindow
                ();
            this.Visibility = Visibility.Collapsed;
            main.ShowDialog();
             
        }
    }
}
