﻿using Business.Services;
using Business.Services.Interfaces;
using Business.Validators;
using Business.Validators.Interfaces;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class ServiceCollectionExtensions
{
    public static void AddBusiness(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IClientService, ClientService>();

        serviceCollection.AddScoped<IMobileNumberValidator, MobileNumberValidator>();
        serviceCollection.AddScoped<IUserValidator, UserValidator>();
        serviceCollection.AddScoped<IClientValidator, ClientValidator>();
        serviceCollection.AddScoped<IAddressValidator, AddressValidator>();
    }
}