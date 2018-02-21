using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrenadesGASource.BL.BusinessClasses
{
    public class BreakfastClass
    {

        private int id;
        private string name;
        private double price;
        private byte[] image;


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

        public override string ToString()
        {
            return Id + " " + Name + " "
                + " " + Price.ToString("C");
        }
    }
}
