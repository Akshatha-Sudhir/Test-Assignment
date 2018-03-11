using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;

namespace TestAssignment.Reports
{
    class LogReports
    {
        public static ExtentReports extent;
        public static ExtentTest test;

        public ExtentTest StartLoggingTest(string testname,string reportname)
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\"+reportname;

            extent = new ExtentReports(reportPath, true);
            extent
            .AddSystemInfo("Host Name", "TestAssignment")
            .AddSystemInfo("Environment", "QA")
            .AddSystemInfo("User Name", "Test User");
            extent.LoadConfig(projectPath + "extent-config.xml");

            test = extent.StartTest(testname);
            return test;
            
        }

        public void LogInfo(ExtentTest test,string message)
        {
            test.Log(LogStatus.Info, message);
        }

        public void LogSuccess(ExtentTest test,string message)
        {
            test.Log(LogStatus.Pass, message);
        }

        public void LogFail(ExtentTest test,string message)
        {
            test.Log(LogStatus.Fail, message);
        }

        public void EndLoggingTest(ExtentTest test)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                test.Log(LogStatus.Fail, stackTrace + errorMessage);
            }
            extent.EndTest(test);

            extent.Flush();
            extent.Close();
        }
    }
}
