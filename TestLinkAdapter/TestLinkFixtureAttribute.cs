using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnit.TestLink
{
    /// <summary>
    /// This attribute is used for the various exporter adapters such as the NUnit test framework
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TestLinkFixtureAttribute : System.Attribute
    {
        private string _url;

        /// <summary>
        /// The url for the Testlink XmlRPC api.
        /// </summary>
        public virtual string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _projectName;

        /// <summary>
        /// The name of the test project in testlink
        /// </summary>
        public virtual string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        private string _projectPrefix;

        /// <summary>
        /// The prefix of the test project in testlink. 
        /// If the projectName is not defined in testlink and this property is not set, 
        /// the first letter of the projectName will be used.
        /// </summary>
        public virtual string ProjectPrefix
        {
            get { return _projectPrefix; }
            set { _projectPrefix = value; }
        }

        private string _projectDescription = "Automated Test Project";

        /// <summary>
        /// The description of the test project in testlink. 
        /// </summary>
        public virtual string ProjectDescription
        {
            get { return _projectDescription; }
            set { _projectDescription = value; }
        }

        private string _userId;

        /// <summary>
        /// The user name to be used for creating new test project, test plan, 
        /// test suites, platforms and test cases (must conicide with the DevKey).
        /// Also the user must have administrative permission. 
        /// </summary>
        public virtual string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _devKey;

        /// <summary>
        /// The devkey or ApiKey for the above userid. provided by testlink
        /// </summary>
        public virtual string DevKey
        {
            get { return _devKey; }
            set { _devKey = value; }
        }

        private string _testPlanName = "Default-Test-Plan";

        /// <summary>
        /// The name of the test plan containing the test case results
        /// </summary>
        public virtual string TestPlanName
        {
            get { return _testPlanName; }
            set { _testPlanName = value; }
        }

        private string _testPlanDescription = "Automated Test Plan";

        /// <summary>
        /// The description of the test plan containing the test case results
        /// </summary>
        public virtual string TestPlanDescription
        {
            get { return _testPlanDescription; }
            set { _testPlanDescription = value; }
        }

        private string _buildName;

        /// <summary>
        /// The name of build for test plan in testlink.
        /// If this property is not set, the latest build name will be used and
        /// if there is not any build, 'Default-Build' will be used.
        /// </summary>
        public virtual string BuildName
        {
            get { return _buildName; }
            set { _buildName = value; }
        }

        private string _buildDescription;

        /// <summary>
        /// The description of build for test plan in testlink.
        /// If this property is not set, the latest build name will be used and
        /// if there is not any build, 'Automated Build' will be used.
        /// </summary>
        public virtual string BuildDescription
        {
            get { return _buildDescription; }
            set { _buildDescription = value; }
        }

        private string _testSuiteName;

        /// <summary>
        /// The name of the top level test suite where the test cases are expected.
        /// If this property is not set, The name of test class will be used.
        /// </summary>
        public virtual string TestSuiteName
        {
            get { return _testSuiteName; }
            set { _testSuiteName = value; }
        }

        private string _testSuiteDescription;

        /// <summary>
        /// The description of the top level test suite where the test cases are expected.
        /// If this property is not set, at first description of TestFixtureAttribute will be used and 
        /// if that property is not set, 'Automated Test Suite' will be used.
        /// </summary>
        public virtual string TestSuiteDescription
        {
            get { return _testSuiteDescription; }
            set { _testSuiteDescription = value; }
        }

        private string _platformName = "Default-Platform";

        /// <summary>
        /// The name of the platform that is linked to the test plan in testlink.
        /// If this property is not set or the platform is not linked to test plan, 
        /// all test cases will be created but non of them will be executed.
        /// </summary>
        public virtual string PlatformName
        {
            get { return _platformName; }
            set { _platformName = value; }
        }
    }
}