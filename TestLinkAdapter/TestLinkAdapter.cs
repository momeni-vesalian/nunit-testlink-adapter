using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Meyn.TestLink;
using NUnit.Framework;

namespace NUnit.TestLink
{
    class TestLinkAdapter
    {
        private static readonly string DefaultBuildName = "Default-Build";
        private static readonly string DefaultBuildDescription = "Automated Build";
        private static readonly string DefaultTestSuiteDescription = "Automated Test Suite";
        private static readonly string DefaultTestCaseDescription = "Automated Test Case";

        public GeneralResult SendToTestlink(MethodBase callerMethod, TestCaseResultStatus status, string notes)
        {
            AdapterContext context = CreateAdapterContext(callerMethod);
            if (context.PlatformId == 0)
            {
                throw new ArgumentException("You must set TestLinkFixture.PlatformName attribute correctly and link it to test plan in testlink!");
            }

            return context.Proxy.ReportTCResult(context.TestCaseId, context.TestPlanId, status, context.PlatformId, notes: notes);
        }

        private AdapterContext CreateAdapterContext(MethodBase method)
        {
            AdapterContext context = new AdapterContext();

            SetAttributes(context, method);

            context.Proxy = new Meyn.TestLink.TestLink(context.TestLinkFixtureAttribute.DevKey, context.TestLinkFixtureAttribute.Url);
            context.UserId = context.TestLinkFixtureAttribute.UserId;

            context.TestProjectName = context.TestLinkFixtureAttribute.ProjectName;
            context.TestProjectPrefix = context.TestLinkFixtureAttribute.ProjectPrefix;
            context.TestProjectDescription = context.TestLinkFixtureAttribute.ProjectDescription;
            SetTestProjectId(context);

            context.TestPlanName = context.TestLinkFixtureAttribute.TestPlanName;
            context.TestPlanDescription = context.TestLinkFixtureAttribute.TestPlanDescription;
            SetTestPlanId(context);

            context.BuildName = context.TestLinkFixtureAttribute.BuildName;
            context.BuildDescription = context.TestLinkFixtureAttribute.BuildDescription;
            setBuildId(context);

            context.PlatformName = context.TestLinkFixtureAttribute.PlatformName;
            SetPlatformId(context);

            SetTestSuiteName(context, method);
            SetTestSuiteDescription(context);
            SetTestSuiteId(context);

            context.TestCaseName = method.Name;
            SetTestCaseDescription(context);
            SetTestCaseId(context);

            return context;
        }

        private void SetAttributes(AdapterContext context, MethodBase method)
        {
            foreach (var customAttribute in method.ReflectedType.GetCustomAttributes(false))
            {
                if (customAttribute is TestLinkFixtureAttribute)
                {
                    context.TestLinkFixtureAttribute = (TestLinkFixtureAttribute) customAttribute;
                }
                else if (customAttribute is TestFixtureAttribute)
                {
                    context.TestFixtureAttribute = (TestFixtureAttribute) customAttribute;
                }
            }
            if (context.TestLinkFixtureAttribute == null)
            {
                throw new ArgumentException("You must set TestLinkFixture attribute for test class!");
            }

            foreach (var customAttribute in method.GetCustomAttributes(false))
            {
                if (customAttribute is TestAttribute)
                {
                    context.TestAttribute = (TestAttribute) customAttribute;
                }
            }
        }

        private void SetTestProjectId(AdapterContext context)
        {
            try
            {
                TestProject testProject = context.Proxy.GetProject(context.TestProjectName);
                context.TestProjectPrefix = testProject.prefix;
                context.TestProjectDescription = testProject.notes;
                context.TestProjectId = testProject.id;
            }
            catch (Exception e)
            {
                // Need to create test project
                if (context.TestProjectPrefix == null)
                {
                    context.TestProjectPrefix = context.TestProjectName.Substring(0, 1);
                }
                GeneralResult result = context.Proxy.CreateProject(context.TestProjectName, context.TestProjectPrefix, context.TestProjectDescription);
                context.TestProjectId = result.id;
            }
        }

        private void SetTestPlanId(AdapterContext context)
        {
            try
            {
                TestPlan testPlan = context.Proxy.getTestPlanByName(context.TestProjectName, context.TestPlanName);
                context.TestPlanDescription = testPlan.notes;
                context.TestPlanId = testPlan.id;
            }
            catch (Exception e)
            {
                // Need to create test plan
                GeneralResult result = context.Proxy.CreateTestPlan(context.TestPlanName, context.TestProjectName, context.TestPlanDescription, true);
                context.TestPlanId = result.id;
            }
        }

        private void setBuildId(AdapterContext context)
        {
            if (context.BuildName == null)
            {
                try
                {
                    Build latestBuild = context.Proxy.GetLatestBuildForTestPlan(context.TestPlanId);
                    context.BuildName = latestBuild.name;
                    context.BuildDescription = latestBuild.notes;
                    context.BuildId = latestBuild.id;
                    return;
                }
                catch (Exception e)
                {
                    context.BuildName = DefaultBuildName;
                    context.BuildDescription = DefaultBuildDescription;
                }
            }
            else
            {
                List<Build> builds = context.Proxy.GetBuildsForTestPlan(context.TestPlanId);
                foreach (Build build in builds)
                {
                    if (build.name == context.BuildName)
                    {
                        context.BuildDescription = build.notes;
                        context.BuildId = build.id;
                        return;
                    }
                }
            }

            // Need to create build
            GeneralResult result = context.Proxy.CreateBuild(context.TestPlanId, context.BuildName, context.BuildDescription);
            context.BuildId = result.id;
        }

        private void SetPlatformId(AdapterContext context)
        {
            List<TestPlatform> platforms = context.Proxy.GetTestPlanPlatforms(context.TestPlanId);
            foreach (TestPlatform platform in platforms)
            {
                if (platform.name == context.PlatformName)
                {
                    context.PlatformId = platform.id;
                }
            }
        }

        private void SetTestSuiteName(AdapterContext context, MethodBase method)
        {
            context.TestSuiteName = context.TestLinkFixtureAttribute.TestSuiteName;
            if (context.TestSuiteName == null)
            {
                // ReSharper disable once PossibleNullReferenceException
                context.TestSuiteName = method.ReflectedType.Name;
            }
        }

        private void SetTestSuiteDescription(AdapterContext context)
        {
            context.TestSuiteDescription = context.TestLinkFixtureAttribute.TestSuiteDescription;
            if (context.TestSuiteDescription == null)
            {
                context.TestSuiteDescription = context.TestFixtureAttribute.Description;
                if (context.TestSuiteDescription == null)
                {
                    context.TestSuiteDescription = DefaultTestSuiteDescription;
                }
            }
        }

        private void SetTestSuiteId(AdapterContext context)
        {
            List<TestSuite> testSuites = context.Proxy.GetFirstLevelTestSuitesForTestProject(context.TestProjectId);
            foreach (TestSuite testSuite in testSuites)
            {
                if (testSuite.name == context.TestSuiteName)
                {
                    context.TestSuiteId = testSuite.id;
                    return;
                }
            }

            // Need to create test suite
            GeneralResult result = context.Proxy.CreateTestSuite(context.TestProjectId, context.TestSuiteName, context.TestSuiteDescription);
            context.TestSuiteId = result.id;
        }

        private static void SetTestCaseDescription(AdapterContext context)
        {
            context.TestCaseDescription = context.TestAttribute.Description;
            if (context.TestCaseDescription == null)
            {
                context.TestCaseDescription = DefaultTestCaseDescription;
            }
        }

        private void SetTestCaseId(AdapterContext context)
        {
            List<TestCaseFromTestSuite> testCases = context.Proxy.GetTestCasesForTestSuite(context.TestSuiteId, true);
            foreach (TestCaseFromTestSuite testCase in testCases)
            {
                if (testCase.name == context.TestCaseName)
                {
                    context.TestCaseDescription = testCase.details;
                    context.TestCaseId = testCase.id;
                    return;
                }
            }

            // Need to create test case
            GeneralResult result = context.Proxy.CreateTestCase(context.UserId, context.TestSuiteId, context.TestCaseName,
                context.TestProjectId, context.TestCaseDescription, new TestStep[0], "", 0, true, ActionOnDuplicatedName.Block, 2, 2);

            string externalId = GenerateCorrectTestCaseExternalId(context.TestProjectPrefix, result.additionalInfo.external_id);
            context.Proxy.addTestCaseToTestPlan(context.TestProjectId, context.TestPlanId, externalId, result.additionalInfo.version_number, context.PlatformId);

            context.TestCaseId = result.additionalInfo.id;
        }

        private string GenerateCorrectTestCaseExternalId(string testProjectPrefix, int externalId)
        {
            return string.Format("{0}-{1}", testProjectPrefix, externalId);
        }

        private class AdapterContext
        {
            private TestAttribute _testAttribute;

            public virtual TestAttribute TestAttribute
            {
                get { return _testAttribute; }
                set { _testAttribute = value; }
            }

            private TestFixtureAttribute _testFixtureAttribute;

            public virtual TestFixtureAttribute TestFixtureAttribute
            {
                get { return _testFixtureAttribute; }
                set { _testFixtureAttribute = value; }
            }

            private TestLinkFixtureAttribute _testLinkFixtureAttribute;

            public virtual TestLinkFixtureAttribute TestLinkFixtureAttribute
            {
                get { return _testLinkFixtureAttribute; }
                set { _testLinkFixtureAttribute = value; }
            }

            private Meyn.TestLink.TestLink _proxy;

            public virtual Meyn.TestLink.TestLink Proxy
            {
                get { return _proxy; }
                set { _proxy = value; }
            }

            private string _testProjectName;

            public virtual string TestProjectName
            {
                get { return _testProjectName; }
                set { _testProjectName = value; }
            }

            private string _testProjectPrefix;

            public virtual string TestProjectPrefix
            {
                get { return _testProjectPrefix; }
                set { _testProjectPrefix = value; }
            }

            private string _testProjectDescription;

            public virtual string TestProjectDescription
            {
                get { return _testProjectDescription; }
                set { _testProjectDescription = value; }
            }

            private int _testProjectId;

            public virtual int TestProjectId
            {
                get { return _testProjectId; }
                set { _testProjectId = value; }
            }

            private string _userId;

            public virtual string UserId
            {
                get { return _userId; }
                set { _userId = value; }
            }


            private string _testPlanName;

            public virtual string TestPlanName
            {
                get { return _testPlanName; }
                set { _testPlanName = value; }
            }

            private string _testPlanDescription;

            public virtual string TestPlanDescription
            {
                get { return _testPlanDescription; }
                set { _testPlanDescription = value; }
            }

            private int _testPlanId;

            public virtual int TestPlanId
            {
                get { return _testPlanId; }
                set { _testPlanId = value; }
            }

            private string _buildName;

            public virtual string BuildName
            {
                get { return _buildName; }
                set { _buildName = value; }
            }

            private string _buildDescription;

            public virtual string BuildDescription
            {
                get { return _buildDescription; }
                set { _buildDescription = value; }
            }

            private int _buildId;

            public virtual int BuildId
            {
                get { return _buildId; }
                set { _buildId = value; }
            }

            private string _platformName;

            public virtual string PlatformName
            {
                get { return _platformName; }
                set { _platformName = value; }
            }

            private int _platformId = 0;

            public virtual int PlatformId
            {
                get { return _platformId; }
                set { _platformId = value; }
            }


            private string _testSuiteName;

            public virtual string TestSuiteName
            {
                get { return _testSuiteName; }
                set { _testSuiteName = value; }
            }

            private string _testSuiteDescription;

            public virtual string TestSuiteDescription
            {
                get { return _testSuiteDescription; }
                set { _testSuiteDescription = value; }
            }

            private int _testSuiteId;

            public virtual int TestSuiteId
            {
                get { return _testSuiteId; }
                set { _testSuiteId = value; }
            }

            private string _testCaseName;

            public virtual string TestCaseName
            {
                get { return _testCaseName; }
                set { _testCaseName = value; }
            }

            private string _testCaseDescription;

            public virtual string TestCaseDescription
            {
                get { return _testCaseDescription; }
                set { _testCaseDescription = value; }
            }

            private int _testCaseId;

            public virtual int TestCaseId
            {
                get { return _testCaseId; }
                set { _testCaseId = value; }
            }
        }
    }
}