using Core.DTOs.Address;
using Infrastructure.Entities;

namespace Business.Validators.Interfaces;

public interface IAddressValidator : IUniquenessValidator<AddressDto, Address>
{
    
}