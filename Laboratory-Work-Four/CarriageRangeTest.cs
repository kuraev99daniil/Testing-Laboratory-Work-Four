using NUnit.Framework;
using System;

namespace Laboratory_Work_Four
{
    [TestFixture]
    class CarriageRangeTest
    {
		[Test]
		public void ParseTest()
        {
			Assert.Throws<ArgumentException>(() =>
			{
				new CarriageRange("^1.x,0", false);
			});

			Assert.Throws<ArgumentException>(() =>
			{
				new CarriageRange(">==3.1.4 <4.0.0", true);
			});

			Assert.Throws<ArgumentException>(() =>
			{
				new CarriageRange("^1.0.0", true);
			});

			Assert.Throws<ArgumentException>(() =>
			{
				new CarriageRange(">=3.1.4 <4.0.0", false);
			});
		}

		[Test]
		public void ContainTest()
		{
			Assert.IsTrue(new CarriageRange(">=3.1.4 <4.0.0", true).Contains(new Versioning("3.9.1")));
			Assert.IsTrue(new CarriageRange("^1", false).Contains(new Versioning("1.9.1")));
			Assert.IsTrue(new CarriageRange("^0.5.x", false).Contains(new Versioning("0.5.55555")));
			Assert.IsTrue(new CarriageRange("^0.0.0", false).Contains(new Versioning("0.0.1")));
			Assert.IsTrue(new CarriageRange(">=1.5.0 <2.0.0", true).Contains(new Versioning("1.9.9999999")));
			Assert.IsTrue(new CarriageRange("^0", false).Contains(new Versioning("0.0.9999999")));
		}

		[Test]
		public void ContaintRangeTest()
        {
			Assert.IsTrue(new CarriageRange(">=3.1.4 <4.0.0", true).Contains(new CarriageRange(">=3.1.4 <4.0.0", true)));
			Assert.IsTrue(new CarriageRange("^3", false).Contains(new CarriageRange("^3.5.1", false)));
			Assert.IsTrue(new CarriageRange("^4.x", false).Contains(new CarriageRange(">=4.1.4 <5.0.0", true)));
			Assert.IsTrue(new CarriageRange("^1.x", false).Contains(new CarriageRange(">=1.1.4 <2.0.0", true)));
			Assert.IsTrue(new CarriageRange("^0.1", false).Contains(new CarriageRange(">=0.1.4 <0.2.0", true)));
		}

		[Test]
		public void ToStringTest()
        {
			Assert.IsTrue(new CarriageRange(">=3.1.4 <4.0.0", true).ToString() == ">=3.1.4 <4.0.0");
			Assert.IsTrue(new CarriageRange("^1", false).ToString() == ">=1.0.0 <2.0.0");
			Assert.IsTrue(new CarriageRange("^0.1", false).ToString() == new CarriageRange(">=0.1.0 <0.2.0", true).ToString());
        }
	}
}
