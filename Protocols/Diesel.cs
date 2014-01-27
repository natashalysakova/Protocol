using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DRandomLib;

namespace Protocols
{
    public class Diesel : Mesure
    {
        public double maxValueMeters { set; get; }
        public double avgValueMeters { set; get; }


        public Diesel(string compName, string gosNumb, string vend, int numb, DateTime dogov, double maxval)
            : base(compName, gosNumb, vend, numb, dogov, maxval)
        {
            values = new double[6, 2];
            maxValueMeters = (-1.0 / 0.43) * Math.Log(1.0 - (maxValue / 100.0));

            DRandom r = new DRandom();



            do
            {
                avgValue = 0;
                values[0, 1] = Math.Round(r.NextDouble(maxValue*0.25, maxValue - 4));
                values[0, 0] = Math.Round((-1.0 / 0.43) * Math.Log(1.0 - (values[0, 1] / 100.0)), 3);

                for (int i = 1; i < values.GetLength(0); i++)
                {
                    values[i, 1] = Math.Round(r.NextDouble(values[0, 1] - 7, values[0, 1] + 7));
                    Thread.Sleep(2);
                    values[i, 0] = Math.Round((-1.0 / 0.43) * Math.Log(1.0 - (values[i, 1] / 100.0)), 3);
                }

                for (int i = 0; i < values.GetLength(0); i++)
                {
                    avgValue += values[i, 1];
                    avgValueMeters += values[i, 0];
                }

                avgValue /= 6;
                avgValueMeters /= 6;

            } while (avgValue > maxValue);


            avgValue = Math.Round(avgValue, 3);
            maxValueMeters = Math.Round(maxValueMeters, 2);
            avgValueMeters = Math.Round(avgValueMeters, 3);
        }
    }
}
