using System;
using System.Collections.Generic;
using CSharpGrenadesGASource.DAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpGrenadesGASource.BL.BusinessClasses;

namespace CSharpGrenadesGASource.BL
{
   public class ErrorBL
    {

        private  ErrorProviderBase providerBase;

        public ErrorBL(string Provider)
        {
            _SetupProviderBase(Provider);
        }

        public List<Error> SelectAll(string path)
        {
            return providerBase.SelectAll(path);
        }

        public List<Error> SelectAll(string path,int value)
        {
            return providerBase.SelectAll(path,value);
        }

        public int Insert(Error error)
        {
            return providerBase.Insert(error);
        }

        private void _SetupProviderBase(string provider)
        {
            //
            //Method Name : void _SetupProviderBase()
            //Purpose     : Helper method to select the correct data provider
            //Re-use      : None
            //Input       : string Provider
            //              - The name of the data provider to use
            //Output      : None
            //
            if (provider.Equals("ErrorXMLProvider"))
            {
                providerBase = new ErrorXMLProvider();//set a new memory to the object,causes the PersonProviderBaseSQLite to execute
            }//end if

        }
    }
}
