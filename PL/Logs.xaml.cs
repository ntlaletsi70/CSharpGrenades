using System;
using CSharpGrenadesGASource.BL;
using CSharpGrenadesGASource.BL.BusinessClasses;
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
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Page
    {

        private string path;
        List<Error> Errors { get; set; }
        private ErrorBL erroBL;
        private ErrorHandling.ErrorLogWriter logWriter;
        Error ErrorObject;

        public Logs()
        {
            InitializeComponent();

            Errors = new List<Error>();
            erroBL = new ErrorBL("ErrorXMLProvider");
            ErrorObject = new Error();
          

        }

        private void lstErrDateType_Loaded(object sender, RoutedEventArgs e)

        {
            logWriter = new ErrorHandling.ErrorLogWriter();
            path = "CSharpGrenades" + logWriter.LogFileName + logWriter.LogFileExtension;

            lstErrDateType.Items.Clear();
            Errors = erroBL.SelectAll("C:\\DataStores\\CSharpGrenades\\" + path, 0);
            foreach (Error error in Errors)
            {
                //ErrorObject.Date = error.Date;
                //ErrorObject.Type = error.Type;
                //ErrorObject.Description = error.Description;

                lstErrDateType.Items.Add(error);
            }
        }

        private void lstErrDateType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Error SelectedError = lstErrDateType.SelectedItem as Error; 
            lstErrorDesc.Items.Add(SelectedError);
           
        }
    }
}
