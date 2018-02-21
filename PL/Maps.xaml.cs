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


namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for Maps.xaml
    /// </summary>
    public partial class Maps : Page
    {
        //private readonly GdsGoogleMap.GdsGoogleMap _gdsGoogleMap;

        CustomDialogOK ohk;
        public Maps()
        {


            InitializeComponent();
            //_gdsGoogleMap = new GdsGoogleMap.GdsGoogleMap();
            ohk = new CustomDialogOK();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string street = tbStreet.Text;
            string city = tbStreet.Text;
            string state = tbStreet.Text;
            string zip = tbStreet.Text;

            try
            {
                StringBuilder queryAddress = new StringBuilder();
                queryAddress.Append("http://maps.google.com/maps?q=");
                if (street != string.Empty)
                {

                    queryAddress.Append(street + "," + "+");
                }
                if (city != string.Empty)
                {

                    queryAddress.Append(city + "," + "+");
                }
                if (state != string.Empty)
                {

                    queryAddress.Append(state + "," + "+");
                }
                if (zip != string.Empty)
                {

                    queryAddress.Append(zip + "," + "+");
                }

                GooglemapsBrowser.Navigate(queryAddress.ToString());

            }
            catch (Exception ex)
            {
                ohk.SetMessage(ex.Message);
                ohk.Show();
            }
        }
    }
}
