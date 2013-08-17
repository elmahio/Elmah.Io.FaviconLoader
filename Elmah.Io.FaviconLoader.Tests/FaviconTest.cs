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
            var faviconUrl = favicon.Load(new Uri("http://elmahio.azurewebsites.net"));
            Assert.That(faviconUrl != null);
            Assert.That(faviconUrl.ToString(), Is.EqualTo("http://elmahio.azurewebsites.net/favicon.ico"));
        }
    }
}
