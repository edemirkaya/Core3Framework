﻿using Core3_Framework.Business.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;

namespace Core3_Framework.Business.Infrastructure.Settings
{
    public class JwtTokenIssuerSettings
    {
        public String BaseAddress { get; set; }
        public String Login { get; set; }
        public String RenewToken { get; set; }
    }

    public interface IJwtTokenIssuerSettings
    {
        String BaseAddress { get; }
        String Login { get; }
        String RenewToken { get; }
    }

    public class JwtTokenIssuerSettingsFactory : IJwtTokenIssuerSettings
    {
        private readonly JwtTokenIssuerSettings settings;

        public String BaseAddress => settings.BaseAddress;
        public String Login => settings.Login;
        public String RenewToken => settings.RenewToken;

        public JwtTokenIssuerSettingsFactory(IOptions<JwtTokenIssuerSettings> options)
        {
            settings = options.Value;
        }
    }
}


namespace System.Net.Http
{
    public class HttpRestClient : HttpClient
    {
        public HttpRestClient(IOptions<RestClientSettings> options) : base()
        {
            BaseAddress = new Uri(options.Value.BaseAddress);

            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}