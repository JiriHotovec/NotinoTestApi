namespace DocumentService.Core.Abstractions;

public sealed class Tag
{
    private readonly string _value;

    public Tag(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        _value = value;
    }

    public override string ToString()
    {
        return _value;
    }
}