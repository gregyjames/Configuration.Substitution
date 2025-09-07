using Microsoft.Extensions.Configuration;

namespace Configuration.Substitution;

public class SubstitutionConfigurationSource: IConfigurationSource
{
    private readonly IConfigurationRoot _configuration;

    public SubstitutionConfigurationSource(IConfigurationRoot configuration)
    {
        _configuration = configuration;
    }
    
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new SubstitutionConfigurationProvider(_configuration);
    }
}