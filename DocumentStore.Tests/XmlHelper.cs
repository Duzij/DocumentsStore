namespace DocumentStore.Tests
{
    public static class XmlHelper
    {
        public static string NormalizeXml(this string xmlString) => xmlString.Replace(" ", "").ReplaceLineEndings("");
    }
}
