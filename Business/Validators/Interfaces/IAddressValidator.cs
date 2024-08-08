using Core.DTOs;
using Infrastructure.Entities;

namespace Business.Validators.Interfaces;

public interface IAddressValidator : IUniquenessValidator<AddressDto, Address>
{
    
}