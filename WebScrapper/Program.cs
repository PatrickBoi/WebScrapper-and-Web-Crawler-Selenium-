using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using System.Timers;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Text;
namespace WebScrapper
{

    class ContextGetter
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://www.overleaf.com/login");

            var userEmail = driver.FindElement(By.XPath("//*[@id=\"email\"]"));
            userEmail.SendKeys("Taha@mailinator.com");

            var userPassword = driver.FindElement(By.XPath("//*[@id=\"password\"]"));
            userPassword.SendKeys("over1234");

            var loginButton = driver.FindElement(By.XPath("/html/body/div[2]/main/div[1]/form/div[5]/button"));
            loginButton.Click();

            Console.WriteLine("Worked");
            Console.WriteLine(loginButton);

            if (loginButton != null)
            {
                Thread.Sleep(15000);
                

                var gotoproject = driver.FindElement(By.XPath("/html/body/main/div[2]/div/div/div[2]/div[3]/div/div/ul/table/tbody/tr[2]/td[1]/div/span/a"));
                gotoproject.Click();

                Thread.Sleep(10000);
                Console.WriteLine("Scrapping Started");

                //var content1 = driver.FindElement(By.XPath("/html/body or div[2]")).Text;

                var content1 = driver.FindElement(By.XPath("/html/body/div[2]")).Text;
                Console.WriteLine(content1);

                if (content1 != null)
                {
                    string filename = "overleaf_data_scrapped_";
                    string extension = ".txt";
                    string filepath = @"E:\dumpster\";
                   if (!File.Exists( filepath + filename + extension))
                    {
                        string create_txt = content1;
                        File.WriteAllText(filepath + filename + extension, create_txt);

                    }
                   else if (File.Exists(filepath + filename + extension))
                    {
                        string create_txt = content1;
                        File.WriteAllText(filepath+ filename + "updated" + extension, create_txt);
                    }
                }
                
            }

            else
            {
                Environment.Exit(0);
            }


            string read_directory = @"E:\dumpster\";
            String[] FileA = File.ReadAllLines(Path.Combine(read_directory, "overleaf_data_scrapped_.txt"));
            String[] FileB = File.ReadAllLines(Path.Combine(read_directory, "overleaf_data_scrapped_updated.txt"));
            IEnumerable<String> onlyB = FileB.Except(FileA);

            File.WriteAllLines(Path.Combine(read_directory, "Result.txt"), onlyB);

        }

    }

}




