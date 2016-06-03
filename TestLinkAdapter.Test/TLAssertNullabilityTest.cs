using System;
using NUnit.Framework;
using NUnit.TestLink;

namespace TestLinkAdapter.Test
{
    [TestFixture(Description = "This Suite is collection of tests that try to validate all methods of TLAssert class that check argument nullability. " +
                               "So in this suite we try to test TestLinkAdapter class without initializing all attributes of TestLinkFixture.")]
    [TestLinkFixture(
        Url = "http://localhost/testlink/lib/api/xmlrpc/v1/xmlrpc.php",
        ProjectName = "NUnit-TestLink-Adapter-Test",
        ProjectPrefix = "NTAT",
        ProjectDescription = "Test Project that try to test all functionalities of nunit-testlink-adapter project. this project is work with nunit 2.6.4 and testlink 1.9.14",
        UserId = "your-admin-user",
        DevKey = "67aa0e00f605bfd00f78a31c9ed96fb8",
        TestPlanName = "NUnit-TestLink-Adapter-Test-Plan",
        TestPlanDescription = "In this plan we try to test all functionalities of TLAssert and TestLinkAdapter classes.",
        PlatformName = "NUnit-TestLink-Adapter-Platform")]
    // ReSharper disable once InconsistentNaming
    public class TLAssertNullabilityTest
    {
        [Test(Description = "Test of TLAssert.IsNull() method by passing null as argument; It is expected that the test will be passed whitout exception.")]
        public void IsNullByNullArgumentTest()
        {
            TLAssert.IsNull(null);
        }

        [Test(Description = "Test of TLAssert.IsNull() method by passing an object as argument; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void IsNullByObjectArgumentTest()
        {
            TLAssert.IsNull(new object());
        }

        [Test(Description = "Test of TLAssert.IsNotNull() method by passing an object as argument; It is expected that the test will be passed whitout exception.")]
        public void IsNotNullByObjectArgumentTest()
        {
            TLAssert.IsNotNull(new object());
        }

        [Test(Description = "Test of TLAssert.IsNotNull() method by passing null as argument; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void IsNotNullByNullArgumentTest()
        {
            TLAssert.IsNotNull(null);
        }
    }
}