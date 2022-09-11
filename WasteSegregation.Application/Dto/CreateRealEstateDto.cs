﻿using System.ComponentModel.DataAnnotations;

namespace WasteSegregation.Application.Dto;

public class CreateRealEstateDto
{
    [Required]
    [MaxLength(30)]
    public string Street { get; set; }
    [Required]
    [MaxLength(10)]
    public string StreetNumber { get; set; }
    [Required]
    [MaxLength(6)]
    public string PostCode { get; set; }
    [Required]
    [MaxLength(20)]
    public string City { get; set; }
}
