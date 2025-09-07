using Microsoft.Extensions.Configuration;

namespace Configuration.Substitution;

public class SubstitutionConfigurationSource(IConfigurationRoot configuration) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new SubstitutionConfigurationProvider(configuration);
    }
}