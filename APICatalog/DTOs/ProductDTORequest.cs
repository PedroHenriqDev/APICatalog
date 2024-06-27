using System.ComponentModel.DataAnnotations;

namespace APICatalog.DTOs;

public class ProductDTORequest
{
    [Range(1, 9999, ErrorMessage = "The {0} must be from {1} and {2}")]
    public float Stock {  get; set; }
}
