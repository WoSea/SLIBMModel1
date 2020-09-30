using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMModel1
{
    class Program
    {
        static void Main(string[] args)
        {
            IBMModel1Demo RunDemo = new IBMModel1Demo();
           RunDemo.RunSimpleTraining();
           //   RunDemo.RunEMTraining();
        }
    }
}
