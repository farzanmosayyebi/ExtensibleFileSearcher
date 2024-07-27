
namespace ExtensionPlatform
{
    public interface IExtension : ISearcher
    {
        public string Author {  get; set; }
        public string Name { get; set; }

    }
}
