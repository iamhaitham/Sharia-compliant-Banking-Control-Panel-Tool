using Core.DTOs;

namespace Core.Interfaces;

public interface IMobileNumberValidator
{
    public bool ValidateMobileNumber(MobileNumberDto mobileNumberDto);
}