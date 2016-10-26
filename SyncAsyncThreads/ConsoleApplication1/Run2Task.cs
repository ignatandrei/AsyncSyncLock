using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Run2Task
    {
        public void WriteToDict(string name)
        {
            Dates.AddOrUpdate(DateTime.Now, name, (dt, oldvalue) => oldvalue + "!" + name);
        }
        public ConcurrentDictionary<DateTime, string> Dates=new ConcurrentDictionary<DateTime, string>();
        async Task<int> AsyncTask(int i) 
        {
            WriteToDict("starting " + nameof(AsyncTask) + "argument :" + i);
            var wait = new Random(i + DateTime.Now.Millisecond).Next(1, 10);
            await Task.Delay(wait*1000);
            WriteToDict("ending " + nameof(AsyncTask) + "argument :" + i);
            return i;

        }

        private  SemaphoreSlim sem = new SemaphoreSlim(1);
        static object myObject = new object();
        async Task<int> syncTask(int i)
        {
            lock (myObject)
            try
            {
                //await sem.WaitAsync();                
                WriteToDict("==>starting " + nameof(syncTask) + "argument :" + i);
                //Thread.Sleep(10 * new Random(i + DateTime.Now.Millisecond).Next(1, 10));
                var wait = new Random(i + DateTime.Now.Millisecond).Next(1, 10);
                Process.Start("timeout", "" + wait).WaitForExit();
                WriteToDict("<==ending " + nameof(syncTask) + "argument :" + i + "<==");
                
            }
            finally
            {
                //sem.Release();
            }
            return i;
            
        }

       

        public async Task<int> RunTask(int nr)
        {
            var list=new List<Task<int>>(nr);
            for (int i = 0; i < nr; i++)
            {
                //WriteToDict(i);
                var backup = i;
                Task<int> t;
                if(backup % 2==0)
                {
                    t = AsyncTask(backup);
                }
                else
                {
                    t = syncTask(backup);
                }
                list.Add(t);
            }
            await Task.WhenAll(list.ToArray());
            return list.Sum(it => it.Result);

        }
    }
}