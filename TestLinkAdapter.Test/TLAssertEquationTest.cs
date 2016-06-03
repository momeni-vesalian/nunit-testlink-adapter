using System;
using NUnit.Framework;
using NUnit.TestLink;

namespace TestLinkAdapter.Test
{
    [TestFixture(Description = "This Suite is collection of tests that try to validate all methods of TLAssert class that check argument equation.")]
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
        PlatformName = "NUnit-TestLink-Adapter-Platform")]
    // ReSharper disable once InconsistentNaming
    public class TLAssertEquationTest
    {
        private readonly object _sampleObject = new object();
        private readonly object _anotherObject = new object();

        [Test(Description = "Test of TLAssert.AreEqual() method by passing equal arguments; It is expected that the test will be passed whitout exception.")]
        public void AreEqualByEqualArgumentsTest()
        {
            TLAssert.AreEqual(1, 1);
        }

        [Test(Description = "Test of TLAssert.AreEqual() method by passing not equal arguments; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void AreEqualByNotEqualArgumentsTest()
        {
            TLAssert.AreEqual(1, 2);
        }

        [Test(Description = "Test of TLAssert.AreNotEqual() method by passing not equal arguments; It is expected that the test will be passed whitout exception.")]
        public void AreNotEqualByNotEqualArgumentsTest()
        {
            TLAssert.AreNotEqual(1, 2);
        }

        [Test(Description = "Test of TLAssert.AreNotEqual() method by passing equal arguments; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void AreNotEqualByEqualArgumentsTest()
        {
            TLAssert.AreNotEqual(1, 1);
        }

        [Test(Description = "Test of TLAssert.AreSame() method by passing same arguments; It is expected that the test will be passed whitout exception.")]
        public void AreSameBySameArgumentsTest()
        {
            TLAssert.AreSame(_sampleObject, _sampleObject);
        }

        [Test(Description = "Test of TLAssert.AreSame() method by passing not same arguments; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void AreSameByNotSameArgumentsTest()
        {
            TLAssert.AreSame(_sampleObject, _anotherObject);
        }

        [Test(Description = "Test of TLAssert.AreNotSame() method by passing not same arguments; It is expected that the test will be passed whitout exception.")]
        public void AreNotSameByNotSameArgumentsTest()
        {
            TLAssert.AreNotSame(_sampleObject, _anotherObject);
        }

        [Test(Description = "Test of TLAssert.AreNotSame() method by passing same arguments; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void AreNotSameBySameArgumentsTest()
        {
            TLAssert.AreNotSame(_sampleObject, _sampleObject);
        }
    }
}