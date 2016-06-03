using System;
using NUnit.Framework;
using NUnit.TestLink;

namespace TestLinkAdapter.Test
{
    /// <summary>
    /// This Suite is collection of tests that try to test TestLinkAdapter class without initializing all attributes of TestLinkFixture.
    /// </summary>
    [TestFixture]
    [TestLinkFixture(
        Url = "http://localhost/testlink/lib/api/xmlrpc/v1/xmlrpc.php",
        ProjectName = "NU",
        UserId = "your-admin-user",
        DevKey = "67aa0e00f605bfd00f78a31c9ed96fb8",
        PlatformName = "NU-Platform")]
    // ReSharper disable once InconsistentNaming
    public class TestLinkAdapterDefaultValuesTest
    {
        /// <summary>
        /// Test default value of TestLinkFixture attributes.
        /// </summary>
        [Test]
        public void TestLinkFixtureDefaultValueTest()
        {
            TLAssert.IsTrue(true);
        }
    }
}