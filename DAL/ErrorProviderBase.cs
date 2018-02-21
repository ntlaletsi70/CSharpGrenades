using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpGrenadesGASource.BL.BusinessClasses;

namespace CSharpGrenadesGASource.DAL
{
   public abstract class ErrorProviderBase
    {
        public  abstract List<Error> SelectAll(string path);
        public abstract List<Error> SelectAll(string path,int value);

        public abstract int Insert(Error error);      
    }
}
