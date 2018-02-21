using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpGrenadesGASource.BL.BusinessClasses;
using System.Threading.Tasks;

namespace CSharpGrenadesGASource.DAL
{
    public abstract class MenuProviderBase
    {
        public abstract int InsertBreakafast(BreakfastClass breakfast);
        public abstract int InsertLunch(LunchClass lunch);
        public abstract int InsertDinner(DinnerClass dinner);
        public abstract int InsertBeverages(BeveragesClass Beverages);

        public abstract int InsertOrders(OrderClass Orders);



        public abstract int UpdateBreakafast(BreakfastClass breakfast);
        public abstract int UpdateLunch(LunchClass lunch);
        public abstract int UpdateDinner(DinnerClass dinner);
        public abstract int UpdateBeverages(BeveragesClass Beverages);
        public abstract int UpdateOrders(OrderClass Orders);



        public abstract int DeleteBreakafast(BreakfastClass breakfast);
        public abstract int DeleteLunch(LunchClass lunch);
        public abstract int DeleteDinner(DinnerClass dinner);
        public abstract int DeleteBeverages(BeveragesClass Beverages);
        public abstract int DeleteOrders(OrderClass Orders);

        public abstract int DeleteAllOrders();

        public abstract List<BreakfastClass> SelectAllBreakfast();
        public abstract List<LunchClass> SelectAllLunch();
        public abstract List<DinnerClass> SelectAllDinner();
        public abstract List<BeveragesClass> SelectAllBev();
        public abstract List<OrderClass> SelectAllOrders();


    }
}
