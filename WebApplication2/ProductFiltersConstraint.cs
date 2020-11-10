using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2
{
    public class ProductFiltersConstraint : IRouteConstraint, IOutboundParameterTransformer
    {
        private readonly string _rootPageName;
        private readonly IServiceProvider _serviceProvider;

        public ProductFiltersConstraint(string rootPageName, IServiceProvider serviceProvider)
        {
            _rootPageName = rootPageName;
            _serviceProvider = serviceProvider;
        }

        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (httpContext == null)
            {
                return false;
            }

            if (routeDirection != RouteDirection.IncomingRequest)
            {
                return true;
            }

            var value = values[routeKey] as string;
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var lookupService = httpContext.RequestServices.GetRequiredService<ILookupService>();
            var pathSegments = value.Split("/", StringSplitOptions.RemoveEmptyEntries);

            foreach (var pathSegment in pathSegments)
            {
                if (lookupService.CountryExists(pathSegment))
                {
                    continue;
                }

                if (lookupService.CategoryExists(pathSegment))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public string? TransformOutbound(object? value)
        {
            if (value is not ProductRouteContext context)
            {
                return value?.ToString();
            }

            // DI test:
            var configurationService = _serviceProvider.GetRequiredService<IConfigurationService>();
            using var scope = _serviceProvider.CreateScope();
            var lookupService = scope.ServiceProvider.GetRequiredService<ILookupService>();

            var pathSegments = new List<string>();

            if (context.Country != null)
            {
                pathSegments.Add(context.Country);
            }

            if (context.Category != null)
            {
                pathSegments.Add(context.Category);
            }

            if (!pathSegments.Any())
            {
                pathSegments.Add(_rootPageName);
            }

            var path = string.Join("/", pathSegments);

            // Add Support for to QueryString parameters transformation:
            var queryString = $"?q={context.SearchQuery}";

            // This will generate the following (encoded) value:
            // e.g. "Germany/Smartphone%3Fq%3Ddual%20sim"
            // Should be: "Germany/Smartphone?q=dual%20sim"
            return string.Concat(path, queryString);
        }
    }
}