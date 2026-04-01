using Xunit.Abstractions;
using Xunit.Sdk;

namespace Application;

public class IntegrationTestDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Type", "Integration");
    }
}

public class UnitTestDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Type", "Unit");
    }
}

[TraitDiscoverer("Application.IntegrationTestDiscoverer", "Application")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class IntegrationTestAttribute : Attribute, ITraitAttribute
{
}

[TraitDiscoverer("Application.UnitTestDiscoverer", "Application")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class UnitTestAttribute : Attribute, ITraitAttribute
{
}
