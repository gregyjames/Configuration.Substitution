using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Configuration.Substitution;

public class SubstitutionConfigurationProvider: ConfigurationProvider
{
    private readonly IConfigurationRoot _configuration;
    private static readonly Regex PlaceholderRegex = new(@"\$\{([^\}]+)\}", RegexOptions.Compiled);
    public SubstitutionConfigurationProvider(IConfigurationRoot configuration)
    {
        _configuration = configuration;
    }

    public override void Load()
    {
        foreach (var kvp in _configuration.AsEnumerable())
        {
            if (kvp.Value is not null)
            {
                var resolved = SubstitutePlaceholder(kvp.Value);
                Data[kvp.Key] = resolved;
            }
        }
    }

    private string SubstitutePlaceholder(string placeholder)
    {
        return PlaceholderRegex.Replace(placeholder, match =>
        {
            var key = match.Groups[1].Value;
            return _configuration[key] ?? match.Value;
        });
    }
}
