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
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string iedriverpath = projectPath + "IEDriverServer_Win32_3.9.0";
            string chromedriverpath = projectPath + "chromedriver_win32";

            switch (browserName)
            {
                case "Firefox":

                    driver = new FirefoxDriver();
                    return driver;


                    
                case "IE":

                    
                    driver = new InternetExplorerDriver(iedriverpath);
                    return driver;
                    

                case "Chrome":

                    
                    driver = new ChromeDriver(chromedriverpath);
                    return driver;

                default:
                    driver = new ChromeDriver(chromedriverpath);
                    return driver;
            }

        }
        

        

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
