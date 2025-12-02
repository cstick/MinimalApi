using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Web.APIs;

internal sealed class DeprecatedOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation is null || context is null)
        {
            return;
        }

        var apiDescription = context.ApiDescription;

        if (apiDescription.IsDeprecated())
        {
            operation.Deprecated = true;

            operation.Description ??= string.Empty;

            if (!operation.Description.Contains("Deprecated", StringComparison.OrdinalIgnoreCase))
            {
                operation.Description = $"{operation.Description} (Deprecated)".Trim();
            }
        }
    }
}
