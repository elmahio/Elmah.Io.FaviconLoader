using System;
using NUnit.Framework;

namespace Elmah.Io.FaviconLoader.Tests
{
    public class FaviconTest
    {
        [Test]
        public void CanGetFaviconIco()
        {
            var favicon = new Favicon();
            var faviconUrl = favicon.Load(new Uri("https://elmah.io"));
            Assert.That(faviconUrl != null);
            Assert.That(faviconUrl.ToString(), Is.EqualTo("https://elmah.io/favicon.ico"));
        }
    }
}
