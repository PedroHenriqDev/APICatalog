using System.ComponentModel.DataAnnotations;

namespace Communication.DTOs.Requests;

public class RequestProductDTO
{
    [Range(1, 9999, ErrorMessage = "The {0} must be from {1} and {2}")]
    public float Stock { get; set; }
}
