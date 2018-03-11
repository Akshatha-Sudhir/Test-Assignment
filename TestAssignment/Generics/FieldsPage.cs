using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestAssignment.Generics
{
    class FieldsPage
    {
        private IWebDriver driver;
        public FieldsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }



        [FindsBy(How = How.Id, Using = "zeroCents")]
        [CacheLookup]
        public IWebElement ZeroCents { get; set; }

        [FindsBy(How = How.Id, Using = "numbers")]
        [CacheLookup]
        public IWebElement OnlyNumbers { get; set; }

        [FindsBy(How = How.Id, Using = "phone")]
        [CacheLookup]
        public IWebElement Phone { get; set; }

        public void SendKeysToZeroCents(string value)
        {
            try
            {
                ZeroCents.SendKeys(value);
            }catch(NoSuchElementException e)
            {
                Console.WriteLine(e.Message);
            }
                
        }
        
        public void SendKeysToOnlyNumbers(string value)
        {
            try
            {
                OnlyNumbers.SendKeys(value);
            }catch(NoSuchElementException e)
            {
                Console.WriteLine(e.Message);
            }
        
        }

        public void SendKeysToPhone(string value)
        {
            try
            {
                Phone.SendKeys(value);
            }catch(NoSuchElementException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
