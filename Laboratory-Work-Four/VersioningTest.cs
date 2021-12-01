using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory_Work_Four
{
	[TestFixture]
	class VersioningTest
    {
		[Test]
		public void MoreTest()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				_ = new Versioning("1.0,0-beta.alpha") > new Versioning("1.0.0-beta.1");
			});

			Assert.Throws<ArgumentException>(() =>
			{
				_ = new Versioning("QWERTY") > new Versioning("1.0.0-beta.1");
			});

			Assert.IsTrue(new Versioning("1.0.0-alpha.beta") > new Versioning("1.0.0-alpha.1"));
			Assert.IsTrue(new Versioning("1.0.0") > new Versioning("1.0.0-alpha"));
			Assert.IsTrue(new Versioning("1.0.1") > new Versioning("1.0.0"));
			Assert.IsTrue(new Versioning("1.1.0") > new Versioning("1.0.0"));
			Assert.IsTrue(new Versioning("1.1.1") > new Versioning("1.1.0"));

			Assert.IsFalse(new Versioning("0.0.9-alpha.5") > new Versioning("0.10.0-beta.10.5"));
			Assert.IsFalse(new Versioning("4.1.9-1") > new Versioning("5.9.0-5"));
			Assert.IsFalse(new Versioning("7.1.4-1") > new Versioning("7.1.5-1"));
			Assert.IsFalse(new Versioning("0.1.9-1") > new Versioning("0.2.0-5"));
		}

		[Test]
		public void LessTest()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				_ = new Versioning("?.!,0-beta.alpha") > new Versioning("1.0.0-alpha.1");
			});

			Assert.IsTrue(new Versioning("1.0.0-beta.1") < new Versioning("1.0.0-beta.alpha"));
			Assert.IsTrue(new Versioning("5.5.4-beta.1") < new Versioning("5.5.5-beta.alpha"));
			Assert.IsTrue(new Versioning("0.1.1-beta.1") < new Versioning("0.1.2-beta.alpha"));

			Assert.IsFalse(new Versioning("54.48.89") < new Versioning("54.48.80-alpha"));
			Assert.IsFalse(new Versioning("8.14.16") < new Versioning("8.14.15"));
			Assert.IsFalse(new Versioning("1.0.8-beta.1") < new Versioning("1.0.8-beta.0"));
		}

		[Test]
		public void MoreOrEqualTest()
		{
			Assert.IsTrue(new Versioning("1.1.0") >= new Versioning("1.0.0"));
			Assert.IsTrue(new Versioning("4.6.7") >= new Versioning("4.6.0"));
			Assert.IsTrue(new Versioning("5.5.5") >= new Versioning("5.5.5-alpha"));

			Assert.IsFalse(new Versioning("5.10.45") >= new Versioning("5.10.46"));
			Assert.IsFalse(new Versioning("40.50.40") >= new Versioning("40.50.45"));
			Assert.IsFalse(new Versioning("10.1.5-alpha.54.1") >= new Versioning("10.1.5-alpha.54.beta"));
		}

		[Test]
		public void LessOrEqualTest()
		{
			Assert.IsTrue(new Versioning("1.0.0") <= new Versioning("1.1.0"));
			Assert.IsTrue(new Versioning("4.8.0") <= new Versioning("4.9.1"));
			Assert.IsTrue(new Versioning("40.40.40-alpha") <= new Versioning("40.40.40"));
			Assert.IsTrue(new Versioning("0.1.0-alpha") <= new Versioning("0.1.0-alpha"));

			Assert.IsFalse(new Versioning("5.8.1-alpha") <= new Versioning("4.8.7-alpha"));
			Assert.IsFalse(new Versioning("4.1.7-alpha.5") <= new Versioning("4.1.7-alpha.1"));
			Assert.IsFalse(new Versioning("50.80.1") <= new Versioning("50.80.1-alpha"));
		}

		[Test]
		public void EqualTest()
		{
			Assert.IsTrue(new Versioning("0.0.0") == new Versioning("0.0.0"));
			Assert.IsTrue(new Versioning("5.1.1") == new Versioning("5.1.1"));
			Assert.IsTrue(new Versioning("1.1.1-beta") == new Versioning("1.1.1-beta"));

			Assert.IsFalse(new Versioning("50.10.8") == new Versioning("50.10.9"));
			Assert.IsFalse(new Versioning("40.88.99-alpha") == new Versioning("40.88.99-alpha.5"));
			Assert.IsFalse(new Versioning("80.10.7-alpha") == new Versioning("80.10.7"));
		}

		[Test]
		public void NoEqualTest()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				bool metka = new Versioning("1.0,0-beta.alpha") > new Versioning("qwerty");
			});

			Assert.IsTrue(new Versioning("1.0.0") != new Versioning("1.0.1"));
			Assert.IsTrue(new Versioning("5.1.5-alpha") != new Versioning("5.1.5"));
			Assert.IsTrue(new Versioning("10.44.1-alpha.beta") != new Versioning("10.44.1-alpha.beta.1"));

			Assert.IsFalse(new Versioning("1.0.0") != new Versioning("1.0.0"));
			Assert.IsFalse(new Versioning("10.5.1-aplha.beta") != new Versioning("10.5.1-aplha.beta"));
			Assert.IsFalse(new Versioning("8.7.4-1") != new Versioning("8.7.4-1"));
		}

		[Test]
		public void ToStringTest()
		{
			Assert.AreEqual("1.0.0", new Versioning("1.0.0").ToString());
			Assert.AreEqual("1.1.1-alpha", new Versioning("1.1.1-alpha").ToString());
			Assert.AreEqual("10.50.1-alpha.beta.1", new Versioning("10.50.1-alpha.beta.1").ToString());
		}
	}
}
