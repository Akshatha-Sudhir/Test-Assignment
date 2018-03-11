using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;


namespace TestAssignment.Wrapper
{
    class Browser
    {

        

        
        public static IWebDriver InitBrowser(IWebDriver driver,string browserName)
        {
            switch (browserName)
            {
                case "Firefox":

                    driver = new FirefoxDriver();
                    return driver;


                    
                case "IE":

                    driver = new InternetExplorerDriver(@"C:\Users\Akshatha\Downloads\IEDriverServer_Win32_3.9.0");
                    return driver;
                    

                case "Chrome":

                    driver = new ChromeDriver(@"C:\Users\Akshatha\Downloads\chromedriver_win32");
                    return driver;

                default:
                    driver = new ChromeDriver(@"C:\Users\Akshatha\Downloads\chromedriver_win32");
                    return driver;
            }

        }
        //


        

        
        

        public static void LoadApplication(IWebDriver driver ,string url)
        {
            driver.Url = url;
        }

        public static void CloseDriver(IWebDriver driver)
        {
            driver.Close();
            driver.Quit();
        }
    }
}
