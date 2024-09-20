using YamlDotNet.Serialization;

namespace SevenKilo.YamlFrontMatter;

public class YamlFrontMatterParser
{
    private readonly IDeserializer _deserializer;

    public YamlFrontMatterParser()
    {
        _deserializer = new DeserializerBuilder().Build();
    }

    public YamlFrontMatterParser(IDeserializer deserializer)
    {
        _deserializer = deserializer;
    }

    public (T?, string?) Parse<T>(string str)
    {
        str = str.TrimStart();
        var parts = str.Split("---\n");
        var yaml = parts.Length > 1 ? parts[1] : null;
        var content = parts.Length > 2 ? parts[2] : parts.Length == 1 ? str : null;

        T? frontMatter = default;

        if (yaml != null)
        {
            frontMatter = _deserializer.Deserialize<T>(yaml);
        }

        return (frontMatter, content);
    }
}
