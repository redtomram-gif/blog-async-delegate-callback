using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Collections.Generic;

namespace Delegate
{
    public class SampleClass
    {
        public static bool SampleMethod()
        {
            Console.WriteLine("Inside sample method ...");
            throw new ArgumentException();
        }
    }

    public delegate bool SampleMethodCaller();

    public class DelegateSample
    {
        List<Exception> exceptions;
        ManualResetEvent waiter;

        public DelegateSample()
        {
            exceptions = new List<Exception>();
        }

        public void CallBackMethodForDelegate(IAsyncResult result)
        {
            SampleMethodCaller smd = (SampleMethodCaller)((AsyncResult)result).AsyncDelegate;
            try
            {
                bool returnValue = smd.EndInvoke(result);
            }
            catch (ArgumentException e)
            {
                lock (this)
                {
                    exceptions.Add(e);
                }
            }
            finally
            {
                waiter.Set();
            }
        }

        public void CallDelegateUsingCallBack()
        {
            try
            {
                waiter = new ManualResetEvent(false);
                SampleMethodCaller smd = new SampleMethodCaller(SampleClass.SampleMethod);
                IAsyncResult result = smd.BeginInvoke(CallBackMethodForDelegate, null);
                waiter.WaitOne();

                if (exceptions.Count != 0)
                {
                    throw exceptions[0];
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Catch exceptions here ...");
            }
        }

        public static void Main()
        {
            DelegateSample ds = new DelegateSample();
            ds.CallDelegateUsingCallBack();
            Console.WriteLine(" -----------------");
        }
    }
}
