namespace WebApiPostulacion.Authentication
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        protected readonly IConfiguration _configuration;

        public ApiKeyValidator(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public bool IsValid(string apiKey)
        {
            if(apiKey == _configuration.GetSection("ApiKey").Value)
            {
                return true;
            }
            return false;
        }
    }

    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}
