using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrenadesGASource.BL.BusinessClasses
{
    public class OrderClass
    {

        private int id;
        private string name;
        private double price;
        private byte[] image;
        private int numberOfItems;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public byte[] Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }

        public int NumberOfItems
        {
            get
            {
                return numberOfItems;
            }

            set
            {
                numberOfItems = value;
            }
        }

        public override string ToString()
        {
            return Id + " " + Name + " "
                + " " + Price.ToString("C");
        }

    }
}
