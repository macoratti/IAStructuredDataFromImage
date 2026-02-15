using System.Globalization;

namespace IAStructuredDataFromImage.Shared;

public static class CultureNormalization
{
    public static void ApplyInvariantCulture()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
    }
}
