using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    enum WhatTaskIs
    {
        None=0,
        Sync=1,
        ASYNC=2

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("head to http://msprogrammer.serviciipeweb.ro/ for the post Async + sync + lock");
            var t=new Run2Task();
            var res=t.RunTask(10).Result;
            
           
            var messages = t.Dates.OrderBy(it => it.Key);
            foreach (var keyValuePair in messages)
            {
                Console.WriteLine(keyValuePair.Value);
            }
        }

        
    }
}
