using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CIPlatformWeb.Utilities;

public class CleanDataModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Check if the model type is a class
        if (context.Metadata.IsComplexType)
        {
            return new CleanDataModelBinder();
        }

        return null;
    }
}
