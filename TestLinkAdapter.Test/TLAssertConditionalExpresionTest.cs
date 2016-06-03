using System;
using NUnit.Framework;
using NUnit.TestLink;

namespace TestLinkAdapter.Test
{
    [TestFixture(Description = "According to goal of the suite, this description should not be used for creation of test suite.")]
    [TestLinkFixture(
        Url = "http://localhost/testlink/lib/api/xmlrpc/v1/xmlrpc.php",
        ProjectName = "NUnit-TestLink-Adapter-Test",
        ProjectPrefix = "NTAT",
        ProjectDescription = "Test Project that try to test all functionalities of nunit-testlink-adapter project. this project is work with nunit 2.6.4 and testlink 1.9.14",
        UserId = "your-admin-user",
        DevKey = "67aa0e00f605bfd00f78a31c9ed96fb8",
        TestPlanName = "NUnit-TestLink-Adapter-Test-Plan",
        TestPlanDescription = "In this plan we try to test all functionalities of TLAssert and TestLinkAdapter classes.",
        BuildName = "NUnit-TestLink-Adapter-Build",
        BuildDescription = "This is default build.",
        TestSuiteName = "TLAssert-Conditional-Expresion-Test",
        TestSuiteDescription = "This Suite is collection of tests that try to validate all methods of TLAssert class with boolean argument. " +
                               "So in this suite we try to test TestLinkAdapter class by initializing all attributes of TestLinkFixture.",
        PlatformName = "NUnit-TestLink-Adapter-Platform")]
    // ReSharper disable once InconsistentNaming
    public class TLAssertConditionalExpresionTest
    {
        [Test(Description = "Test of TLAssert.IsTrue() method by passing true as argument; It is expected that the test will be passed whitout exception.")]
        public void IsTrueByTrueArgumentTest()
        {
            TLAssert.IsTrue(true);
        }

        [Test(Description = "Test of TLAssert.IsTrue() method by passing false as argument; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void IsTrueByFalseArgumentTest()
        {
            TLAssert.IsTrue(false);
        }

        [Test(Description = "Test of TLAssert.IsFalse() method by passing false as argument; It is expected that the test will be passed whitout exception.")]
        public void IsFalseByFalseArgumentTest()
        {
            TLAssert.IsFalse(false);
        }

        [Test(Description = "Test of TLAssert.IsFalse() method by passing true as argument; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void IsFalseByTrueArgumentTest()
        {
            TLAssert.IsFalse(true);
        }
    }
}