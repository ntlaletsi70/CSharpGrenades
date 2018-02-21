using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpGrenadesGASource.DAL;
using CSharpGrenadesGASource.BL.BusinessClasses;

namespace CSharpGrenadesGASource.BL
{
    public class MenuBL
    {
        private MenuProviderBase providerBase;

        public MenuBL(string Provider)
        {
            SetupProvider(Provider);
        }

        public int InsertBeverage(BeveragesClass Bev)
        {
            return providerBase.InsertBeverages(Bev);
        }

        public int InsertBreakfast(BreakfastClass Breakfast)
        {
            return providerBase.InsertBreakafast(Breakfast);
        }

        public int InsertDinner(DinnerClass Dinner)
        {
            return providerBase.InsertDinner(Dinner);
        }

        public int InsertLunch(LunchClass lunch)
        {
            return providerBase.InsertLunch(lunch);
        }


        public int UpdateBeverage(BeveragesClass Bev)
        {
            return providerBase.UpdateBeverages(Bev);
        }

        public int UpdateBreakfast(BreakfastClass Breakfast)
        {
            return providerBase.UpdateBreakafast(Breakfast);
        }

        public int UpdateDinner(DinnerClass Dinner)
        {
            return providerBase.UpdateDinner(Dinner);
        }

        public int UpdateLunch(LunchClass lunch)
        {
            return providerBase.UpdateLunch(lunch);
        }


        public int DeleteBeverages(BeveragesClass Bev)
        {
            return providerBase.DeleteBeverages(Bev);
        }

        public int DeleteBreakafast(BreakfastClass Breakfast)
        {
            return providerBase.DeleteBreakafast(Breakfast);
        }

        public int DeleteDinner(DinnerClass Dinner)
        {
            return providerBase.DeleteDinner(Dinner);
        }

        public int DeleteLunch(LunchClass lunch)
        {
            return providerBase.DeleteLunch(lunch);
        }

        public List<BeveragesClass> SelectAllBeverage()
        {
            return providerBase.SelectAllBev();
        }
        public List<LunchClass> SelectAllLunch()
        {
            return providerBase.SelectAllLunch();
        }
        public List<BreakfastClass> SelectAllBreakFast()
        {
            return providerBase.SelectAllBreakfast();
        }
        public List<DinnerClass> SelectAllDinner()
        {
            return providerBase.SelectAllDinner();
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
