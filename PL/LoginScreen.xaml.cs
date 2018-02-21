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

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        List<User> Users { get; set; }
        User User { get; set; }
        public bool IsTrue { get; set; }
        UserBL UserBl { get; set; }
        CustomDialogOK ohk;
        ErrorLogWriter logWriter = new ErrorLogWriter();
        public LoginScreen()
        {
            InitializeComponent();
            Users = new List<User>();
            User = new User();
            UserBl = new UserBL("SQLProvider");
            ohk = new CustomDialogOK();
            Users = UserBl.SelectAllUsers();

            foreach (User users in Users)
            {

                if ((users.UserName == "admin" && users.Password == "1234"))
                {
                    IsTrue = true;

                    if (txtUserNameHome.Text == "admin" && txtUserPassword.Password == "1234")
                    {
                        IsTrue = true;
                    }
                }

            }

        }


 

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtUserNameHome.Text.Length != 0)
                {
                    IsTrue = false;

                    Users = UserBl.SelectAllUsers();

                    foreach (User users in Users)
                    {

                        if (users.UserName == txtUserNameHome.Text && users.Password == txtUserPassword.Password)
                        {
                            IsTrue = true;

                            if (txtUserNameHome.Text == "admin" && txtUserPassword.Password == "1234")
                            {
                                IsTrue = true;
                            }
                        }

                    }

                    if (IsTrue)
                    {

                        //MainWindow window = new MainWindow();
                        //window.Users = Users;
                        //window.Show();
                        //try to get the admin panel through main window after log in
                        AdminPanel panel = new AdminPanel();
                        panel.ShowDialog();
                        //main.IsTrue = IsTrue;

                        Close();

                    }
                    else
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("No such user found!");
                        ohk.Show();
                     
                    }
                }
                else
                {
                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Incorrect username or password");
                    ohk.Show();
                   
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

        private void btSignUp_Click(object sender, RoutedEventArgs e)
        {
            //Register reg = new Register();
            //reg.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
