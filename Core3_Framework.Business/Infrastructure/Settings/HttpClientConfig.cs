using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Core3_Framework.Business.Infrastructure.Settings
{
    public class RestClientSettings
    {
        public String BaseAddress { get; set; }
    }

    namespace System.Net.Http
    {
        public interface IRestClient
        {
            String BaseAddress { get; }

            HttpClient CreateClient(ClaimsPrincipal principal);
        }


        public class RestClientFactory : IRestClient
        {
            private readonly RestClientSettings settings;

            public String BaseAddress => settings.BaseAddress;


            public RestClientFactory(IOptions<RestClientSettings> options) : base()
            {
                settings = options.Value;
            }


            public HttpClient CreateClient(ClaimsPrincipal principal)
            {
                // Prepare client
                var result = new HttpClient() { BaseAddress = new Uri(BaseAddress) };

                result.DefaultRequestHeaders.Accept.Clear();
                result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Fetch JWT from user claims
                var jwt = principal.FindFirst("jwt")?.Value;

                // Add JWT to header for authentication and authorization
                result.DefaultRequestHeaders.Add("Authorization", $"{JwtBearerDefaults.AuthenticationScheme} {jwt}");
                result.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
                return result;
            }
        }
    }
}
