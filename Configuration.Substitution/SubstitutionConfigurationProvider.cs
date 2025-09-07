using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Configuration.Substitution;

public class SubstitutionConfigurationProvider : ConfigurationProvider, IDisposable
{
    private readonly IConfigurationRoot _configuration;
    private IDisposable? _changeSubscription;

    public SubstitutionConfigurationProvider(IConfigurationRoot configuration)
    {
        _configuration = configuration;
        _changeSubscription = ChangeToken.OnChange(configuration.GetReloadToken, OnConfigurationReload);
    }

    private void OnConfigurationReload()
    {
        Load();
        OnReload();
    }

    private static readonly Regex PlaceholderRegex = new(@"\$\{([^\}]+)\}", RegexOptions.Compiled);

    public override void Load()
    {
        var dict = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        
        foreach (var kvp in _configuration.AsEnumerable())
        {
            if (kvp.Value is null) continue;
            dict[kvp.Key] = SubstitutePlaceholder(kvp.Value);
        }
        
        Data = dict;
    }

    private string SubstitutePlaceholder(string placeholder)
    {
        return PlaceholderRegex.Replace(placeholder, match =>
        {
            var key = match.Groups[1].Value;
            return _configuration[key] ?? match.Value;
        });
    }

    public void Dispose()
    {
        _changeSubscription?.Dispose();
        _changeSubscription = null;
    }
}
