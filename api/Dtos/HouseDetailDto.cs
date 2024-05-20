using System.ComponentModel.DataAnnotations;

public record HouseDetailDto(int Id, [property: Required]string? Address, 
[property: Required]string? Country, int Price, string? Description, string? Photo);