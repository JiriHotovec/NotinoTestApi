namespace DocumentService.Core.Abstractions;

public sealed class DocumentId : IEquatable<DocumentId>
{
    private readonly string _value;

    public DocumentId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        }

        _value = value;
    }

    public bool Equals(DocumentId? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(_value, other._value, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return _value;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is DocumentId other && Equals(other));
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(_value);
    }

    public static bool operator ==(DocumentId? left, DocumentId? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(DocumentId? left, DocumentId? right)
    {
        return !Equals(left, right);
    }
}