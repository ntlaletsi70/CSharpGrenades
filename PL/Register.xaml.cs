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


namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        UserBL UserBl { get; set; }
        CustomDialogOK ohk;
        public Register()
        {
            InitializeComponent();
            UserBl = new UserBL("SQLProvider");
            ohk = new CustomDialogOK();
        }

        private void btRegisterCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btRegisterSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtFirstName.Text.Length != 0 && txtLastName.Text.Length != 0 && txtPassword.Text.Length != 0 && txtReEnterPassword.Text.Length != 0 && txtUserName.Text.Length != 0)
                {
                    if (txtPassword.Text.Equals(txtReEnterPassword.Text))
                    {

                        User user = new User();
                        user.UserName = txtUserName.Text;
                        user.Firstname = txtFirstName.Text;
                        user.Lastname = txtLastName.Text;
                        user.Password = txtPassword.Text;
                        user.ConfirmPassword = txtReEnterPassword.Text;

                        int rc = UserBl.InsertUser(user);
                        if (rc == 0)
                        {
                            ohk.SetMessage("Registered Successfully");
                            ohk.Show();
                            txtFirstName.Text = null;
                            txtLastName.Text = null;
                            txtPassword.Text = null;
                            txtReEnterPassword.Text = null;
                            txtUserName.Text = null;
                            Close();
                        }
                        else
                        {
                            ohk.SetMessage("Cannot Register");
                            ohk.Show();
                            Close();

                        }
                    }
                    else
                    {

                        ohk.SetMessage("Incorrect username or password");
                        ohk.Show();
                        Close();
                    }
                }
                else
                {
                    ohk.SetMessage("Please enter all fields!");
                    ohk.Show();
                    Close();
                }
            }

            catch (Exception ex)
            {
                ohk.SetMessage(ex.Message);
                ohk.Show();
                Close();
            }

        }
    }
}
