using Core.DTOs.MobileNumber;
using Core.Interfaces;
using PhoneNumbers;

namespace Business.Validators;

public class MobileNumberValidator : IMobileNumberValidator
{
    public bool ValidateMobileNumber(MobileNumberDto mobileNumberDto)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();

        try
        {
            var parsedMobileNumber = phoneNumberUtil.Parse(mobileNumberDto.Number, mobileNumberDto.Region);

            return phoneNumberUtil.IsValidNumber(parsedMobileNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return false;
        }
    }
}