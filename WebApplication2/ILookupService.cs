namespace WebApplication2
{
    public interface ILookupService
    {
        bool CountryExists(string pageName);

        bool CategoryExists(string pageName);
    }

    public class LookupService : ILookupService
    {
        public bool CountryExists(string pageName) => pageName == "Germany";

        public bool CategoryExists(string pageName) => pageName == "Smartphone";
    }
}