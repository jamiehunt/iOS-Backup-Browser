namespace Unit_Tests
{
    using System;
    using iOS_Backup_Browser;
    using NUnit.Framework;

    public class ExtensionsTests
    {
        [Test]
        [TestCase(0, "1970-01-01")]
        [TestCase(1483228800, "2017-01-01")]
        [TestCase(32503593600, "2999-12-31")]
        [TestCase(1483360496, "2017-01-02 12:34:56")]
        [TestCase(2147483648, "2038-01-19 03:14:08")]
        public void UnixTimeStampToDateTimeTests(double unixTimeStamp, DateTime expectedDateTime)
        {
            var result = Extensions.UnixTimeStampToDateTime(unixTimeStamp);

            Assert.AreEqual(result, expectedDateTime);
        }
    }
}
