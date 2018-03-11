﻿using System;
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

namespace TestAssignment.TestCases
{
    class Functionality_ZeroCents_Validation
    {
        public IWebDriver driver;
                           
        [Test]
        public void functionality_ZeroCents_Validation()      //This test case accounts for 2 different datasets
        {
            try
            {
                LogReports utility = new LogReports();

                //Starting the test
                ExtentTest test=utility.StartLoggingTest("Validate Zero Cents Field","ZeroCents.html");
                Console.WriteLine("Start Test: Validate ZeroCents Field\n");
                bool flag1 = false, flag2 = false;          // Both the flags indicate status of the 2 Scenarios. 

                utility.LogInfo(test, "Setting up the browser.");
                Console.WriteLine("Setting up the browser.\n");
                driver = Browser.InitBrowser(driver, ConfigurationManager.AppSettings["Browser"]);

                utility.LogInfo(test, "Navigating to the required page.");
                Console.WriteLine("Navigating to the required page.\n");
                Browser.LoadApplication(driver, ConfigurationManager.AppSettings["URL"]);

                //Initialising all the web elements present on the page.
                var fieldsPage = new FieldsPage(driver);

                //Setting the path of the TestData file.
                string testdata_path = @"C:\Users\Akshatha\source\repos\TestAssignment\TestAssignment\TestData\Validate_ZeroCents.xml";
                AccessData test_data = new AccessData();

                //Scenario-1 : Positive Flow Test-Passing numbers into the field.
                utility.LogInfo(test, "Scenario-1 : Positive Flow Test-Passing numbers into the field.");
                Console.WriteLine("Scenario-1 : Positive Flow Test-Passing numbers into the field.");
                string data = test_data.read(testdata_path, 0);        //Value 0 indicates the first dataset to be passed into the field.            
                utility.LogInfo(test, "Entering data " + data + " into ZeroCents field");
                Console.WriteLine("Entering data " + data + " into ZeroCents field");
                fieldsPage.SendKeysToZeroCents(data);

                //validate whether the data sent is rendered in the required format.
                var tobevalidated_value = fieldsPage.ZeroCents.GetAttribute("value");
                string zerocents_pattern = @"[0-9]{1,3}(\.[0-9]{3})*,00";
                Match match = Regex.Match(tobevalidated_value, zerocents_pattern);
                if (match.Success)
                {
                    flag1 = true;
                    utility.LogSuccess(test, "The value - " + tobevalidated_value + " has been rendered in the expected format");
                    Console.WriteLine("Success : The value - " + tobevalidated_value + " has been rendered in the expected format.\n");
                }
                else
                {
                    utility.LogFail(test, "The value is not being rendered in the expected format.");
                    Console.WriteLine("Failure : The value is not being rendered in the expected format.\n");
                }

                //Clearing the field to send in different data.
                fieldsPage.ZeroCents.Clear();

                //Scenario-2 : Negative Flow Test-Passing alphabets and special characters.
                utility.LogInfo(test, "Scenario-2 : Negative Flow Test-Passing alphabets and special characters.");
                Console.WriteLine("Scenario-2 : Negative Flow Test-Passing alphabets and special characters.");
                data = test_data.read(testdata_path, 1);   //Value 1 indicates the second dataset to be passed into the field.
                utility.LogInfo(test, "Entering data " + data + " into ZeroCents field");
                Console.WriteLine("Entering data " + data + " into ZeroCents field");
                fieldsPage.SendKeysToZeroCents(data);

                //validate whether the data is accepted into the field.
                tobevalidated_value = fieldsPage.ZeroCents.GetAttribute("value").ToString();
                if (tobevalidated_value.Equals("0,00"))
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
