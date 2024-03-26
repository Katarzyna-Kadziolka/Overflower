﻿using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Overflower.Api.Configuration.Swagger;

// REF: https://github.com/dotnet/aspnet-api-versioning/blob/main/examples/AspNetCore/WebApi/OpenApiExample/SwaggerDefaultValues.cs
public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply( OpenApiOperation operation, OperationFilterContext context )
    {
        var apiDescription = context.ApiDescription;
        operation.Deprecated |= apiDescription.IsDeprecated();
        
        foreach ( var responseType in context.ApiDescription.SupportedResponseTypes )
        {
            var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
            var response = operation.Responses[responseKey];

            foreach ( var contentType in response.Content.Keys )
            {
                if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                {
                    response.Content.Remove( contentType );
                }
            }
        }

        if ( operation.Parameters == null )
        {
            return;
        }
        
        foreach ( var parameter in operation.Parameters )
        {
            var description = apiDescription.ParameterDescriptions.First( p => p.Name == parameter.Name );
            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            parameter.Description = description.ModelMetadata?.Description;

            if ( parameter.Schema.Default == null &&
                 description.DefaultValue != null &&
                 description.DefaultValue is not DBNull &&
                 description.ModelMetadata is not null)
            {
                var json = JsonSerializer.Serialize( description.DefaultValue, description.ModelMetadata.ModelType );
                parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson( json );
            }

            parameter.Required |= description.IsRequired;
        }
    }
}