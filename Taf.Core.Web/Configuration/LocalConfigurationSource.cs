using Microsoft.Extensions.Configuration.Json;

namespace Taf.Core.Web;

/// <summary>
/// 
/// </summary>
public class LocalConfigurationSource : JsonConfigurationSource{
    /// <summary>
    /// Builds the <see cref="JsonConfigurationProvider"/> for this source.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
    /// <returns>A <see cref="JsonConfigurationProvider"/></returns>
    public override IConfigurationProvider Build(IConfigurationBuilder builder){
        EnsureDefaults(builder);
        return new LcocalConfigurationProvider(this);
    }
}