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
using System.Text.RegularExpressions;
using TestAssignment.Reports;
using RelevantCodes.ExtentReports;
using System.Threading;

namespace TestAssignment.TestCases
{
    class Functionality_Phone_Validation
    {
        public IWebDriver driver;

        [Test]
        public void functionality_Phone_Validation()        //This test accounts for 2 different datasets.
        {
            try
            {
                LogReports utility = new LogReports();

                //Starting the test
                ExtentTest test=utility.StartLoggingTest("Validate Phone Field","Phone.html");
                Console.WriteLine("Start Test: Validate Phone Field\n");
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
                string testdata_path = projectPath + "TestData\\Validate_Phone.xml";

                AccessData test_data = new AccessData();

                //Scenario-1 : Positive Flow Test-Passing numbers into the field.
                utility.LogInfo(test, "Scenario-1 : Positive Flow Test-Passing numbers into the field.");
                Console.WriteLine("Scenario-1 : Positive Flow Test-Passing numbers into the field.");
                string data = test_data.read(testdata_path, 0);   //Value 0 indicates the first dataset to be passed into the field.
                utility.LogInfo(test, "Entering data " + data + " into Phone field");
                Console.WriteLine("Entering data " + data + " into Phone field");
                fieldsPage.SendKeysToPhone(data);
                Thread.Sleep(2000);

                //validate whether the data sent is rendered in the required format.
                string tobevalidated_value = fieldsPage.Phone.GetAttribute("value").ToString();
                string phone_pattern = @"\([0-9]{1,2}\)? ?[0-9]*-*[0-9]*";
                Match match = Regex.Match(tobevalidated_value, phone_pattern);
                if (match.Success)
                {
                    flag1 = true;
                    utility.LogSuccess(test, "The value - " + tobevalidated_value + " has been rendered in the expected format.");
                    Console.WriteLine("Success : The value - " + tobevalidated_value + " has been rendered in the expected format.\n");
                }
                else
                {
                    utility.LogFail(test, "The value is not being rendered in the expected format.");
                    Console.WriteLine("Failure : The value is not being rendered in the expected format.\n");
                }

                //Clearing the field to send in different data.
                fieldsPage.Phone.Clear();

                //Scenario-2 : Negative Flow Test-Passing alphabets and special characters.
                utility.LogInfo(test, "Scenario-2 : Negative Flow Test-Passing alphabets and special characters.");
                Console.WriteLine("Scenario-2 : Negative Flow Test-Passing alphabets and special characters.");
                data = test_data.read(testdata_path, 1);   //Value 1 indicates the second dataset to be passed into the field.
                utility.LogInfo(test, "Entering data " + data + " into Phone field");
                Console.WriteLine("Entering data " + data + " into Phone field");
                fieldsPage.SendKeysToPhone(data);
                Thread.Sleep(2000);

                //validate whether the data is accepted into the field.
                tobevalidated_value = fieldsPage.Phone.GetAttribute("value").ToString();
                if (tobevalidated_value.Equals(""))
                {
                    flag2 = true;
                    utility.LogSuccess(test, "The field did not accept any other alphabet or a special character.");
                    Console.WriteLine("Success : The field did not accept any other alphabet or a special character.\n");
                }
                else
                {
                    utility.LogFail(test, "The field should not accept only numbers, but behaving differently.");
                    Console.WriteLine("Failure : The field should not accept only numbers, but behaving differently.\n");
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
