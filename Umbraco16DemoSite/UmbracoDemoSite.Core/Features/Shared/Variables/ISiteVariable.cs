namespace UmbracoDemoSite.Core.Features.Shared.Variables
{
    public interface ISiteVariable
    {
        T Get<T>(string alias, T fallback);
    }
}