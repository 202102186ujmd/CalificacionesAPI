using Microsoft.AspNetCore.Mvc;

namespace WebApiPostulacion.Authentication
{
    public class ApikeyAttribute : ServiceFilterAttribute
    {
        public ApikeyAttribute() : base(typeof(ApiKeyAuthorizationFilter))
        {
            
        }
    }
}
