using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Laboratory_Work_Four
{
    [TestFixture]
    class CarriageRangeTest
    {
		[Test]
		public void IsCorrected()
		{
			var carriageRange = new CarriageRange(">=3.1.4 <4.0.0", true);

			Assert.IsTrue(carriageRange.Contains(new Versioning("3.1.5")));

			Assert.IsTrue(carriageRange.Contains(new CarriageRange(">=3.1.5 <4.0.0", true)));

			Assert.IsTrue(carriageRange.ToString() == ">=3.1.4 <4.0.0");
		}
	}
}
