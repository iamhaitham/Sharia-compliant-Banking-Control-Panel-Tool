using System.ComponentModel.DataAnnotations;
using Core.DTOs.MobileNumber;
using Core.Interfaces;
using Core.Utilities;

namespace Core.CustomAttributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class MobileNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not MobileNumberDto)
        {
            return new ValidationResult(
                CustomErrorMessage.IncompatibleTypeMessage(
                    nameof(MobileNumberDto)
                )
            );
        }

        var mobileNumberValidator = (IMobileNumberValidator?)validationContext.GetService(typeof(IMobileNumberValidator));

        if (mobileNumberValidator is null)
        {
            return new ValidationResult(
                CustomErrorMessage.CouldNotResolveDependency(
                    nameof(IMobileNumberValidator)
                )
            );
        }

        return mobileNumberValidator.ValidateMobileNumber((MobileNumberDto)value)
            ? ValidationResult.Success
            : new ValidationResult(
                CustomErrorMessage.InvalidMobileNumberFormat()
            );
    }
}