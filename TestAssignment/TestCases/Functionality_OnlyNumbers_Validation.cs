using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using TestAssignment.Wrapper;
using TestAssignment.Generics;
using System.Configuration;
using TestAssignment.TestData;
using System.Windows;
using TestAssignment.Reports;
using RelevantCodes.ExtentReports;
using System.Threading;


namespace TestAssignment.TestCases
{
    class Functionality_OnlyNumbers_Validation 
    {
        public IWebDriver driver;  

        
        [Test]
        public void functionality_OnlyNumbers_Validation()          // This test accounts for 2 different Datasets.
        {
            try
            {
                LogReports utility = new LogReports();
                //Starting the test
                ExtentTest test=utility.StartLoggingTest("Validate Only Numbers Field","OnlyNumbers.html");
                Console.WriteLine("Start Test: Validate OnlyNumbers Field\n");
                bool flag1 = false, flag2 = false;          // Both the flags indicate status of the 2 Scenarios. 

                utility.LogInfo(test, "Setting up the browser");
                Console.WriteLine("Setting up the browser\n");
                driver = Browser.InitBrowser(driver, ConfigurationManager.AppSettings["Browser"]);

                utility.LogInfo(test, "Navigating to the required page");
                Console.WriteLine("Navigating to the required page\n");
                Browser.LoadApplication(driver, ConfigurationManager.AppSettings["URL"]);

                //Initialising all the web elements present on the page.
                var fieldsPage = new FieldsPage(driver);

                //Setting the path of the TestData file.
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string testdata_path = projectPath + "TestData\\Validate_OnlyNumbers.xml";

                AccessData test_data = new AccessData();

                //Scenario-1 : Positive Flow Test-Passing only numbers into the field.
                utility.LogInfo(test, "Scenario-1 : Positive Flow Test-Passing only numbers into the field.");
                Console.WriteLine("Scenario-1 : Positive Flow Test-Passing only numbers into the field.");
                string data = test_data.read(testdata_path, 0);  //Value 0 indicates the first dataset to be passed into the field.
                utility.LogInfo(test, "Entering data " + data + " into OnlyNumbers field");
                Console.WriteLine("Entering data " + data + " into OnlyNumbers field");
                fieldsPage.SendKeysToOnlyNumbers(data);
                Thread.Sleep(2000);

                //validate whether the data sent is equal to the data rendered.
                string tobevalidated_value = fieldsPage.OnlyNumbers.GetAttribute("value").ToString();
                if (data.Equals(tobevalidated_value))
                {
                    flag1 = true;
                    utility.LogSuccess(test, "The data sent matches with the data - " + tobevalidated_value + " rendered on the screen.");
                    Console.WriteLine("Success: The data sent matches with the data - " + tobevalidated_value + " rendered on the screen.\n");
                }
                else
                {
                    utility.LogFail(test, "The data sent differs from the data rendered on the screen.");
                    Console.WriteLine("Failure: The data sent differs from the data rendered on the screen.\n");
                }

                //Clearing the field to send in different data.
                fieldsPage.OnlyNumbers.Clear();

                //Scenario-2 : Negative Flow Test-Passing alphabets and special characters into the field.
                utility.LogInfo(test, "Scenario-2 : Negative Flow Test-Passing alphabets and special characters into the field.");
                Console.WriteLine("Scenario-2 : Negative Flow Test-Passing alphabets and special characters into the field.");
                data = test_data.read(testdata_path, 1);  //Value 1 indicates the second dataset to be passed into the field.
                utility.LogInfo(test, "Entering data " + data + " into OnlyNumbers field");
                Console.WriteLine("Entering data " + data + " into OnlyNumbers field");
                fieldsPage.SendKeysToOnlyNumbers(data);
                Thread.Sleep(2000);

                //validate whether the data sent is visible on the screen.
                tobevalidated_value = fieldsPage.OnlyNumbers.GetAttribute("value").ToString();
                if (tobevalidated_value.Equals(""))
                {
                    flag2 = true;
                    utility.LogSuccess(test, "The field does not accept any other character except numbers.");
                    Console.WriteLine("Success: The field does not accept any other character except numbers.\n");
                }
                else
                {
                    utility.LogFail(test, "The field should accept only numbers, but behaving differently.");
                    Console.WriteLine("Failure: The field should accept only numbers, but behaving differently.\n");
                }


                //verify if both the scenarios passed
                if (flag1 && flag2)
                {
                    Console.WriteLine("Test status: Success");
                }
                else
                {
                    Console.WriteLine("Test status: Failure");
                }

                utility.EndLoggingTest(test);

                //Closing the driver
                Browser.CloseDriver(driver);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}