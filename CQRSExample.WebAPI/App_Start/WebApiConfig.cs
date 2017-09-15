using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CQRSExample.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serialization config
            config.Formatters.Add(new BrowserJsonFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCaseExceptDictionaryKeysResolver();
            // CORS Config
            config.SetCorsPolicyProviderFactory(new CorsPolicyFactory());
            config.EnableCors();
            // API routing config
            config.MapHttpAttributeRoutes();
        }

        private class CamelCaseExceptDictionaryKeysResolver : CamelCasePropertyNamesContractResolver
        {
            protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
            {
                JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);

                contract.DictionaryKeyResolver = propertyName => propertyName;

                return contract;
            }
        }

        private class BrowserJsonFormatter : JsonMediaTypeFormatter
        {
            public BrowserJsonFormatter()
            {
                SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }

        private class CorsPolicyFactory : ICorsPolicyProviderFactory
        {
            private ICorsPolicyProvider _provider = new CqrsExampleCORSPolicyProvider();

            public ICorsPolicyProvider GetCorsPolicyProvider(HttpRequestMessage request)
            {
                return _provider;
            }
        }

        private class CqrsExampleCORSPolicyProvider : ICorsPolicyProvider
        {
            private CorsPolicy _policy;

            public CqrsExampleCORSPolicyProvider()
            {
                _policy = new CorsPolicy
                {
                    AllowAnyMethod = true,
                    AllowAnyHeader = true,
                    SupportsCredentials = true
                };
                // TODO read from config file
                // Add allowed origins.
                _policy.Origins.Add("http://localhost:4200");
            }

            public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_policy);
            }
        }
    }
}