using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace CIPlatformWeb.Utilities;

public class CleanDataModelBinder: IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // Get the model type from the binding context
        var modelType = bindingContext.ModelType;

        // Create an instance of the model type
        var model = Activator.CreateInstance(modelType);

        // Loop through all properties of the model type
        foreach (var property in modelType.GetProperties())
        {
            // Get the value of the property from the request
            var valueProviderResult = bindingContext.ValueProvider.GetValue(property.Name);
            if (valueProviderResult == ValueProviderResult.None)
            {
                continue;
            }

            var value = valueProviderResult.FirstValue;
           
            // Clean the value and set the property on the model
            if (property.PropertyType == typeof(string))
            {
                if (property.Name.ToLower() == "email")
                {
                    value = value?.ToLower();
                }
                var cleanedValue = CleanData(value);
                property.SetValue(model, cleanedValue);
            }
            else if (property.PropertyType.IsNullableType())
            {
                if (string.IsNullOrEmpty(value))
                {
                    // If the value is null or empty, set the property to null
                    property.SetValue(model, null);
                }
                else
                {
                    // Get the underlying type of the nullable property
                    var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);

                    try
                    {
                        // Attempt to convert the value to the underlying type
                        var convertedValue = Convert.ChangeType(value, underlyingType);
                        property.SetValue(model, convertedValue);
                    }
                    catch (Exception ex)
                    {
                        bindingContext.ModelState.AddModelError(property.Name, ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(model, convertedValue);
                }
                catch (Exception ex)
                {
                    bindingContext.ModelState.AddModelError(property.Name, ex.Message);
                }
            }
        }

        // Set the result in the binding context
        bindingContext.Result = ModelBindingResult.Success(model);

        return Task.CompletedTask;
    }

    private static string? CleanData(string? value)
    {
        if (value.IsNullOrEmpty()) return value;

        string? cleanedValue = value?.Trim();

        return cleanedValue;
    }

}
