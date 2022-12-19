namespace DocumentService.Web.Dtos;

public record PersonDto(string FirstName, string LastName, DateTimeOffset? DateOfBirth);