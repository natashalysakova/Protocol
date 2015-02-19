using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Protocols
{
    public class Gas : Mesure
    {

        public Gas(string compName, string gosNumb, string vend, int numb, DateTime dogov, double maxval, DateTime vypiska)
            : base(compName, gosNumb, vend, numb, dogov, maxval, vypiska)
        {
            values = new double[6, 1];
            DRandomLib.DRandom r = new DRandomLib.DRandom();

            double coef = 1.55;

            do
            {
                avgValue = 0;
                values[0, 0] = Math.Round(r.NextDouble(Math.Pow(maxValue,2)/(4*coef)/*парабола*/, maxValue), 2);
                for (int i = 1; i < values.GetLength(0); i++)
                {
                    values[i, 0] = Math.Round(r.NextDouble(values[0, 0] - 0.03, values[0, 0] + 0.03), 2);
                    Thread.Sleep(2);

                }

                for (int i = 0; i < values.GetLength(0); i++)
                {
                    avgValue += values[i, 0];
                }

                avgValue /= 6;
                avgValue = Math.Round(avgValue, 2);

            } while (avgValue > maxValue);

        }


    }
}
