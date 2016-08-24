using System.Linq;
using System.Xml.XPath;
using NUnit.Framework;

namespace GrafGenerator.ResxLocalize.Tests
{
    [TestFixture]
    public class ResxLocalizerFixture
    {
        [Test]
        public void ShouldBeAbleToChangePresentKey()
        {
            var keyName = "presentKey";

            var doc = ResxLocalizeCommand.ResxLocalizer.Localize(Input.XDoc.Sample, Input.JData.Sample, "en", new []{"ru"}).FirstOrDefault()?.Item2;
            Assert.That(doc, Is.Not.Null);

            var value = doc.XPathSelectElement($"/root/data[@name='{keyName}']/value");
            Assert.That(value, Is.Not.Null);

            Assert.That(value.Value, Is.EqualTo("changed_ru"));
        }

        [Test]
        public void ShouldNotChangeMissingKey()
        {
            var keyName = "missingKey";

            var doc = ResxLocalizeCommand.ResxLocalizer.Localize(Input.XDoc.Sample, Input.JData.Sample, "en", new[] { "ru" }).FirstOrDefault()?.Item2;
            Assert.That(doc, Is.Not.Null);

            var value = doc.XPathSelectElement($"/root/data[@name='{keyName}']/value");
            Assert.That(value, Is.Not.Null);

            Assert.That(value.Value, Is.EqualTo("missing"));
        }


        [Test]
        public void ShouldBeAbleToChangeToSourceKeyString()
        {
            var keyName = "presentKey";

            var doc = ResxLocalizeCommand.ResxLocalizer.Localize(Input.XDoc.Sample, Input.JData.Sample, "en", new[] { "en" }).FirstOrDefault()?.Item2;
            Assert.That(doc, Is.Not.Null);

            var value = doc.XPathSelectElement($"/root/data[@name='{keyName}']/value");
            Assert.That(value, Is.Not.Null);

            Assert.That(value.Value, Is.EqualTo("present"));
        }

        [Test]
        public void ShouldNotChangeIfTargetKeyNotFound()
        {
            var keyName = "presentKey";

            var doc = ResxLocalizeCommand.ResxLocalizer.Localize(Input.XDoc.Sample, Input.JData.Sample, "en", new[] { "nonexistingkey" }).FirstOrDefault()?.Item2;
            Assert.That(doc, Is.Not.Null);

            var value = doc.XPathSelectElement($"/root/data[@name='{keyName}']/value");
            Assert.That(value, Is.Not.Null);

            Assert.That(value.Value, Is.EqualTo("present"));
        }
    }
}
