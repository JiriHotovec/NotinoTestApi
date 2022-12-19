namespace DocumentService.Web.Dtos;

public record DocumentDto(string Id, string[] Tags, PersonDto Data);