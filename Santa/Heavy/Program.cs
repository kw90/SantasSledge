using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavy
{
    class Program
    {
        static void Main(string[] args)
        {
            var activ = new List<int>();

            var list = new double[1000];
            for (int i = 0; i < 0; i++)
            {
                list[i] = i;
            }

            var before = DateTime.UtcNow;

            ParallelRun(activ, list);
            //Run(activ, list);

            Console.WriteLine("Total time {0}", DateTime.UtcNow - before);
            Console.ReadLine();
        }

        private static void ParallelRun(List<int> activ, double[] list)
        {
            Parallel.For(0, list.Length,
                               index =>
                               {
                                   Calculate(list, activ, index);
                               });
        }

        private static void Run(List<int> activ, double[] list)
        {
            for(int index = 0; index < list.Length; index++)
            {
                Calculate(list, activ, index);
            }
        }

        private static void Calculate(double[] list, List<int> activ, int index)
        {
            activ.Add(index);
            Console.WriteLine("jobs: {0} jobIndex: {1}", activ.Count, Print(activ));

            for (int i = 0; i < 1000000; i++)
            {
                var test = list[index];
                var sin = Math.Sin(test);
                var cos = Math.Cos(test);
                var backSin = Math.Asin(sin);
                var backCos = Math.Acos(cos);
                var tan = Math.Tan(test);
                var backTan = Math.Atan(tan);
                test++;
            };

            activ.Remove(index);
            Console.WriteLine("jobs: {0} jobIndex: {1}", activ.Count, Print(activ));
        }

        private static string Print(List<int> list)
        {
            //var builder = new StringBuilder();
            //foreach (var value in list)
            //{
            //    builder.Append(value);
            //    builder.Append(" ");
            //}
            //return builder.ToString();
            return string.Empty;
        }
    }
}
