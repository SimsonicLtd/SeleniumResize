using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Selenium.Resize
{
    class Program
    {
        const int height = 1080;
        const int width = 1920;
        const int loop = 10;

        static void Main(string[] args)
        {
            var or = OpenAndResize();
            var ocs = OpenCorrectSize();
            var orh = OpenAndResizeHeadless();
            var ocsh = OpenCorrectSizeHeadless();
            Console.WriteLine($"Open and Resize: {or}");
            Console.WriteLine($"Open Correct Size: {ocs}");
            Console.WriteLine($"Open and Resize (Headless): {orh}");
            Console.WriteLine($"Open Correct Size (Headless): {ocsh}");

            Console.ReadKey();
        }

        private static double Measure(int loop, Action task)
        {
            var times = new List<long>();
            for (int i = 0; i < loop; i++)
            {
                var stopwatch = new Stopwatch();

                stopwatch.Start();
                task.Invoke();
                stopwatch.Stop();
                times.Add(stopwatch.ElapsedMilliseconds);
            }

            return times.Average();
        }

        
        private static double OpenAndResize()
        {
            return Measure(loop, () => {
                var chrome = new ChromeDriver(".");

                chrome.Manage().Window.Size = new System.Drawing.Size(width, height);

                chrome.Quit();
            });
        }

        private static double OpenCorrectSize()
        {
            return Measure(loop, () => {
                var options = new ChromeOptions();
                options.AddArgument($"--window-size={width},{height}");

                var chrome = new ChromeDriver(".", options);

                chrome.Quit();
            });

            
        }


        private static double OpenAndResizeHeadless()
        {
            return Measure(loop, () => {
                var options = new ChromeOptions();
                options.AddArgument("--headless");

                var chrome = new ChromeDriver(".", options);

                chrome.Manage().Window.Size = new System.Drawing.Size(width, height);

                chrome.Quit();
            });

            
        }

        private static double OpenCorrectSizeHeadless()
        {
            return Measure(loop, () => {
                var options = new ChromeOptions();
                options.AddArgument($"--window-size={width},{height}");
                options.AddArgument("--headless");

                var chrome = new ChromeDriver(".", options);

                chrome.Quit();
            });
        }
    }
}
