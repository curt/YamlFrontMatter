using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SevenKilo.YamlFrontMatter.UnitTests;

[TestClass]
public class TestYamlFrontMatter
{
    [TestMethod]
    [DataRow("", false, false)]
    [DataRow("---\n---\n", false, false)]
    [DataRow("content only", false, true)]
    [DataRow("---\nfront_matter: \"only\"\n---\n", true, false)]
    [DataRow("---\nfront_matter: \"also\"\n---\ncontent", true, true)]
    public void TestValidParser(string s, bool hasFrontMatter, bool hasContent)
    {
        var (fm, c) = new YamlFrontMatterParser().Parse<object>(s);

        if (hasFrontMatter)
        {
            Assert.IsNotNull(fm);
        }

        if (hasContent)
        {
            Assert.IsNotNull(c);
            Assert.IsFalse(string.IsNullOrEmpty(c));
        }
    }

    [TestMethod]
    [DataRow("", false, false)]
    [DataRow("---\n---\n", false, false)]
    [DataRow("content only", false, true)]
    [DataRow("---\nfront_matter: \"only\"\n---\n", true, false)]
    [DataRow("---\nfront_matter: \"also\"\n---\ncontent", true, true)]
    public void TestValidParserWithDeserializer(string s, bool hasFrontMatter, bool hasContent)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var (fm, c) = new YamlFrontMatterParser(deserializer).Parse<object>(s);

        if (hasFrontMatter)
        {
            Assert.IsNotNull(fm);
        }

        if (hasContent)
        {
            Assert.IsNotNull(c);
            Assert.IsFalse(string.IsNullOrEmpty(c));
        }
    }
}
