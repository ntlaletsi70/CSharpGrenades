using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpGrenadesGASource.DAL;
using CSharpGrenadesGASource.BL.BusinessClasses;

namespace CSharpGrenadesGASource.BL
{
    public class OrderBL
    {
        private MenuProviderBase providerBase;

        public OrderBL(string Provider)
        {
            SetupProvider(Provider);
        }


        public int InsertOrder(OrderClass Order)
        {
            return providerBase.InsertOrders(Order);
        }

        public int DeleteOrder(OrderClass Order)
        {
            return providerBase.DeleteOrders(Order);
        }

        public int DeleteAllOrders()
        {
            return providerBase.DeleteAllOrders();
        }

        public int UpdateOrder(OrderClass Order)
        {
            return providerBase.UpdateOrders(Order);
        }

        public List<OrderClass> SelectAllOrders()
        {
            return providerBase.SelectAllOrders();
        }

        public void SetupProvider(string Provider)
        {
            if (Provider == "MenuSystemSQLProvider")
            {
                providerBase = new MenuSystemSQLProvider();
            }
        }
    }
}
