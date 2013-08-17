Elmah.Io.FaviconLoader
================

Nice little tool for getting the URL of the favicon for any website.

How to use it:

    [Test]
    public void CanGetFaviconIco()
    {
        var favicon = new Favicon();
        var faviconUrl = favicon.Load(new Uri("http://elmahio.azurewebsites.net"));
        Assert.That(faviconUrl != null);
        Assert.That(faviconUrl.ToString(), Is.EqualTo("http://elmahio.azurewebsites.net/favicon.ico"));
    }
