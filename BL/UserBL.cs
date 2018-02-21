using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.DAL;

namespace CSharpGrenadesGASource.BL
{
    public class UserBL
    {

        private ProviderBase providerBase;



        public UserBL(string Provider)
        {
            _SetupProviderBase(Provider);
        }

        public List<User> SelectAllUsers()
        {
            return providerBase.SelectAllUsers();
        }

        public int InsertUser(User User)
        {

            return providerBase.InsertUser(User);
        }
        private void _SetupProviderBase(string Provider)
        {
            //
            //Method Name : void _SetupProviderBase()
            //Purpose     : Helper method to select the correct data provider
            //Re-use      : None
            //Input       : string Provider
            //              - The name of the data provider to use
            //Output      : None
            //
            if (Provider.Equals("SQLProvider"))
            {
                providerBase = new SQLProvider();//set a new memory to the object,causes the PersonProviderBaseSQLite to execute
            }//end if


        }
    }
}
