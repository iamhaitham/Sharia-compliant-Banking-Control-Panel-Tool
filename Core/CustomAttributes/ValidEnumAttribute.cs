using System.ComponentModel.DataAnnotations;
using Core.Utilities;

namespace Core.CustomAttributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ValidEnumAttribute : ValidationAttribute
{
    private Type _enumType;

    public ValidEnumAttribute(Type enumType)
    {
        _enumType = enumType;
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return new ValidationResult(
                CustomErrorMessage.CannotBeNull(
                    _enumType.Name
                )
            );
        }

        return Enum.IsDefined(_enumType, value)
            ? ValidationResult.Success
            : new ValidationResult(
                CustomErrorMessage.CouldNotResolveType(
                    _enumType.Name
                )
            );
    }
}