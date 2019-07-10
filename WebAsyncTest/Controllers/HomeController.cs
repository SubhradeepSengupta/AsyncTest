using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAsyncTest.Models;

namespace WebAsyncTest.Controllers
{
    public class HomeController : Controller
    {
        public string syncthread = " " , asyncthread = " " ;
        public IActionResult Index()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Synchronous().Wait();
            var sync = "Synchronous await:" + sw.Elapsed + " Threads : " + syncthread;
            sw.Restart();
            Asynchronous().Wait();
            var async = "Parallel await:" + sw.Elapsed + " Threads : " + asyncthread;

            return Content("Sync = " + sync + "                        " + "Async = " + async);
        }

        private async Task<string> Synchronous()
        {

            await Task.Run(() =>
            {
                for (int i = 0; i <= 1000000; i++) { continue; }
                syncthread = (Thread.CurrentThread.ManagedThreadId).ToString();
            });
            await Task.Run(() =>
            {
                for (int i = 0; i <= 1000000; i++) { continue; }
                syncthread = syncthread + " " + (Thread.CurrentThread.ManagedThreadId).ToString();
            });
            await Task.Run(() =>
            {
                for (int i = 0; i <= 1000000; i++) { continue; }
                syncthread = syncthread + " " + (Thread.CurrentThread.ManagedThreadId).ToString();
            });

            return syncthread;
        }
        private async Task<string> Asynchronous()
        {
            Task task1 = Task.Run(() =>
            {
                for (int i = 0; i <= 1000000; i++) { continue; }
                asyncthread = asyncthread + " "  + (Thread.CurrentThread.ManagedThreadId).ToString();
            });
            Task task2 = Task.Run(() =>
            {
                for (int i = 0; i <= 1000000; i++) { continue; }
                asyncthread = asyncthread + " " +(Thread.CurrentThread.ManagedThreadId).ToString();
            });
            Task task3 = Task.Run(() =>
            {
                for (int i = 0; i <= 1000000; i++) { continue; }
                asyncthread = asyncthread + " " +(Thread.CurrentThread.ManagedThreadId).ToString();
            });
            await Task.WhenAll(task1, task2, task3);
            return asyncthread;
        }
    }
}
