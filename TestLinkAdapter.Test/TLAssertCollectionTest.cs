using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using NUnit.Framework;
using NUnit.TestLink;

namespace TestLinkAdapter.Test
{
    [TestFixture(Description = "This Suite is collection of tests that try to validate all methods of TLAssert class that have callection argument. " +
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
    public class TLAssertCollectionTest
    {
        private readonly ICollection _sampleCollection = new ArrayList() {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};

        [Test(Description = "Test of TLAssert.Contains() method by passing one element of collection; It is expected that the test will be passed whitout exception.")]
        public void ContainsWhenContainsTest()
        {
            TLAssert.Contains(1, _sampleCollection);
        }

        [Test(Description = "Test of TLAssert.Contains() method by passing non of collection elements; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void ContainsWhenNotContainsTest()
        {
            TLAssert.Contains(100, _sampleCollection);
        }

        [Test(Description = "Test of TLAssert.IsEmpty() method by passing empty collection as argument; It is expected that the test will be passed whitout exception.")]
        public void IsEmptyByEmptyArgumentTest()
        {
            TLAssert.IsEmpty(new ArrayList());
        }

        [Test(Description = "Test of TLAssert.IsEmpty() method by passing not empty collection as argument; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void IsEmptyByNotEmptyArgumentTest()
        {
            TLAssert.IsEmpty(_sampleCollection);
        }

        [Test(Description = "Test of TLAssert.IsNotEmpty() method by passing not empty collection as argument; It is expected that the test will be passed whitout exception.")]
        public void IsNotEmptyByNotEmptyArgumentTest()
        {
            TLAssert.IsNotEmpty(_sampleCollection);
        }

        [Test(Description = "Test of TLAssert.IsNotEmpty() method by passing empty collection as argument; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void IsNotEmptyByEmptyArgumentTest()
        {
            TLAssert.IsNotEmpty(new ArrayList());
        }

        [Test(Description = "Test of TLAssert.IsEmpty() method by passing empty string as argument; It is expected that the test will be passed whitout exception.")]
        public void IsEmptyByEmptyStringArgumentTest()
        {
            TLAssert.IsEmpty("");
        }

        [Test(Description = "Test of TLAssert.IsEmpty() method by passing not empty string as argument; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void IsEmptyByNotEmptyStringArgumentTest()
        {
            TLAssert.IsEmpty("Test");
        }

        [Test(Description = "Test of TLAssert.IsNotEmpty() method by passing not empty string as argument; It is expected that the test will be passed whitout exception.")]
        public void IsNotEmptyByNotEmptyStringArgumentTest()
        {
            TLAssert.IsNotEmpty("Test");
        }

        [Test(Description = "Test of TLAssert.IsNotEmpty() method by passing empty string as argument; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void IsNotEmptyByEmptyStringArgumentTest()
        {
            TLAssert.IsNotEmpty("");
        }

        [Test(Description = "Test of TLAssert.IsNullOrEmpty() method by passing null as argument; It is expected that the test will be passed whitout exception.")]
        public void IsNullOrEmptyByNullArgumentTest()
        {
            TLAssert.IsNullOrEmpty(null);
        }

        [Test(Description = "Test of TLAssert.IsNullOrEmpty() method by passing empty string as argument; It is expected that the test will be passed whitout exception.")]
        public void IsNullOrEmptyByEmptyStringArgumentTest()
        {
            TLAssert.IsNullOrEmpty("");
        }

        [Test(Description = "Test of TLAssert.IsNullOrEmpty() method by passing not empty string as argument; It is expected that the test has an exception.")]
        [ExpectedException(typeof(AssertionException))]
        public void IsNullOrEmptyByNotEmptyStringArgumentTest()
        {
            TLAssert.IsNullOrEmpty("Test");
        }
    }
}