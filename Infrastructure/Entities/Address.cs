﻿namespace Infrastructure.Entities;

public class Address
{
    public string Country { get; set; } = string.Empty;
    
    public string City { get; set; } = string.Empty;
    
    public string Street { get; set; } = string.Empty;
    
    public string ZipCode { get; set; } = string.Empty;
}