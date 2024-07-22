﻿using Infrastructure.Validations;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CategoryDTORequest
{
    [StringLength(80, MinimumLength = 1, ErrorMessage = "The {0} must be {1} to {2} characters long")]
    public string? Name { get; set; }

    [IsImageUrl]
    [StringLength(300, MinimumLength = 1, ErrorMessage = "The {0} must be {1} to {2} characters")]
    public string? ImageUrl { get; set; }
}
