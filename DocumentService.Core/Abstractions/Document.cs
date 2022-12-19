namespace DocumentService.Core.Abstractions;

public sealed class Document : IEquatable<Document>
{
    public DocumentId Id { get; }
    public Person Data { get; }
    public IEnumerable<Tag> Tags { get; }

    public Document(DocumentId id, Person data, IEnumerable<Tag> tags)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Data = data ?? throw new ArgumentNullException(nameof(data));
        ArgumentNullException.ThrowIfNull(tags);
        Tags = tags.ToList();
    }

    public override string ToString()
    {
        return Id.ToString();
    }

    public bool Equals(Document? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is Document other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Document? left, Document? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Document? left, Document? right)
    {
        return !Equals(left, right);
    }
}