namespace WebApplication2
{
    public interface IConfigurationService
    {
        string GetSetting(string key);
    }

    public class ConfigurationService : IConfigurationService
    {
        public string GetSetting(string key)
        {
            return key;
        }
    }
}