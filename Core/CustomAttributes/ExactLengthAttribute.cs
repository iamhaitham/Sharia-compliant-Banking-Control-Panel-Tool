using System.ComponentModel.DataAnnotations;
using Core.Utilities;

namespace Core.CustomAttributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ExactLengthAttribute : ValidationAttribute
{
    private readonly int _exactLength;

    public ExactLengthAttribute(int exactLength)
    {
        _exactLength = exactLength;
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string)
        {
            return new ValidationResult(
                CustomErrorMessage.IncompatibleTypeMessage(
                    nameof(String)
                )
            );
        }

        return ((string)value).Length == _exactLength
            ? ValidationResult.Success
            : new ValidationResult(
                CustomErrorMessage.ExactStringLengthNotSatisfied(
                    _exactLength
                )
            );
    }
}