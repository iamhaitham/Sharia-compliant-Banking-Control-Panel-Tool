using Core.DTOs.MobileNumber;

namespace Core.Interfaces;

public interface IMobileNumberValidator
{
    public bool ValidateMobileNumber(MobileNumberDto mobileNumberDto);
}