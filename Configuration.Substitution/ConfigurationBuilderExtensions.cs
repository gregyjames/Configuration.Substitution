using Microsoft.Extensions.Configuration;

namespace Configuration.Substitution;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationRoot EnableSubstitution(this IConfigurationBuilder builder)
    {
        var config = builder.Build();
        builder.Add(new SubstitutionConfigurationSource(config));
        return builder.Build();
    }
}