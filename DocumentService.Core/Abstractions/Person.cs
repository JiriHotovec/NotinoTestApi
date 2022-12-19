namespace DocumentService.Core.Abstractions;

public sealed class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }

    // Parameterless ctor due to serialization, do not remove
    public Person()
    {
    }
    
    public Person(string firstName, string lastName, DateTimeOffset? dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(lastName));
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }
}