using System.Net;
using Business.Validators.Interfaces;
using Core.DTOs.Address;
using Core.Utilities;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Business.Validators;

public class AddressValidator : IAddressValidator
{
    private readonly IAddressRepository _addressRepository;

    public AddressValidator(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<GenericResponse<Address>> IsUnique(AddressDto requestDto)
    {
        Address? addressFromDatabase;

        try
        {
            addressFromDatabase = await _addressRepository.GetByFilter(a =>
                a.Country == requestDto.Country
                && a.City == requestDto.City
                && a.Street == requestDto.Street
                && a.ZipCode == requestDto.ZipCode
            );
        }
        catch (Exception ex)
        {
            return new GenericResponse<Address>()
            {
                Error = ex.Message,
                IsSuccessful = false,
                HttpCode = HttpStatusCode.InternalServerError
            };
        }

        if (addressFromDatabase is not null)
        {
            return new GenericResponse<Address>()
            {
                Body = addressFromDatabase,
                IsSuccessful = true,
                HttpCode = HttpStatusCode.Conflict
            };
        }
        
        return new GenericResponse<Address>()
        {
            IsSuccessful = true
        };
    }
}