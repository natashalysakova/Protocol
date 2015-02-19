using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocols
{
    public abstract class Mesure
    {
        public string CompanyName { set; get; }
        public string GosNumber { set; get; }
        public string VendorModel { set; get; }

        public int DogovorNumber { set; get; }

        public DateTime DogovorDate { set; get; }

        public double maxValue { set; get; }

        public double avgValue { set; get; }

        public double[,] values;

        public DateTime VypiskaDate { set; get; }


        public Mesure(string compName, string gosNumb, string vend, int numb, DateTime dogov, double maxval, DateTime vypiska)
        {
            CompanyName = compName;
            GosNumber = gosNumb;
            VendorModel = vend;
            DogovorNumber = numb;
            DogovorDate = dogov;
            maxValue = maxval;
            VypiskaDate = vypiska;
        }

    }
}
